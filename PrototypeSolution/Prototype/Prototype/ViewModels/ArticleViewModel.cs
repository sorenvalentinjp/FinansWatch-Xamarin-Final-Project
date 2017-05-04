using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Prototype.ModelControllers;
using Prototype.Models;
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

        public ArticleViewModel(StateController stateController, Article articleToDisplay, Page page)
        {
            this._stateController = stateController;
            DataTemplate = new RelatedArticlesTemplateSelector(_stateController, page);

            GetArticleDetails(articleToDisplay);
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

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
