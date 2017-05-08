using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views;
using Prototype.Views.Helpers;
using Prototype.Views.TemplateSelectors;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly StateController _stateController;
        private Article _article;
        public Article Article
        {
            get { return _article; }
            set
            {
                if (_article == value) { return; }
                _article = value;
                Notify("Article");
            }
        }

        private bool _locked;
        public bool Locked
        {
            get { return _locked; }
            set
            {
                if (_locked == value) { return; }
                _locked = value;
                Notify("Locked");
            }
        }

        private ImageSource _lockedIndicatorImageSource;

        public ImageSource LockedIndicatorImageSource
        {
            get { return _lockedIndicatorImageSource;}
            set
            {
                if (_lockedIndicatorImageSource == value) { return; }
                _lockedIndicatorImageSource = value;
                Notify("LockedIndicatorImageSource");
            }
        }

        public ArticleViewModel(StateController stateController, Article articleToDisplay)
        {
            this._stateController = stateController;

            Locked = CalculateIfArticleShouldBeLocked(articleToDisplay, _stateController.Subscriber);

            _stateController.LoginController.LoginSucceeded += LoginSucceeded;

            Article = articleToDisplay;
        }

        //Subscribed Event
        //If the user just logged in, recalculate if the article should display as locked
        private void LoginSucceeded(Subscriber subscriber)
        {
            Locked = CalculateIfArticleShouldBeLocked(Article, _stateController.Subscriber);
        }

        /// <summary>
        /// Calculates wether the view should lock the article based on the logged or not not logged in subscribers access and the locked property on the article.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        private bool CalculateIfArticleShouldBeLocked(Article article, Subscriber subscriber)
        {
            var locked = article.locked;
            if (locked && subscriber != null)
            {
                if (subscriber.HasAccessToSite())
                    locked = false;
                //If the subscriber is logged in, has access and the article is also locked, show the unlocked icon
                this.LockedIndicatorImageSource = ImageSource.FromResource("unlocked.png");
            }
            else if(locked)
            {
                //If the article is locked and subscriber is not logged in or does not have access, show the locked icon
                this.LockedIndicatorImageSource = ImageSource.FromResource("locked.png");
            }
            
            return locked;
        }

        public async Task<ArticleViewModel> GetArticleDetails()
        {
            if (Article.bodyText == "")
            {
                Article = await this._stateController.GetArticleDetails(Article);
            }

            Article.relatedDetailedArticles = await this._stateController.GetRelatedArticles(Article);

            return this;
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
