using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrontPageView : ContentPage, INotifyPropertyChanged
    {
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

        private readonly StateController stateController;

        public FrontPageView(StateController stateController)
		{
			InitializeComponent();

            listView.ItemTemplate = new SectionTemplateSelector(stateController, this);

            BindingContext = this;
            
            this.stateController = stateController;
		    IsRefreshing = false;
            this.stateController.ArticleController.IsRefreshingFrontPage += IsRefreshingChanged;
            this.stateController.ArticleController.FrontPageArticlesAreReady += FrontPageArticlesAreReady;
            this.stateController.GetFrontPageArticles();
            DisableItemSelectedAction();
        }

        private void FrontPageArticlesAreReady(IList<Article> newArticles)
        {
            Articles = newArticles;
        }

        //IsRefreshing is used to display an 'busy' icon while the listview is refreshing its content.
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
                return new Command(() =>
                {
                    this.stateController.GetFrontPageArticles();
                });
            }
        }

        /// <summary>
        /// We dont want the user to be able to select the articles. Its on by default, so this method is nessecary to counter that.
        /// </summary>
        private void DisableItemSelectedAction()
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        //Because INotifyProportyChanged is implemented
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
