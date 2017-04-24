using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FrontPageView : ContentPage, INotifyPropertyChanged
    {
        private ObservableCollection<Article> articles;
        public ObservableCollection<Article> Articles
        {
            get { return articles; }
            set
            {
                if (articles == value) { return; }
                articles = value;
                notify("Articles");
            }
        }
        
        private StateController StateController { get; set; }

        public FrontPageView(StateController stateController)
		{
			InitializeComponent();

            Content.BindingContext = this;
            this.StateController = stateController;
            getFrontPageArticles();

            //-----test - simple objects to test binding is working
            //List<Article> articles = new List<Article>();
            //Article a1 = new Article();
            //a1.Title = "Title 1 - LOOOOOOONG";
            //a1.Teaser = "Teaser1";
            //Article a2 = new Article();
            //a2.Title = "Title 2 - SHORT";
            //a2.Teaser = "Teaser2";
            //articles.Add(a1);
            //articles.Add(a2);
            //listView.BindingContext = articles;
        }

        public async void getFrontPageArticles()
        {
            IsRefreshing = true; //to show 'busy' indicator
            List<Article> fetchedArticles = await this.StateController.getFrontPageArticles();
            determineTopArticle(fetchedArticles);
            this.Articles = new ObservableCollection<Article>(fetchedArticles);
            IsRefreshing = false; //to remove 'busy' indicator again
        }

        /// <summary>
        /// Sets the proporty 'IsTopArticle' for all articles in the list.
        /// </summary>
        /// <param name="articles">The list containing the articles</param>
        private void determineTopArticle(List<Article> articles)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                if (i == 0)
                    articles[i].IsTopArticle = true;
                else
                    articles[i].IsTopArticle = false;
            }
        }

        //--------------------------- REFRESH STUFF --------------------------
        private bool isRefreshning = false;
        public bool IsRefreshing
        {
            get { return isRefreshning; }
            set
            {
                isRefreshning = value;
                notify("IsRefreshing");
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    getFrontPageArticles();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
