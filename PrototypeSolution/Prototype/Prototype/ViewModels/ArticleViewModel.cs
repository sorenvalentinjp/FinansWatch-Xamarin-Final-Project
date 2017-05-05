using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
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

        private bool _hasAccess;
        public bool HasAccess
        {
            get { return _hasAccess; }
            set
            {
                if (_hasAccess == value) { return; }
                _hasAccess = value;
                Notify("HasAccess");
            }
        }

        public ArticleViewModel(StateController stateController, Article articleToDisplay)
        {
            this._stateController = stateController;
            DataTemplate = new RelatedArticlesTemplateSelector(_stateController);

            if (_stateController.Subscriber != null)
            {
                HasAccess = _stateController.Subscriber.HasAccessToSite("finanswatch");
            }
            else
            {
                HasAccess = false;
            }

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

        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    //App.Navigation.PushAsync();
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
