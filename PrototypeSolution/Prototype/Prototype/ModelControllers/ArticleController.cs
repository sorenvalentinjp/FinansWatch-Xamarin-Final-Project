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
using System.Linq;
using Newtonsoft.Json.Converters;
using Prototype.ViewModels;
using Xamarin.Forms;

namespace Prototype.ModelControllers
{
    public class ArticleController
    {
        private readonly ContentApi _contentApi;
        private StateController _stateController;
        public event Action<IList<Article>> FrontPageArticlesAreReady;
        public event Action<IList<Article>> LatestArticlesAreReady;
        public event Action<bool> IsRefreshingFrontPage;
        public event Action<bool> IsRefreshingLatestArticles;

        public ArticleController(StateController stateController)
        {
            _stateController = stateController;
            _contentApi = new ContentApi();
        }

        public async void GetLatestArticlesAsync()
        {
            IsRefreshingLatestArticles(true);

            IList<Article> articles = DeserializeArticlesFromJson(await _contentApi.DownloadLatestArticles());

            LatestArticlesAreReady(articles);

            IsRefreshingLatestArticles(false);
        }

        public async void GetFrontPageArticlesAsync()
        {
            IsRefreshingFrontPage(true);

            IList<Article> articles = DeserializeArticlesFromJson(await _contentApi.DownloadFrontPageArticles());
           
            FrontPageArticlesAreReady(articles);
            IsRefreshingFrontPage(false);
        }



        public async Task<IList<Article>> GetRelatedArticlesAsync(Article article)
        {
            IList<Article> newRelatedArticles = new List<Article>();
            foreach (var relatedArticle in article.relatedArticles)
            {
                var newRelatedArticle = await GetArticleDetailsAsync(relatedArticle.url);
                //We reuse the url from the related article in order to make equals work
                newRelatedArticle.contentUrl = relatedArticle.url;
                if (_stateController.SavedArticles.Contains(newRelatedArticle))
                {
                    newRelatedArticle.SavedImageSource = ImageSource.FromResource("saved.png");
                }
                    
                newRelatedArticles.Add(newRelatedArticle);
            }
            
            return newRelatedArticles;
        }

        public async Task<Article> GetArticleDetailsAsync(Article article)
        {
            Article detailedArticle = DeserializeArticle(await _contentApi.DownloadArticle(article.contentUrl));
            article.AddFieldsFromAnotherArticle(detailedArticle);
            SetArticleFields(article);

            return article;
        }

        public async Task<Article> GetArticleDetailsAsync(string contentUrl)
        {
            return DeserializeArticle(await _contentApi.DownloadArticle(contentUrl));

        }


        //Deserialize methods
        private IList<Article> DeserializeArticlesFromJson(string json)
        {
            //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd-MM-yyyy HH:mm" };
            IList<Article> articles = JsonConvert.DeserializeObject<List<Article>>(json);

            foreach (var article in articles)
            {
                StripArticle(article);
                try
                {
                    DateTime publishedDateTime = DateTime.ParseExact(article.publishedDate, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    article.publishedDateTime = publishedDateTime;
                }
                catch (Exception e) { Debug.Print(e.Message); }
            }

            if (articles.Count > 0)
            {
                articles[0].isTopArticle = true;
            }

            return articles;
        }


        public Article DeserializeArticle(string json)
        {        
            var article = JsonConvert.DeserializeObject<Article>(json);

            article = SetArticleFields(article);

            return article;

        }



        //Helper methods
        private Article SetArticleFields(Article article)
        {
            //article.AddFieldsFromAnotherArticle(newArt);
            article = StripArticle(article);

            if (article.topImages.Count > 0)
            {
                article.topImage = article.topImages[0];
            }
            return article;
        }

        private Article StripArticle(Article article)
        {
            article.bodyText = StripRelatedArticles(article.bodyText);
            article.teasers.FRONTPAGE = StripAllHtmlParagraphTags(article.teasers.FRONTPAGE);
            article.teasers.DEFAULT = StripAllHtmlParagraphTags(article.teasers.DEFAULT);
            return article;
        }


        private string StripAllHtmlParagraphTags(string html)
        {
            if (html == null)
            {
                return "";
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
        private string StripRelatedArticles(string html)
        {
            if (html == null)
            {
                return "";
            }
            var pattern = "<ul.*</ul>";
            return Regex.Replace(html, pattern, "");
        }

    }


}
