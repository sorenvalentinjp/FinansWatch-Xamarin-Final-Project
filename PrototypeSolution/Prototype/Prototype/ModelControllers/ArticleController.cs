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
    public class ArticleController
    {
        private ContentAPI contentAPI;
        public event Action<Article> articleIsReady;
        public event Action<bool> isRefreshing;

        public ArticleController()
        {
            contentAPI = new ContentAPI();
        }

        public async void getFrontPageArticlesAsync()
        {
            isRefreshing(true);
            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadFrontPageArticles());
            foreach (var articleJson in json)
            {
                Article newArt = new Article();

                //Other fields
                string contentURL = articleJson.contentUrl;
                string title = articleJson.titles.FRONTPAGE;                
                string teaser = articleJson.teasers.FRONTPAGE;
                Boolean locked = articleJson.locked;

                //Get frontpage image
                newArt.FrontPageImage = getFrontPageImage(articleJson);

                //Save fields
                newArt.Title = title;
                newArt.ContentURL = contentURL;
                newArt.Teaser = stripAllHtmlParagraphTags(teaser);
                newArt.Locked = locked;

                newArt = await getArticleDetailsAsync(newArt);

                articleIsReady(newArt);
            }
            isRefreshing(false);
        }

        public async void getRelatedArticlesAsync(Article article)
        {
            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadArticle(article.ContentURL));

            article.RelatedArticles.Clear();

            foreach (var relatedArticleJson in json.relatedArticles)
            {
                Article relatedArticle = new Article();
                relatedArticle.ContentURL = relatedArticleJson.url;
                relatedArticle.Title = relatedArticleJson.title;
                relatedArticle.Locked = relatedArticleJson.locked;

                //Download the related article json and fetch images
                dynamic relatedArticleDetailsJson = JsonConvert.DeserializeObject(await contentAPI.downloadArticle(relatedArticle.ContentURL));
                relatedArticle.ArticleImage = getArticleImage(relatedArticleDetailsJson);

                //Add the new related article
                article.RelatedArticles.Add(relatedArticle);
            }
        }

        public async Task<Article> getArticleDetailsAsync(Article article)
        {
            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadArticle(article.ContentURL));

            //Get fields
            string bodyText = json.bodyText;
            string title = json.titles.DEFAULT;
            string teaser = json.teasers.DEFAULT;
            string publishInfo = json.publishData.publishInfo;
            string homeSectionName = json.metadata.sectionDisplayName;

            //Get article image
            article.ArticleImage = getArticleImage(json);

            //Dates
            //String publishedDateString = json.publishData.publishedTime;
            //DateTime publishedDate = DateTime.ParseExact(publishedDateString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            //Save fields
            //Strip related articles used on the website, at the end of the bodytext.
            article.BodyText = stripRelatedArticles(bodyText);
            article.Title = title;
            article.Teaser = teaser;
            article.PublishInfo = publishInfo;
            article.HomeSectionName = homeSectionName;

            return article;

        }

        private ArticleImage getArticleImage(dynamic articleJson)
        {
            //Images
            try
            {
                foreach (var image in articleJson.topImages)
                {
                        bool isPrimaryImage = image.primary;
                        if (isPrimaryImage)
                        {
                            string imageBigUrl = image.big.url;
                            string imageSmallURL = image.small.url;
                            string imageThumbURL = image.thumb.url;
                            string imageCaption = "";
                            try
                            {
                                imageCaption = image.imageCaption;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(@"getArticleImages ImageCaption {0}", ex.Message);
                            }

                            return new ArticleImage(imageBigUrl, imageSmallURL, imageThumbURL, imageCaption);
                    }                    
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getArticleImages articleJson.topImages {0}", ex.Message);
                return null;
            }            
        }

        private ArticleImage getFrontPageImage(dynamic articleJson)
        {
            //Images
            try
            {
                string imageFrontPageBigUrl = articleJson.image.versions.big_article_460.url;
                string imageFrontPageSmallUrl = articleJson.image.versions.small_article_220.url;
                return new ArticleImage(imageFrontPageBigUrl, imageFrontPageSmallUrl, "", "");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getFrontPageImage {0}", ex.Message);
                return null;
            }
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
