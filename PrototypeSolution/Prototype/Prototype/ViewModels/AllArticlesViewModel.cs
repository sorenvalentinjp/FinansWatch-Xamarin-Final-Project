﻿using Prototype.ModelControllers;
using Prototype.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
            this._stateController.ArticleController.IsRefreshingLatestArticles += IsRefreshingChanged;
            this._stateController.Bucket2IsReady += Bucket2IsReady;

            this.DataTemplate = new DataTemplate(() => new DateTimeCell(stateController));
            this.DataTemplateGroupHeader = new DataTemplate(() => new DateTimeCellGroupHeader());

            if (_stateController.LatestArticles == null)
            {
                _stateController.GetBucket2();
            }
            else
            {
                Bucket2IsReady();
            }
        }

        private void Bucket2IsReady()
        {
            GroupArticles(_stateController.LatestArticles);
        }

        private void GroupArticles(IList<Article> articles)
        {
            IList<ArticleViewModel> articleViewModels = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                articleViewModels.Add(new ArticleViewModel(_stateController, article));
            }

            var sortedArticleViewModels = from articleViewModel in articleViewModels
                                          orderby articleViewModel.Article.publishedDateTime descending
                                          group articleViewModel by articleViewModel.Article.publishedDateTime.Date.ToString("dd. MMMM", CultureInfo.InvariantCulture) into articleGroup
                                          select new Grouping<string, ArticleViewModel>(articleGroup.Key, articleGroup);

            var groupedArticleViewModels = new List<Grouping<string, ArticleViewModel>>(sortedArticleViewModels);

            Grouped = groupedArticleViewModels;
        }

        private void IsRefreshingChanged(bool isRefreshing)
        {
            IsRefreshing = isRefreshing;
        }

        /// <summary>
        /// When the user refreshed the view, this.articles is updated.
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IList<Article> refreshedArticles = await _stateController.RefreshLatestArticles();
                    GroupArticles(refreshedArticles);
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
