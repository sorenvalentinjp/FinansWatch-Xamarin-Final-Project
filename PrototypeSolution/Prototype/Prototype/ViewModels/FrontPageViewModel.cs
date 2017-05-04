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

        private IList<Article> articles;
        public IList<Article> Articles
        {
            get { return articles; }
            set
            {
                if (articles == value) { return; }
                articles = value;
                Notify("Articles");
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing == value) { return; }
                isRefreshing = value;
                Notify("IsRefreshing");
            }
        }

        private DataTemplate dateTemplate;
        public DataTemplate DataTemplate
        {
            get { return dateTemplate; }
            set
            {
                if (dateTemplate == value) { return; }
                dateTemplate = value;
                Notify("DataTemplate");
            }
        }

        private StateController stateController;

        public FrontPageViewModel(StateController stateController)
        {
            //DataTemplate = new SectionTemplateSelector(stateController);
            
            this.stateController = stateController;
            //this.stateController.ArticleController.isRefreshingFrontPage += IsRefreshingChanged;
            //this.stateController.ArticleController.frontPageArticlesAreReady += FrontPageArticlesAreReady;
            this.stateController.GetFrontPageArticles();            
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
