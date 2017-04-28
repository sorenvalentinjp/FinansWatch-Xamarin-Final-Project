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

        private StateController stateController;

        public FrontPageView(StateController stateController)
		{
			InitializeComponent();
            BindingContext = this;
            
            this.stateController = stateController;
            this.stateController.ArticleController.isRefreshing += IsRefreshingChanged;
            Articles = this.stateController.FrontPageArticles;
            this.stateController.getFrontPageArticles();
            DisableItemSelectedAction();
        }

        //IsRefreshing is used to display an 'busy' icon while the listview is refreshing its content.
        private void IsRefreshingChanged(bool isRefreshing)
        {
            IsRefreshing = isRefreshing;
        }

        override protected void OnAppearing()
        {
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
                    this.stateController.getFrontPageArticles();
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
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this.stateController, tabbedArticle)), true);
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
