using System.Collections;
using Prototype.ModelControllers;
using Prototype.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prototype.Views.Cells;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class AllArticlesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StateController _stateController;

        private IList<Grouping<string, ArticleViewModel>> _grouped;
        public IList<Grouping<string, ArticleViewModel>> Grouped
        {
            get { return _grouped; }
            set
            {
                if (_grouped == value) { return; }
                _grouped = value;
                Notify("Grouped");
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing == value) { return; }
                _isRefreshing = value;
                Notify("IsRefreshing");
            }
        }

        private DataTemplate _dataTemplateGroupHeader;
        public DataTemplate DataTemplateGroupHeader
        {
            get { return _dataTemplateGroupHeader; }
            set
            {
                if (_dataTemplateGroupHeader == value) { return; }
                _dataTemplateGroupHeader = value;
                Notify("DataTemplateGroupHeader");
            }
        }

        private DataTemplate _dataTemplate;
        public DataTemplate DataTemplate
        {
            get { return _dataTemplate; }
            set
            {
                if (_dataTemplate == value) { return; }
                _dataTemplate = value;
                Notify("DataTemplate");
            }
        }

        public AllArticlesViewModel(StateController stateController)
        {
            this._stateController = stateController;
            IsRefreshing = false;

            this.DataTemplate = new DataTemplate(() => new DateTimeCell(stateController));
            this.DataTemplateGroupHeader = new DataTemplate(() => new DateTimeCellGroupHeader());

            _stateController.LoginController.LoginEventSucceeded += LoginEvent;
            _stateController.ArticleController.SavedArticlesChangedEvent += SavedArticlesChanged;

            if (_stateController.ArticleController.LatestArticles == null)
            {
                RefreshLatestArticlesAsync();
            }
            else
            {
                GroupArticlesAsync();
            }
        }

        /// <summary>
        /// If the user just logged in then recalculate if the articles should display as locked
        /// </summary>
        /// <param name="subscriber"></param>
        private void LoginEvent(Subscriber subscriber)
        {
            foreach (var grouping in this.Grouped)
            {
                foreach (var articleViewModel in grouping)
                {
                    articleViewModel.CalculateIfArticleShouldBeLocked();
                }
            }
        }

        /// <summary>
        /// If the user adds an article to saved articles, notify article viewm models to update the cell icons
        /// </summary>
        private void SavedArticlesChanged()
        {
            foreach (var grouping in Grouped)
            {
                foreach (var articleViewModel in grouping)
                {
                    articleViewModel.SavedArticlesChanged();
                }
            }
        }

        /// <summary>
        /// Refreshes the articles by downloading them again.
        /// </summary>
        private async void RefreshLatestArticlesAsync()
        {
            IsRefreshing = true;
            Grouped = await GroupArticles(await _stateController.ArticleController.GetLatestArticlesAsync());
            IsRefreshing = false;
        }

        /// <summary>
        /// Groups the articles by the date they are published
        /// </summary>
        public async void GroupArticlesAsync()
        {
            Grouped = await GroupArticles(_stateController.ArticleController.LatestArticles);
        }

        /// <summary>
        /// Helper for GroupArticlesAsync
        /// </summary>
        public Task<List<Grouping<string, ArticleViewModel>>> GroupArticles(IList<Article> articles)
        {
            return Task.Run((() =>
            {
                IList<ArticleViewModel> articleViewModels = new List<ArticleViewModel>();

                foreach (var article in articles)
                {
                    articleViewModels.Add(new ArticleViewModel(_stateController, article));
                }

                var sortedArticleViewModels = from articleViewModel in articleViewModels
                    orderby articleViewModel.Article.publishedDateTime descending
                    group articleViewModel by articleViewModel.Article.publishedDateTime.Date.ToString("dd. MMMM",
                        CultureInfo.InvariantCulture)
                    into articleGroup
                    select new Grouping<string, ArticleViewModel>(articleGroup.Key, articleGroup);

                var groupedArticleViewModels = new List<Grouping<string, ArticleViewModel>>(sortedArticleViewModels);

                return groupedArticleViewModels;
            }));
        }

        /// <summary>
        /// When the user refreshed the view, this.articles is updated.
        /// </summary>
        public ICommand RefreshCommand => new Command(RefreshLatestArticlesAsync);

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
