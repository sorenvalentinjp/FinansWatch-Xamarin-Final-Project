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
        //public event Action SavedArticlesChangedEvent;
        //public event Action<IList<Article>> Bucket1IsReady;
        //public event Action Bucket2IsReady;

        //articles and sections
        //public IList<Article> FrontPageArticles;
        //public IList<Article> LatestArticles;
        //public ObservableCollection<Article> SavedArticles;
        //public IList<Section> Sections;

        //controllers
        public ArticleController ArticleController;
        public LoginController LoginController;

        public StateController()
        {
            this.ArticleController = new ArticleController();
            //this.SavedArticles = new ObservableCollection<Article>();
            this.LoginController = new LoginController();

            //create sections in MasterDetailView
            //this.Sections = new List<Section>();

        }

        //-----------------Bucket methods start

        public async Task<IList<Article>> GetBucket1()
        {
            await this.ArticleController.GetBucket1FrontPage();

            //this.FrontPageArticles = await ArticleController.GetBucket1FrontPage();
            //Bucket1IsReady?.Invoke(this.ArticleController.FrontPageArticles);
            return this.ArticleController.FrontPageArticles;
        }

        
        public async void GetBucket2()
        {
            await this.ArticleController.GetBucket2();

            //this.LatestArticles = await ArticleController.GetBucket2(this.FrontPageArticles);

            //List<Task> taskList = new List<Task>();

            //foreach (var section in this.Sections)
            //{
            //    var lastTask = GetArticlesForSection(section);
            //    taskList.Add(lastTask);
            //}

            //await Task.WhenAll(taskList.ToArray());
            //GC.Collect(0);
            //Bucket2IsReady?.Invoke();
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

        //public void LocalStorageLoaded()
        //{
        //    this.Bucket1IsReady?.Invoke(this.FrontPageArticles);
        //    this.Bucket2IsReady?.Invoke();
        //}

        //--------Sections
        public async Task<Section> GetArticlesForSection(Section section)
        {
            section.Articles = await this.ArticleController.GetArticlesAndDetailsForSection(section);
            return section;
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

        

        /// <summary>
        /// If the article is not in the list, add it and return true, if it is in the list, remove it and return false
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        //public bool AddOrRemoveSavedArticle(Article article)
        //{
        //    if (!SavedArticles.Contains(article))
        //    {
        //        article.IsSaved = true;
        //        SavedArticles.Add(article);
        //        SavedArticlesChangedEvent();
        //        return true;
        //    }
        //    else
        //    {
        //        article.IsSaved = false;
        //        SavedArticles.Remove(article);
        //        SavedArticlesChangedEvent();
        //        return false;
        //    }
        //}

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
