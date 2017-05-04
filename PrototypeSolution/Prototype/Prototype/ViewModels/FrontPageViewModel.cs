using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    class FrontPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IList<Article> _articles;
        public IList<Article> Articles
        {
            get { return _articles; }
            set
            {
                if (_articles == value) { return; }
                _articles = value;
                Notify("Articles");
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

        private readonly StateController _stateController;

        public FrontPageViewModel(StateController stateController)
        {
            //DataTemplate = new SectionTemplateSelector(stateController);
            
            this._stateController = stateController;
            //this.stateController.ArticleController.isRefreshingFrontPage += IsRefreshingChanged;
            //this.stateController.ArticleController.frontPageArticlesAreReady += FrontPageArticlesAreReady;
            this._stateController.GetFrontPageArticles();            
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
