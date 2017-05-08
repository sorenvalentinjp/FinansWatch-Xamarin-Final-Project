using Prototype.Models;
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

        /// <summary>
        /// Fetches all frontpage articles async and wait for the articleIsReady events to fire
        /// During this operation a 'refresh' icon is displayed.
        /// </summary>
        public void GetFrontPageArticles()
        {
            ArticleController.GetFrontPageArticlesAsync();
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
