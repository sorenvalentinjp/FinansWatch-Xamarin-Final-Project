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

        private bool _subscriberHasAccess;
        public bool SubscriberHasAccess
        {
            get { return _subscriberHasAccess; }
            set
            {
                if (_subscriberHasAccess == value) { return; }
                _subscriberHasAccess = value;
                Notify("SubscriberHasAccess");
            }
        }

        private ImageSource _unlockedIndicatorImageSource;
        public ImageSource UnlockedIndicatorImageSource
        {
            get { return _unlockedIndicatorImageSource; }
            set
            {
                if (_unlockedIndicatorImageSource == value) { return; }
                _unlockedIndicatorImageSource = value;
                Notify("UnlockedIndicatorImageSource");
            }
        }

        private ImageSource _lockedIndicatorImageSource;
        public ImageSource LockedIndicatorImageSource
        {
            get { return _lockedIndicatorImageSource; }
            set
            {
                if (_lockedIndicatorImageSource == value) { return; }
                _lockedIndicatorImageSource = value;
                Notify("LockedIndicatorImageSource");
            }
        }

        private bool _lockedIndicatorImageVisible;
        public bool LockedIndicatorImageVisible
        {
            get { return _lockedIndicatorImageVisible; }
            set
            {
                if (_lockedIndicatorImageVisible == value) { return; }
                _lockedIndicatorImageVisible = value;
                Notify("LockedIndicatorImageVisible");
            }
        }

        private bool _unlockedIndicatorImageVisible;
        public bool UnlockedIndicatorImageVisible
        {
            get { return _unlockedIndicatorImageVisible; }
            set
            {
                if (_unlockedIndicatorImageVisible == value) { return; }
                _unlockedIndicatorImageVisible = value;
                Notify("UnlockedIndicatorImageVisible");
            }
        }

        public ArticleViewModel(StateController stateController, Article articleToDisplay)
        {
            this._stateController = stateController;

            Locked = CalculateIfArticleShouldBeLocked(articleToDisplay, _stateController.Subscriber);

            _stateController.LoginController.LoginEventSucceeded += LoginSucceeded;

            _stateController.LoginController.LogoutEvent += LogoutEvent;

            if (_stateController.SavedArticles.Contains(articleToDisplay))
                articleToDisplay.IsSaved = true;

            UnlockedIndicatorImageSource = ImageSource.FromFile("unlocked.png");
            LockedIndicatorImageSource = ImageSource.FromFile("locked.png");

            Article = articleToDisplay;

        }

        //Subscribed Event
        //If the user just logged in, recalculate if the article should display as locked
        private void LoginSucceeded(Subscriber subscriber)
        {
            SubscriberHasAccess = subscriber.HasAccessToSite();
            Locked = CalculateIfArticleShouldBeLocked(Article, _stateController.Subscriber);
        }

        private void LogoutEvent()
        {
            SubscriberHasAccess = false;
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
                if (SubscriberHasAccess)
                //If the subscriber is logged in, has access and the article is also locked, show the unlocked icon
                UnlockedIndicatorImageVisible = true;
                LockedIndicatorImageVisible = false;
            }
            else if(locked)
            {
                //If the article is locked and subscriber is not logged in or does not have access, show the locked icon
                LockedIndicatorImageVisible = true;
                UnlockedIndicatorImageVisible = false;

            } else
            {
                //If article is not locked, show no icons
                LockedIndicatorImageVisible = false;
                UnlockedIndicatorImageVisible = false;
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
