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
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action SavedArticlesChangedEvent;

        public ArticleController ArticleController;
        public ObservableCollection<Article> SavedArticles;
        public LoginController LoginController;
        private Subscriber _subscriber;
        public Subscriber Subscriber
        {
            get { return _subscriber; }
            set
            {
                if (_subscriber == value) { return; }
                _subscriber = value;
                Notify("Subscriber");
            }
        }

        public StateController()
        {
            this.ArticleController = new ArticleController(this);
            this.SavedArticles = new ObservableCollection<Article>();
            this.LoginController = new LoginController(this);
        }

        //-----------------Bucket methods start
        //bucket events
        public event Action<IList<Article>> Bucket1IsReady;
        public event Action Bucket2IsReady;

        public IList<Article> FrontPageArticles;
        public async void GetBucket1()
        {
            this.FrontPageArticles = await ArticleController.GetBucket1FrontPage();
            Bucket1IsReady?.Invoke(this.FrontPageArticles);
        }

        public IList<Article> LatestArticles;
        public async void GetBucket2()
        {
            this.LatestArticles = await ArticleController.GetBucket2(this.FrontPageArticles);
            Bucket2IsReady?.Invoke();
        }

        /// <summary>
        /// When the user pulls down to refresh the frontpage, all frontpage articles and their details are downloaded again
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Article>> RefreshFrontPage()
        {
            this.FrontPageArticles = await this.ArticleController.GetBucket1FrontPage();
            this.ArticleController.GetArticleDetailsForCollection(this.FrontPageArticles);
            return this.FrontPageArticles;
        }

        /// <summary>
        /// When the user pulls down to refresh latest articles view, all articles and their details are downloaded again
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Article>> RefreshLatestArticles()
        {
            this.LatestArticles = await this.ArticleController.GetLatestArticlesAsync();
            this.ArticleController.GetArticleDetailsForCollection(this.LatestArticles);
            return this.LatestArticles;
        }

        public void LocalStorageLoaded()
        {
            this.Bucket1IsReady?.Invoke(this.FrontPageArticles);
            this.Bucket2IsReady?.Invoke();
        }
        //-----------------Bucket methods end

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

        /// <summary>
        /// If the article is not in the list, add it and return true, if it is in the list, remove it and return false
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public bool AddOrRemoveSavedArticle(Article article)
        {
            if (!SavedArticles.Contains(article))
            {
                article.IsSaved = true;
                SavedArticles.Add(article);
                SavedArticlesChangedEvent();
                return true;
            }
            else
            {
                article.IsSaved = false;
                SavedArticles.Remove(article);
                SavedArticlesChangedEvent();
                return false;
            }
        }
    }

}
