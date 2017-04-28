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

        public async Task<IList<Article>> getFrontPageArticles()
        {
            IList<Article> articles = new List<Article>();

            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadFrontPageArticles());
            foreach (var articleJson in json)
            {
                Article newArt = new Article();

                //Other fields
                string title = articleJson.titles.FRONTPAGE;
                string contentURL = articleJson.contentUrl;
                string teaser = articleJson.teasers.FRONTPAGE;
                Boolean locked = articleJson.locked;

                //BigImage
                //SmallImage
                //newArt.ImageBigURL = getFrontPageArticleBigUrl(articleJson);
                //newArt.ImageSmallURL = getFrontPageArticleSmallUrl(articleJson);
                
                //Save fields
                newArt.Title = title;
                newArt.ContentURL = contentURL;
                newArt.Teaser = stripAllHtmlParagraphTags(teaser);
                newArt.Locked = locked;

                getFrontPageImage(articleJson, newArt);

                newArt = await getArticleDetails(newArt);

                articles.Add(newArt);
            }

            return articles;
        }

        public async Task<Article> getArticleDetails(Article article)
        {
            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadArticle(article.ContentURL));

            //Get fields
            string bodyText = json.bodyText;
            string title = json.titles.DEFAULT;
            string teaser = json.teasers.DEFAULT;
            string publishInfo = json.publishData.publishInfo;
            string homeSectionName = json.metadata.sectionDisplayName;

            //BigImage
            //SmallImage
            //ImageCaption
            getArticleImage(json, article);
            //ImageSource bigImageSource = getArticleBigImageSource(json);
            //string imageCaption = getArticleImageCaption(json);

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
            //article.ImageSourceBig = bigImageSource;
            //article.ImageCaption = imageCaption;
            article.RelatedArticles = await getRelatedArticles(json.relatedArticles);

            return article;

        }

        /// <summary>
        /// Takes a json string with a list of related articles. Returns a list of related article objects including their small images.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="article"></param>
        private async Task<IList<Article>> getRelatedArticles(dynamic relatedArticlesJson)
        {
            IList<Article> relatedArticles = new List<Article>();

            foreach (var relatedArticleJson in relatedArticlesJson)
            {
                Article relatedArticle = new Article();
                relatedArticle.ContentURL = relatedArticleJson.url;
                relatedArticle.Title = relatedArticleJson.title;
                relatedArticle.Teaser = relatedArticleJson.teaser.DEFAULT;
                relatedArticle.Locked = relatedArticleJson.locked;

                //Download the related article json and fetch images
                //string relatedArticleDetailsJson = await contentAPI.downloadArticle(relatedArticle.ContentURL);
                dynamic relatedArticleDetailsJson = JsonConvert.DeserializeObject(await contentAPI.downloadArticle(relatedArticle.ContentURL));
                getArticleImage(relatedArticleDetailsJson, relatedArticle);

                //Add the new related article
                relatedArticles.Add(relatedArticle);
            }
            return relatedArticles;
        }

        private void getArticleImage(dynamic articleJson, Article article)
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

                            article.ImageSourceBig = new UriImageSource { Uri = new Uri(imageBigUrl) };
                            article.ImageSourceSmall = new UriImageSource { Uri = new Uri(imageSmallURL) };
                            article.ImageSourceThumb = new UriImageSource { Uri = new Uri(imageThumbURL) };
                    }                    
                }
                //return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getArticleImages articleJson.topImages {0}", ex.Message);
                //return null;
            }            
        }

        private Article getFrontPageImage(dynamic articleJson, Article article)
        {
            //Images
            try
            {
                string imageFrontPageBigUrl = articleJson.image.versions.big_article_460.url;
                string imageFrontPageSmallUrl = articleJson.image.versions.small_article_220.url;
                //string imageThumbURL = "";
                //string imageCaption = "";
                article.ImageSourceFrontPageBig = new UriImageSource { Uri = new Uri(imageFrontPageBigUrl) };
                article.ImageSourceFrontPageSmall = new UriImageSource { Uri = new Uri(imageFrontPageSmallUrl) };
                return article;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getFrontPageImage {0}", ex.Message);
                return article;
            }
        }

        private string getArticleBigImageUrl(dynamic articleJson)
        {
            //BigImage
            try
            {
                foreach (var image in articleJson.topImages)
                {
                    return image.big.url;
                }
                return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getArticleBigImageSource {0}", ex.Message);
                return null;
            }
        }


        private string getArticleSmallImageUrl(dynamic articleJson)
        {
            //SmallImage
            try
            {
                foreach (var image in articleJson.topImages)
                {
                    return image.small.url;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getArticleSmallImageSource {0}", ex.Message);
                return null;
            }
        }

        private string getArticleImageCaption(dynamic articleJson)
        {
            //ImageCaption
            try
            {
                foreach (var image in articleJson.topImages)
                {
                    return image.imageCaption;
                }
                return "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getArticleImageCaption {0}", ex.Message);
                return null;
            }
        }

        private string getFrontPageArticleBigUrl(dynamic articleJson)
        {
            //BigImage
            try
            {
                return articleJson.image.versions.big_article_460.url;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getFrontPageArticleBigImageSource {0}", ex.Message);
                return null;
            }
        }

        private string getFrontPageArticleSmallUrl(dynamic articleJson)
        {
            //SmallImage
            try
            {
                return articleJson.image.versions.small_article_220.url;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"getFrontPageArticleSmallImageSource {0}", ex.Message);
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
