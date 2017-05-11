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

            DataTemplate = new SectionTemplateSelector(_stateController);

            //
            if (section.Articles == null)
            {
                DownloadSectionArticles(Section);
            }
            else
            {
                //Prepare articleViewModels
            }
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
                return new Command(() =>
                {
                    //Refresh
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
