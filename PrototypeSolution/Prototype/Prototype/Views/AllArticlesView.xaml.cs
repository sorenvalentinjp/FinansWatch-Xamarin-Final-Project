using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllArticlesView : ContentPage, INotifyPropertyChanged
    {
        private StateController StateController;

        private IList<Grouping<string, Article>> grouped;
        public IList<Grouping<string, Article>> Grouped
        {
            get { return grouped; }
            set
            {
                if (grouped == value) { return; }
                grouped = value;
                Notify("Grouped");
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

        public AllArticlesView (StateController stateController)
		{
			InitializeComponent ();
            BindingContext = this;
            this.StateController = stateController;
            this.StateController.ArticleController.isRefreshingLatestArticles += ArticleController_isRefreshing;
            this.StateController.ArticleController.latestArticlesAreReady += ArticleController_latestArticlesAreReady;            
            this.StateController.ArticleController.getLatestArticlesAsync();
            DisableItemSelectedAction();
        }

        private void DisableItemSelectedAction()
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        private void ArticleController_isRefreshing(bool isRefreshing)
        {
            IsRefreshing = isRefreshing;
        }

        private void ArticleController_latestArticlesAreReady(List<Grouping<string, Article>> groupedArticles)
        {
            Grouped = groupedArticles;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedArticle = (Article)e.Item;
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this.StateController, tappedArticle)), true);
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
                    this.StateController.ArticleController.getLatestArticlesAsync();
                });
            }
        }
    }


}


