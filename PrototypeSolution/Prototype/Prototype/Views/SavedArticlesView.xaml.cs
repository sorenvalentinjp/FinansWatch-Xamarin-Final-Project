using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavedArticlesView : ContentPage, INotifyPropertyChanged
	{
        private Article tappedArticle;
        private ObservableCollection<Article> savedArticles;
        public ObservableCollection<Article> SavedArticles
        {
            get { return savedArticles; }
            set
            {
                if(savedArticles == value) { return; }
                savedArticles = value;
                Notify("SavedArticles");
            }
        }

        private StateController stateController;

        public SavedArticlesView(StateController stateController)
		{
			InitializeComponent();
            DisableItemSelectedAction();
            BindingContext = this;
            this.stateController = stateController;
            this.SavedArticles = this.stateController.SavedArticles;
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
        /// When the user tabs an article, that article is pushed modally as a navigation page.
        /// </summary>
        private void ListViewTappedAction(object sender, ItemTappedEventArgs e)
        {
            this.tappedArticle = (Article)e.Item;
            //Article tabbedArticle = (Article)e.Item;
            //this.SavedArticles.Remove(tabbedArticle);
            //await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this.stateController, tabbedArticle)), true);
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
        private void LongPressingGesture(object sender, MR.Gestures.LongPressEventArgs e)
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
