using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
            this._stateController = stateController;
            this.ArticleViewModels = new List<ArticleViewModel>();
            Section = section;

            this._stateController.Bucket2IsReady += BucketIsReady;

            DataTemplate = new SectionTemplateSelector(_stateController);

            //
            if (section.Articles == null)
            {
                DownloadSectionArticles(Section);
            }
            else
            {
                BucketIsReady();
            }
        }

        public void BucketIsReady()
        {
            IList<ArticleViewModel> articleViewModels = new List<ArticleViewModel>();
            foreach (var article in this.Section.Articles)
            {
                articleViewModels.Add(new ArticleViewModel(this._stateController, article));
            }
            this.ArticleViewModels = articleViewModels;
            IsRefreshing = false;
        }

        public async void DownloadSectionArticles(Section section)
        {
            section.Articles = await this._stateController.ArticleController.GetArticlesForSection(section);
            IList<ArticleViewModel> articleViewModels = new List<ArticleViewModel>();
            foreach (var article in section.Articles)
            {
                articleViewModels.Add(new ArticleViewModel(_stateController, article));
            }
            this.ArticleViewModels = articleViewModels;
        }

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
