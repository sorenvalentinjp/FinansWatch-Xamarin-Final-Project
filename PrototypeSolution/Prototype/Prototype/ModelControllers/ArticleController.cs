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
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.ModelControllers
{
    class ArticleController
    {
        private ContentAPI contentAPI;
        private ImageDownloader imageDownloader;

        public ArticleController()
        {
            contentAPI = new ContentAPI();
            imageDownloader = new ImageDownloader();
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
                try
                {
                    //string image620URL = article.image.versions.huge_article_620.url;
                    string image460URL = article.image.versions.big_article_460.url;
                    //string image380URL = article.image.versions.frontpage_large_380.url;
                    //string image300URL = article.image.versions.medium_frontpage_300.url;
                    string image220URL = article.image.versions.small_article_220.url;
                    //string imageFURL = article.image.versions.f.url;
                    //string imageEURL = article.image.versions.e.url;
                    //string imageDURL = article.image.versions.d.url;
                    string imageCaption = article.image.imageCaption;
                    newArt.ImageBigURL = image460URL;
                    newArt.ImageSmallURL = image220URL;
                    newArt.ImageCaption = imageCaption;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"getFrontPageArticles {0}", ex.Message);
                }


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
                newArt.PublishedDate = publishedDate;
                

                articles.Add(newArt);
            }

            foreach(Article a in articles)
            {
                downloadArticleBigImage(a);
                downloadArticleSmallImage(a);
                //downloadArticleThumbImage(newArt);
            }

            return articles;
        }

        public async Task<Article> getArticleDetails(Article article)
        { 
            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadArticle(article.ContentURL));
                
            //Get fields
            string bodyText = json.bodyText;
            int id = json.id;
            string publishInfo = json.publishData.publishInfo;

            //Save fields
            article.BodyText = bodyText;
            article.Id = id;
            article.PublishInfo = publishInfo;

            return article;
        }

        public async void downloadArticleBigImage(Article article)
        {
            //Set the article imagesource async
            article.ImageSourceBig = await imageDownloader.downloadImage(article.ImageBigURL);
        }

        public async void downloadArticleSmallImage(Article article)
        {
            //Set the article imagesource async
            article.ImageSourceSmall = await imageDownloader.downloadImage(article.ImageSmallURL);
        }

        public async void downloadArticleThumbImage(Article article)
        {
            //Set the article imagesource async
            article.ImageSourceThumb = await imageDownloader.downloadImage(article.ImageThumbURL);
        }

    }


}
