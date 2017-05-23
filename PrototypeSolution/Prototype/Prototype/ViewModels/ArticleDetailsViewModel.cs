
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
using Plugin.Share;
using Plugin.Share.Abstractions;
using System.Linq;

namespace Prototype.ViewModels
{
    public class ArticleDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StateController _stateController;
        private ArticleViewModel _articleViewModel;
        public ArticleViewModel ArticleViewModel
        {
            get { return _articleViewModel; }
            set
            {
                if (_articleViewModel == value) { return; }
                _articleViewModel = value;
                Notify("ArticleViewModel");
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

        private IList<ArticleViewModel> _relatedArticleViewModels;
        public IList<ArticleViewModel> RelatedArticleViewModels
        {
            get { return _relatedArticleViewModels; }
            set
            {
                if (_relatedArticleViewModels == value) { return; }
                _relatedArticleViewModels = value;
                Notify("RelatedArticleViewModels");
            }
        }

        public ArticleDetailsViewModel(StateController stateController, ArticleViewModel articleViewModel)
        {
            this._stateController = stateController;
            DataTemplate = new RelatedArticlesTemplateSelector(_stateController);

            ArticleViewModel = articleViewModel;

            GetRelatedArticleViewModels(ArticleViewModel.Article);

            _stateController.LoginController.LoginEventSucceeded += LoginEvent;
        }

        /// <summary>
        /// When the loginEvent is invoked, all then calculate if the related articles should be locked
        /// </summary>
        /// <param name="subscriber"></param>
        private void LoginEvent(Subscriber subscriber)
        {
            foreach (var relatedArticleViewModel in RelatedArticleViewModels)
            {
                relatedArticleViewModel.CalculateIfArticleShouldBeLocked();
            }
        }

        /// <summary>
        /// Downloads all the articles related articles
        /// </summary>
        /// <param name="article"></param>
        private async void GetRelatedArticleViewModels(Article article)
        {
            IList<ArticleViewModel> toReturn = new List<ArticleViewModel>();
            foreach (var relatedArticle in await _stateController.ArticleController.GetRelatedArticlesAsync(article))
            {
                toReturn.Add(new ArticleViewModel(_stateController, relatedArticle));
            }
            this.RelatedArticleViewModels = toReturn;
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

        /// <summary>
        /// Allows the user to share the article using the native interface og the users device
        /// </summary>
        public ICommand ShareCommand
        {
            get
            {
                return new Command(async () =>
                {
                    ShareMessage msg = new ShareMessage();
                    msg.Url = this.ArticleViewModel.Article.desktopUrl;

                    if (msg.Url != null)
                    {
                        await CrossShare.Current.Share(msg);
                    }
                    else
                    {
                        await App.Navigation.NavigationStack.First().DisplayAlert("", "Artiklen kan desværre ikke deles, fordi artiklens web adresse ikke kunne findes.", "OK");
                    }
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