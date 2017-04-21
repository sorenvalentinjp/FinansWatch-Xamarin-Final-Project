using Prototype.Database;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.ModelControllers
{
    class ArticleController
    {
        private ContentAPI contentAPI;

        public ArticleController()
        {
            contentAPI = new ContentAPI();
        }

        public async Task<List<Article>> getFrontPageArticles()
        {
            List<Article> articles = new List<Article>();

            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadFrontPageArticles());
            foreach (var article in json)
            {
                Article newArt = new Article();

                //Other fields
                string title = article.titles.FRONTPAGE;
                string contentURL = article.contentUrl;
                int homeSectionId = article.homeSectionId;
                string homeSectionName = article.homeSectionName;
                string teaser = article.teasers.FRONTPAGE;
                Boolean locked = article.locked;

                //Imageversions
                string image620URL = article.image.versions.huge_article_620.url;
                string image460URL = article.image.versions.big_article_460.url;
                string image380URL = article.image.versions.frontpage_large_380.url;
                string image300URL = article.image.versions.medium_frontpage_300.url;
                string image220URL = article.image.versions.small_article_220.url;
                string imageFURL = article.image.versions.f.url;
                string imageEURL = article.image.versions.e.url;
                string imageDURL = article.image.versions.d.url;

                //Dates
                String publishedDateString = article.publishedDate;
                DateTime publishedDate = DateTime.ParseExact(publishedDateString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

                //Save fields
                newArt.Title = title;
                newArt.ContentURL = contentURL;
                newArt.HomeSectionId = homeSectionId;
                newArt.HomeSectionName = homeSectionName;
                newArt.Teaser = teaser;
                newArt.Locked = locked;
                newArt.ImageBigURL = image460URL;
                newArt.ImageSmallURL = image220URL;

                articles.Add(newArt);
            }

            return articles;
        }
    }
}
