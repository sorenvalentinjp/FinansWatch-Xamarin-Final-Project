﻿using Newtonsoft.Json;
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
using System.Collections.ObjectModel;
using Prototype.Helpers;

namespace Prototype.ModelControllers
{
    /// <summary>
    /// This class is responsible for keeping track of all articles that have been downloaded.
    /// The class also contains methods to update the articles that is stored in the various collections using.
    /// These methods use _contentApi to download the articles through Jyllands-Postens Content API.
    /// </summary>
    public class ArticleController
    {
        private readonly IContentApi _contentApi;

        //events
        public event Action SavedArticlesChangedEvent;

        //collections of articles
        public IList<Article> LatestArticles;
        public ObservableCollection<Article> SavedArticles;
        public IList<Section> Sections;

        public ArticleController()
        {
            _contentApi = new ContentApi();
            this.SavedArticles = new ObservableCollection<Article>();
            this.Sections = new List<Section>();
        }

        /// <summary>
        /// Constructor for tests only
        /// </summary>
        public ArticleController(IContentApi contentApi, IList<Section> sections)
        {
            _contentApi = contentApi;
            this.SavedArticles = new ObservableCollection<Article>();
            this.Sections = sections;
        }

        /// <summary>
        /// Downloads all FrontPageArticles WITHOUT including their details. This is to speed up the loading/refreshing of the FrontPageView.
        /// </summary>
        public async Task<IList<Article>> GetBucket1FrontPageAsync()
        {

            Section frontPageSection = this.Sections.FirstOrDefault();
            if (frontPageSection != null)
            {
                frontPageSection.Articles = await GetSectionArticlesAsync(frontPageSection);
                return frontPageSection.Articles;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Downloading details for the FrontPageArticles + LatestArticles + Sections and their details.
        /// </summary>
        /// <returns></returns>
        public async Task GetBucket2Async()
        {
            //Details for frontpage articles is downloaded async
            Section frontPageSection = this.Sections.FirstOrDefault();
            if (frontPageSection != null)
            {
                GetArticleDetailsForCollectionAsync(frontPageSection.Articles);
            }            

            //Latest articles is downloaded and awaited. Afterwards the details are downloaded
            //This is to speed up the loading of LatestArticlesView
            IList<Article> latestArticles = await GetLatestArticlesAsync();
            GetArticleDetailsForCollectionAsync(latestArticles);

            //Downloading sections
            for(int i = 1; i < this.Sections.Count; i++)
            {
                Section currentSection = this.Sections[i];
                currentSection.Articles = await GetArticlesAndDetailsForSectionAsync(currentSection);
            }
        }

        /// <summary>
        /// This method downloads articles for a given section + the details for each article in the section.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public async Task<IList<Article>> GetArticlesAndDetailsForSectionAsync(Section section)
        {
            string json = await _contentApi.DownloadSection(section.SectionContentUrl);
            IList<Article> articles = DeserializeArticlesFromJson(json);
            GetArticleDetailsForCollectionAsync(articles);
            return articles;
        }

        /// <summary>
        /// Downloading all details for the collection of articles
        /// </summary>
        /// <param name="articles"></param>
        public async Task<IList<Article>> GetArticleDetailsForCollectionAsync(IList<Article> articles)
        {
            foreach (var article in articles)
            {
                await GetArticleDetailsAsync(article);
            }
            return articles;
        }

        /// <summary>
        /// Downloading latest articles without their details
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Article>> GetLatestArticlesAsync()
        {
            this.LatestArticles = DeserializeArticlesFromJson(await _contentApi.DownloadLatestArticles());
            return this.LatestArticles;
        }

        /// <summary>
        /// Downloading a sections articles without the details.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public async Task<IList<Article>> GetSectionArticlesAsync(Section section)
        {
            IList<Article> articles = DeserializeArticlesFromJson(await _contentApi.DownloadSection(section.SectionContentUrl));

            foreach (var article in articles)
            {
                PrepareArticle(article);
            }            
            return articles;
        }

        /// <summary>
        /// Downloading all related articles for an article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public async Task<IList<Article>> GetRelatedArticlesAsync(Article article)
        {
            IList<Article> newRelatedArticles = new List<Article>();
            try
            {
                
                foreach (var relatedArticle in article.relatedArticles)
                {
                    var newRelatedArticle = await GetArticleDetailsAsync(relatedArticle.url);
                    //We reuse the url from the related article in order to make equals work
                    newRelatedArticle.contentUrl = relatedArticle.url;

                    newRelatedArticles.Add(newRelatedArticle);
                }
                return newRelatedArticles;
            }
            catch (Exception e)
            {
                Debug.Print("Could not download related articles: " + e.Message);
                return newRelatedArticles;
            }
        }

        /// <summary>
        /// Downloading the details for an article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public async Task<Article> GetArticleDetailsAsync(Article article)
        {
            Article detailedArticle = DeserializeArticle(await _contentApi.DownloadArticle(article.contentUrl));
            article.AddFieldsFromAnotherArticle(detailedArticle);
            PrepareArticle(article);

            return article;
        }

        /// <summary>
        /// Downloading the details for an article using its contentUrl
        /// </summary>
        /// <param name="contentUrl"></param>
        /// <returns></returns>
        public async Task<Article> GetArticleDetailsAsync(string contentUrl)
        {
            return DeserializeArticle(await _contentApi.DownloadArticle(contentUrl));
        }


        /// <summary>
        /// If the article is not in the list, add it and return true, if it is in the list, remove it and return false
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public bool AddOrRemoveSavedArticle(Article article)
        {
            if (!this.SavedArticles.Contains(article))
            {
                article.IsSaved = true;
                this.SavedArticles.Add(article);
                this.SavedArticlesChangedEvent?.Invoke();
                return true;
            }
            else
            {
                article.IsSaved = false;
                this.SavedArticles.Remove(article);
                this.SavedArticlesChangedEvent?.Invoke();
                return false;
            }
        }

        /// <summary>
        /// Deserializes a list of articles from json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private IList<Article> DeserializeArticlesFromJson(string json)
        {
            IList<Article> articles = new List<Article>();
            try
            {
                articles = JsonConvert.DeserializeObject<List<Article>>(json);
            }
            catch (Exception e)
            {
                Debug.Print("Could not deserialize articles: " + e.Message);
                return articles;
            }

            foreach (var article in articles)
            {
                //Strip teasers to remove unwanted html tags
                ArticleStripper.StripArticleTeasers(article);
                try
                {
                    DateTime publishedDateTime = DateTime.ParseExact(article.publishedDate, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    article.publishedDateTime = publishedDateTime;
                }
                catch (Exception e) { Debug.Print(e.Message); }
            }

            //The first article in the list is a top article
            if (articles.Count > 0)
            {
                articles[0].isTopArticle = true;
            }            

            return articles;
        }

        /// <summary>
        /// Deserializes a single article from json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Article DeserializeArticle(string json)
        {
            try
            {
                var article = JsonConvert.DeserializeObject<Article>(json);

                article = PrepareArticle(article);

                return article;
            }
            catch (Exception e)
            {
                Debug.Print("Could not deserialize article: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Prepares the an article so it looks good in the app.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Article PrepareArticle(Article article)
        {
            article = ArticleStripper.StripArticleBodyText(article);
            article = ArticleStripper.StripArticleTeasers(article);

            if (article.topImages?.Count > 0)
            {
                article.topImage = article.topImages[0];
            }

            return article;
        }
    }
}
