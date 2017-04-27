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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleView : ContentPage, INotifyPropertyChanged
    {
        private StateController stateController;
        private Article article;
        public Article Article
        {
            get { return article; }
            set
            {
                if (article == value) { return; }
                article = value;
                Notify("Article");
            }
        }

        public ArticleView(StateController stateController, Article article)
		{
            InitializeComponent();
            
            this.stateController = stateController;
            this.article = article;

            GetArticle();

            BindingContext = this.article;

            DisableItemSelectedAction();

            if (imageView.Source == null)
            {
                imageView.IsVisible = false;
                imageCaptionLabel.IsVisible = false;
            }


        }

        private async void GetArticle()
        {
            this.article = await this.stateController.getArticleDetails(this.article);
        }

        private void DisableItemSelectedAction()
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        private async void ListViewTabbedAction(object sender, ItemTappedEventArgs e)
        {
            Article tabbedArticle = (Article)e.Item;
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this.stateController, tabbedArticle)));
        }

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
