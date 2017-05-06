using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views;
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

        private DataTemplate _dateTemplate;
        public DataTemplate DataTemplate
        {
            get { return _dateTemplate; }
            set
            {
                if (_dateTemplate == value) { return; }
                _dateTemplate = value;
                Notify("DataTemplate");
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

        public ArticleViewModel(StateController stateController, Article articleToDisplay)
        {
            this._stateController = stateController;
            DataTemplate = new RelatedArticlesTemplateSelector(_stateController);

            Locked = CalculateIfArticleShouldBeLocked(articleToDisplay, _stateController.Subscriber);

            _stateController.LoginController.LoginSucceeded += LoginSucceeded;

            GetArticleDetails(articleToDisplay);
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
                if (subscriber.HasAccessToSite()) locked = false;
            }
            return locked;
        }

        private async void GetArticleDetails(Article articleToDisplay)
        {
            if (articleToDisplay.bodyText == "")
            {
                articleToDisplay = await this._stateController.GetArticleDetails(articleToDisplay);
            }

            articleToDisplay.relatedDetailedArticles = await this._stateController.GetRelatedArticles(articleToDisplay);

            Article = articleToDisplay;
        }

        /// <summary>
        /// When user clicks on login the view navigates to login screen
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                App.Navigation.PushAsync(new LoginView(new LoginViewModel(_stateController)));
            });
            }
        }

        /// <summary>
        /// When user clicks try watch, the user gets linked to site try url.
        /// </summary>
        public ICommand TryWatchCommand
        {
            get
            {
                return new Command(() =>
                {
                    string url = "https://secure.finanswatch.dk/user/create?mode=trial";
                    Device.OpenUri(new Uri(url));
                });
            }
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
