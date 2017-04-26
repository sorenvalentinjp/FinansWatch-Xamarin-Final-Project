using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
            disableItemSelectedAction();

            Content.BindingContext = this;
            this.StateController = stateController;
            getFrontPageArticles();

            
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
            articles[0].IsTopArticle = true;
            //for (int i = 0; i < articles.Count; i++)
            //{
            //    if (i == 0)
            //        articles[i].IsTopArticle = true;
            //    else
            //        articles[i].IsTopArticle = false;
            //}
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

        //Disables selection on the frontpage
        private void disableItemSelectedAction()
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        private async void listViewTabbedAction(object sender, ItemTappedEventArgs e)
        {
            Article tabbedArticle = (Article)e.Item;
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(StateController, tabbedArticle)));
        }
    }
}
