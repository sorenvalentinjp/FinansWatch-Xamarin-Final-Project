using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.Cells;
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
        private readonly StateController _stateController;

        private IList<Grouping<string, Article>> _grouped;
        public IList<Grouping<string, Article>> Grouped
        {
            get { return _grouped; }
            set
            {
                if (_grouped == value) { return; }
                _grouped = value;
                Notify("Grouped");
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

        public AllArticlesView (StateController stateController)
		{
			InitializeComponent ();

            listView.ItemTemplate = new DataTemplate(() => new DateTimeCell(this._stateController, this));

            listView.GroupHeaderTemplate = new DataTemplate(() => new DateTimeCellGroupHeader());

            BindingContext = this;
            this._stateController = stateController;
            this._stateController.ArticleController.IsRefreshingLatestArticles += ArticleController_isRefreshing;
            this._stateController.ArticleController.LatestArticlesAreReady += ArticleController_latestArticlesAreReady;            
            this._stateController.ArticleController.GetLatestArticlesAsync();
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
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this._stateController, tappedArticle)), true);
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
                    this._stateController.ArticleController.GetLatestArticlesAsync();
                });
            }
        }
    }


}


