using Prototype.ModelControllers;
using Prototype.Models;
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
        private Article tappedArticle;
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

        private StateController stateController;

        public FrontPageView(StateController stateController)
		{
			InitializeComponent();
            BindingContext = this;
            
            this.stateController = stateController;
            this.stateController.ArticleController.isRefreshing += IsRefreshingChanged;
            this.stateController.ArticleController.frontPageArticlesAreReady += FrontPageArticlesAreReady;
            this.stateController.getFrontPageArticles();
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

        //Because INotifyProportyChanged is implemented
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        /// When the user taps an article, that article is stored and used for the MR.Gesture commands.
        /// This is a hack to avoid highlighting the tapped article.
        /// </summary>
        private void ListViewTappedAction(object sender, ItemTappedEventArgs e)
        {
            this.tappedArticle = (Article)e.Item;
        }

        /// <summary>
        /// Single tap - no longer than 250ms, otherwise it is considered a longpress instead
        /// </summary>
        private async void TappedGesture(object sender, MR.Gestures.TapEventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this.stateController, this.tappedArticle)), true);
            
        }

        /// <summary>
        /// When tap takes longer than 250ms
        /// </summary>
        private void LongPressedGesture(object sender, MR.Gestures.LongPressEventArgs e)
        {
            if (this.stateController.SavedArticles.Contains(this.tappedArticle))
            {
                this.stateController.SavedArticles.Remove(this.tappedArticle);
                DisplayAlert("", "Artiklen er fjernet fra læselisten.", "OK");
                Console.WriteLine("REMOVED: " + this.tappedArticle.Title);
            }
            else
            {
                this.stateController.SavedArticles.Add(this.tappedArticle);
                DisplayAlert("", "Artiklen er gemt i læselisten.", "OK");
                Console.WriteLine("ADDED: " + this.tappedArticle.Title);
            }
        }
    }
}
