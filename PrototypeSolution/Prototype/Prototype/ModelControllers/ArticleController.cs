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
        public event Action<IList<Article>> LatestArticlesAreReady;
        public event Action<bool> IsRefreshingFrontPage;
        public event Action<bool> IsRefreshingLatestArticles;

        public ArticleController(StateController stateController)
        {
            _stateController = stateController;
            _contentApi = new ContentApi();
        }


        //-------------------Bucket methods start

        //BUCKET 1
        /// <summary>
        /// Downloading all frontpage articles WITHOUT including their details. This is to speed up load of the FrontPageView
        /// </summary>
        public async Task<IList<Article>> GetBucket1FrontPage()
        {
            return await GetFrontPageArticlesAsync();
        }

        /// <summary>
        /// Downloading details for the frontpage articles + all other articles the app is capable of presenting
        /// </summary>
        /// <param name="frontPageArticles"></param>
        /// <returns></returns>
        public async Task<IList<Article>> GetBucket2(IList<Article> frontPageArticles)
        {
            //Details for frontpage articles
            GetArticleDetailsForCollection(frontPageArticles);

            //Latest articles
            IList<Article> latestArticles = await GetLatestArticlesAsync();
            GetArticleDetailsForCollection(latestArticles);
            return latestArticles;

            //TODO
            //Section X

            //Section Y
        }

        //Currently used
        //pengeinstitutter(id 373)
        //forsikringer (id 3002)
        //pension (id 356)
        //realkredit (id 404)
        //navne og job(id 344)
        //klummer (413 ??)

        //not used atm
        //finansnyt: 353 (formentlig forsiden)
        //Kapitalforvaltning: 3011
        public async Task<IList<Article>> GetArticlesForSection(Section section)
        {
            IList<Article> articles = DeserializeArticlesFromJson(await _contentApi.DownloadSection(section.Id, section.Area));
            GetArticleDetailsForCollection(articles);
            return articles;
        }

        //BUCKET HELPER
        /// <summary>
        /// Downloading all details for the collection of articles
        /// </summary>
        /// <param name="articles"></param>
        public async void GetArticleDetailsForCollection(IList<Article> articles)
        {
            foreach (var article in articles)
            {
                await GetArticleDetailsAsync(article);
            }
        }

        //------------------Bucket methods end

        ////////////////////////////////////////////////////////////////////////////////////
        public async Task<IList<Article>> GetLatestArticlesAsync()
        {
            IsRefreshingLatestArticles?.Invoke(true);

            IList<Article> articles = DeserializeArticlesFromJson(await _contentApi.DownloadLatestArticles());

            LatestArticlesAreReady?.Invoke(articles);

            IsRefreshingLatestArticles?.Invoke(false);

            return articles;
        }

        public async Task<IList<Article>> GetFrontPageArticlesAsync()
        {
            IsRefreshingFrontPage?.Invoke(true);

            IList<Article> articles = DeserializeArticlesFromJson(await _contentApi.DownloadFrontPageArticles());

            foreach (var article in articles)
            {
                PrepareArticle(article);
            }
           
            IsRefreshingFrontPage?.Invoke(false);

            return articles;
        }



        public async Task<IList<Article>> GetRelatedArticlesAsync(Article article)
        {
            IList<Article> newRelatedArticles = new List<Article>();
            foreach (var relatedArticle in article.relatedArticles)
            {
                var newRelatedArticle = await GetArticleDetailsAsync(relatedArticle.url);
                //We reuse the url from the related article in order to make equals work
                newRelatedArticle.contentUrl = relatedArticle.url;
                    
                newRelatedArticles.Add(newRelatedArticle);
            }
            
            return newRelatedArticles;
        }

        public async Task<Article> GetArticleDetailsAsync(Article article)
        {
            Article detailedArticle = DeserializeArticle(await _contentApi.DownloadArticle(article.contentUrl));
            article.AddFieldsFromAnotherArticle(detailedArticle);
            PrepareArticle(article);

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

            article = PrepareArticle(article);

            return article;

        }

        //Helper methods
        private Article PrepareArticle(Article article)
        {
            article = StripArticle(article);

            if (article.topImages?.Count > 0)
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
