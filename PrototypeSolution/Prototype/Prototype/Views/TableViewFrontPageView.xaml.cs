using FFImageLoading.Forms;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.CustomRenderers;
using Refractored.XamForms.PullToRefresh;
using System;
using System.Collections.Generic;
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
	public partial class TableViewFrontPageView : ContentPage, INotifyPropertyChanged
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

        private StateController stateController;
        public TableViewFrontPageView (StateController stateController)
		{
			InitializeComponent ();
            this.stateController = stateController;
            GetFrontPageArticles();

            var masterStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            IList<StackLayout> articleStackLayouts = new List<StackLayout>();
            foreach (var article in articles)
            {
                masterStackLayout.Children.Add(
                    new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Children =
                      {
                        new CachedImage { Source = article.ImageSourceBig},
                        new Label { Text = article.Title },
                        new HtmlFormattedLabel { Text = article.Teaser, FontAttributes = FontAttributes.Bold }
                      }
                    }); 
            }



            foreach (var sl in articleStackLayouts)
            {
            }

            var scrollView = new ScrollView();

            var refreshView = new PullToRefreshLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Content = scrollView,
                RefreshColor = Color.FromHex("#3498db")
            };

            //Set Bindings
            //refreshView.SetBinding<TestViewModel>(PullToRefreshLayout.IsRefreshingProperty, vm => vm.IsBusy, BindingMode.OneWay);
            //refreshView.SetBinding<TestViewModel>(PullToRefreshLayout.RefreshCommandProperty, vm => vm.RefreshCommand);

            Content = refreshView;


            //Additionally, your content could be anything you want including a StackLayout:




        }

        public async void GetFrontPageArticles()
        {
            IsRefreshing = true; //to show 'busy' indicator
            List<Article> fetchedArticles = await this.stateController.getFrontPageArticles();
            this.Articles = fetchedArticles;
            IsRefreshing = false; //to remove 'busy' indicator again
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
