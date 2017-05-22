using Prototype.Models;
using Prototype.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.ModelControllers
{
    /// <summary>
    /// This class is linking the view models to the different controllers (eg. ArticleController, LoginController and so on).
    /// This way the StateController contains information about the state of the app (which articles are downloaded, is the user logged in etc),
    /// which means, we can store the app's StateController locally, when the app is put into the background and load it again on resume.
    /// </summary>
    public class StateController
    {
        //controllers
        public ArticleController ArticleController;
        public LoginController LoginController;

        public StateController()
        {
            this.ArticleController = new ArticleController();
            this.LoginController = new LoginController();
        }

        /// <summary>
        /// Downloads and return bucket 1.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Article>> GetBucket1()
        {
            return await this.ArticleController.GetBucket1FrontPageAsync();
        }

        /// <summary>
        /// Downloads and returns bucket 2.
        /// </summary>
        public async void GetBucket2()
        {
            await this.ArticleController.GetBucket2Async();
        }

        /// <summary>
        /// When the user pulls down to refresh latest articles view, all articles and their details are downloaded again
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Article>> RefreshLatestArticles()
        {
            await this.ArticleController.GetLatestArticlesAsync();
            await this.ArticleController.GetArticleDetailsForCollectionAsync(this.ArticleController.LatestArticles);
            return this.ArticleController.LatestArticles;
        }

        /// <summary>
        /// Given a section all it's articles and their details are downloaded and returned.
        /// Used when the user pulls down to refresh a SectionView.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public async Task<Section> GetArticlesForSection(Section section)
        {
            section.Articles = await this.ArticleController.GetArticlesAndDetailsForSectionAsync(section);
            return section;
        } 

        /// <summary>
        /// Given an article, all it's related articles are downloaded and returned.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Task<IList<Article>> GetRelatedArticles(Article article)
        {
            return ArticleController.GetRelatedArticlesAsync(article);
        }

        /// <summary>
        /// Given an article, all it's details are downloaded and returned, so it contains all nessecary data to view the article in ArticleDetailsView.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Task<Article> GetArticleDetails(Article article)
        {
            return this.ArticleController.GetArticleDetailsAsync(article);
        }
    }
}
