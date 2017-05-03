using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prototype.Database;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Newtonsoft.Json.Converters;

namespace Prototype.ModelControllers
{
    public class ArticleController
    {
        private ContentAPI contentAPI;
        public event Action<IList<Article>> frontPageArticlesAreReady;
        public event Action<List<Grouping<string, Article>>> latestArticlesAreReady;
        public event Action<bool> isRefreshingFrontPage;
        public event Action<bool> isRefreshingLatestArticles;

        public ArticleController()
        {
            contentAPI = new ContentAPI();
        }

        public async void getLatestArticlesAsync()
        {
            isRefreshingLatestArticles(true);

            IList<Article> articles = deserializeArticlesFromJson(await contentAPI.downloadLatestArticles());

            var sortedArticles = from article in articles
                                 orderby article.publishedDateTime descending
                                 group article by article.publishedDateTime.Date.ToString("dd. MMMM", CultureInfo.InvariantCulture) into articleGroup
                                 select new Grouping<string, Article>(articleGroup.Key, articleGroup);

            var groupedArticles = new List<Grouping<string, Article>>(sortedArticles);

            latestArticlesAreReady(groupedArticles);
            isRefreshingLatestArticles(false);
        }

        public async void getFrontPageArticlesAsync()
        {
            isRefreshingFrontPage(true);

            IList<Article> articles = deserializeArticlesFromJson(await contentAPI.downloadFrontPageArticles());
           
            frontPageArticlesAreReady(articles);
            isRefreshingFrontPage(false);
        }



        public async Task<IList<Article>> getRelatedArticlesAsync(Article article)
        {
            IList<Article> newRelatedArticles = new List<Article>();
            foreach (var relatedArticle in article.relatedArticles)
            {
                newRelatedArticles.Add(await getArticleDetailsAsync(relatedArticle.url));
            }
            return newRelatedArticles;
        }

        public async Task<Article> getArticleDetailsAsync(Article article)
        {
            return deserializeArticle(await contentAPI.downloadArticle(article.contentUrl));

        }

        public async Task<Article> getArticleDetailsAsync(string contentUrl)
        {
            return deserializeArticle(await contentAPI.downloadArticle(contentUrl));

        }


        //Deserialize methods
        private IList<Article> deserializeArticlesFromJson(string json)
        {
            //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd-MM-yyyy HH:mm" };
            IList<Article> articles = JsonConvert.DeserializeObject<List<Article>>(json);

            foreach (var article in articles)
            {
                stripArticle(article);
                try
                {
                    DateTime publishedDateTime = DateTime.ParseExact(article.publishedDate, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    article.publishedDateTime = publishedDateTime;
                }
                catch (Exception) { }
            }

            if (articles.Count > 0)
            {
                articles[0].isTopArticle = true;
            }

            return articles;
        }


        public Article deserializeArticle(string json)
        {        
            var article = JsonConvert.DeserializeObject<Article>(json);

            article = setArticleFields(article);

            return article;

        }



        //Helper methods
        private Article setArticleFields(Article article)
        {
            //article.addFieldsFromAnotherArticle(newArt);
            article = stripArticle(article);

            if (article.topImages.Count > 0)
            {
                article.topImage = article.topImages[0];
            }
            return article;
        }

        private Article stripArticle(Article article)
        {
            article.bodyText = stripRelatedArticles(article.bodyText);
            article.teasers.FRONTPAGE = stripAllHtmlParagraphTags(article.teasers.FRONTPAGE);
            article.teasers.DEFAULT = stripAllHtmlParagraphTags(article.teasers.DEFAULT);
            return article;
        }


        private string stripAllHtmlParagraphTags(string html)
        {
            if (html == null)
            {
                return null;
            }
            var pattern = "<p>|<\\/p>";
            return Regex.Replace(html, pattern, "");
        }

        /// <summary>
        /// Strips html text with related articles from a html string. Used in every article at the end of its bodytext.
        /// Works by stripping <ul></ul> tags, so it might strip lists who arent meant to be stripped.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private string stripRelatedArticles(string html)
        {
            if (html == null)
            {
                return null;
            }
            var pattern = "<ul.*</ul>";
            return Regex.Replace(html, pattern, "");
        }

    }


}
