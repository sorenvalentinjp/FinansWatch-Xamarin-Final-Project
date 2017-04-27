using Prototype.ModelControllers;
using Prototype.Models;
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
        private ObservableCollection<Article> articles;
        public ObservableCollection<Article> Articles
        {
            get { return articles; }
            set
            {
                if (articles == value) { return; }
                articles = value;
                Notify("Articles");
            }
        }

        private StateController stateController;

        public FrontPageView(StateController stateController)
		{
			InitializeComponent();
            DisableItemSelectedAction();
            Content.BindingContext = this;
            this.stateController = stateController;
            GetFrontPageArticles();
        }

        /// <summary>
        /// Fetches all frontpage articles and stores them in the viewmodel.
        /// During this operation a 'refresh' icon is displayed.
        /// </summary>
        public async void GetFrontPageArticles()
        {
            IsRefreshing = true; //to show 'busy' indicator
            List<Article> fetchedArticles = await this.stateController.getFrontPageArticles();
            DetermineTopArticle(fetchedArticles);
            this.Articles = new ObservableCollection<Article>(fetchedArticles);
            IsRefreshing = false; //to remove 'busy' indicator again
        }

        /// <summary>
        /// Sets the proporty 'IsTopArticle' for the toparticle to true.
        /// This is to be able to assign the correct template to the article.
        /// </summary>
        /// <param name="articles">The list containing the articles</param>
        private void DetermineTopArticle(List<Article> articles)
        {
            articles[0].IsTopArticle = true;
        }

        //IsRefrehing is used to display an 'busy' icon while the listview is refreshing its content.
        private bool isRefreshning = false;
        public bool IsRefreshing
        {
            get { return isRefreshning; }
            set
            {
                isRefreshning = value;
                Notify("IsRefreshing");
            }
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
                    GetFrontPageArticles();
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

        /// <summary>
        /// When the user tabs an article, that article is pushed modally as a navigation page.
        /// </summary>
        private async void ListViewTabbedAction(object sender, ItemTappedEventArgs e)
        {
            Article tabbedArticle = (Article)e.Item;
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this.stateController, tabbedArticle)));
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
