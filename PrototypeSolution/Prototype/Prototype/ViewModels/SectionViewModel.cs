﻿using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class SectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly StateController _stateController;
        private Section _section;
        public Section Section
        {
            get { return _section; }
            set
            {
                if (_section == value) { return; }
                _section = value;
                Notify("Section");
            }
        }

        private IList<ArticleViewModel> _articleViewModels;
        public IList<ArticleViewModel> ArticleViewModels
        {
            get { return _articleViewModels; }
            set
            {
                if (_articleViewModels == value) { return; }
                _articleViewModels = value;
                Notify("ArticleViewModels");
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

        public SectionViewModel(StateController stateController, Section section)
        {
            IsRefreshing = false;
            _stateController = stateController;
            this.ArticleViewModels = new List<ArticleViewModel>();
            Section = section;

            DataTemplate = new SectionTemplateSelector(_stateController);

            _stateController.LoginController.LoginEventSucceeded += LoginEvent;
            _stateController.ArticleController.SavedArticlesChangedEvent += SavedArticlesChanged;

            this.Section.ArticlesChanged += BucketIsReady;
            if (this.Section.Articles != null)
            {
                BucketIsReady();
            }
        }

        /// <summary>
        /// If the user just logged in, recalculate if the article should display as locked
        /// </summary>
        /// <param name="subscriber"></param>
        private void LoginEvent(Subscriber subscriber)
        {
            foreach (var articleViewModel in ArticleViewModels)
            {
                articleViewModel.CalculateIfArticleShouldBeLocked();
            }
        }

        /// <summary>
        /// If the user adds an article to saved articles, notify article viewm models to update the cell icons.
        /// </summary>
        private void SavedArticlesChanged()
        {
            foreach (var articleViewModel in ArticleViewModels)
            {
                articleViewModel.SavedArticlesChanged();
            }
        }

        /// <summary>
        /// When the bucket is ready, then create ArticleViewModels for each article in the section. 
        /// </summary>
        public void BucketIsReady()
        {
            Task.Run(() =>
            {
                IList<ArticleViewModel> articleViewModels = new List<ArticleViewModel>();
                foreach (var article in this.Section.Articles)
                {
                    articleViewModels.Add(new ArticleViewModel(this._stateController, article));
                }
                this.ArticleViewModels = articleViewModels;
                IsRefreshing = false;
            });
        }

        /// <summary>
        /// Refreshes the section by downloading its article again
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    this.Section = await _stateController.GetArticlesForSection(this.Section);
                    BucketIsReady();
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
