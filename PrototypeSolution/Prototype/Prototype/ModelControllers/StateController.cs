using Prototype.Models;
using Prototype.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.ModelControllers
{
    public class StateController : INotifyPropertyChanged
    {
        //events
        public event PropertyChangedEventHandler PropertyChanged;

        //controllers
        public ArticleController ArticleController;
        public LoginController LoginController;

        public StateController()
        {
            this.ArticleController = new ArticleController();
            this.LoginController = new LoginController();
        }

        public async Task<IList<Article>> GetBucket1()
        {
            await this.ArticleController.GetBucket1FrontPage();
            return this.ArticleController.FrontPageArticles;
        }

        public async void GetBucket2()
        {
            await this.ArticleController.GetBucket2();
        }

        /// <summary>
        /// When the user pulls down to refresh the frontpage, all frontpage articles and their details are downloaded again
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Article>> RefreshFrontPage()
        {
            await this.ArticleController.GetBucket1FrontPage();
            this.ArticleController.GetArticleDetailsForCollection(this.ArticleController.FrontPageArticles);
            return this.ArticleController.FrontPageArticles;
        }

        /// <summary>
        /// When the user pulls down to refresh latest articles view, all articles and their details are downloaded again
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Article>> RefreshLatestArticles()
        {
            await this.ArticleController.GetLatestArticlesAsync();
            this.ArticleController.GetArticleDetailsForCollection(this.ArticleController.LatestArticles);
            return this.ArticleController.LatestArticles;
        }

        public async Task<Section> GetArticlesForSection(Section section)
        {
            section.Articles = await this.ArticleController.GetArticlesAndDetailsForSection(section);
            return section;
        } 

        public Task<IList<Article>> GetRelatedArticles(Article article)
        {
            return ArticleController.GetRelatedArticlesAsync(article);
        }

        public Task<Article> GetArticleDetails(Article article)
        {
            return this.ArticleController.GetArticleDetailsAsync(article);
        }

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
