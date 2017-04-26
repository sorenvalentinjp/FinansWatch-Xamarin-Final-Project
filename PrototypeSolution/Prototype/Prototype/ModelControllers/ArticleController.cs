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

                //BigImage
                try
                {
                    //string image620URL = article.image.versions.huge_article_620.url;
                    string image460URL = article.image.versions.big_article_460.url;
                    //string image380URL = article.image.versions.frontpage_large_380.url;
                    //string image300URL = article.image.versions.medium_frontpage_300.url;
                    //string image220URL = article.image.versions.small_article_220.url;
                    //string imageFURL = article.image.versions.f.url;
                    //string imageEURL = article.image.versions.e.url;
                    //string imageDURL = article.image.versions.d.url;

                    newArt.ImageBigURL = image460URL;
                    newArt.ImageSourceBig = new UriImageSource { CachingEnabled = true, Uri = new Uri(newArt.ImageBigURL) };

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"getFrontPageArticles {0}", ex.Message);
                }

                //SmallImage
                try
                {
                    string image220URL = article.image.versions.small_article_220.url;
                    newArt.ImageSmallURL = image220URL;
                    newArt.ImageSourceSmall = new UriImageSource { CachingEnabled = true, Uri = new Uri(newArt.ImageSmallURL) };
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"getFrontPageArticles {0}", ex.Message);
                }

                //ImageCaption
                try
                {
                    string imageCaption = article.image.imageCaption;
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
                newArt.Teaser = stripAllHtmlParagraphTags(teaser);
                newArt.Locked = locked;
                newArt.PublishedDate = publishedDate;

                articles.Add(newArt);
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
            //Relatedarticles
            var relatedArticles = json.relatedArticles;
            foreach(var relatedArticleJson in relatedArticles)
            {
                Article relatedArticle = new Article();
                relatedArticle.ContentURL = relatedArticleJson.url;
                relatedArticle.Title = relatedArticleJson.title;
                relatedArticle.HomeSectionName = relatedArticleJson.sectionName;
                relatedArticle.Teaser = relatedArticleJson.teaser.DEFAULT;
                relatedArticle.Locked = relatedArticleJson.locked;
                article.RelatedArticles.Add(relatedArticle);
            }

            //Save fields
            //Strip related articles used on the website, at the end of the bodytext.
            article.BodyText = stripRelatedArticles(bodyText);            
            article.Id = id;
            article.PublishInfo = publishInfo;

            return article;

        }

        private string stripAllHtmlParagraphTags(string html)
        {          
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
            var pattern = "<ul.*</ul>";
            return Regex.Replace(html, pattern, "");
        }

    }


}
