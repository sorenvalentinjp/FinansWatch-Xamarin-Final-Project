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
            Article = article;
            BindingContext = Article;
            DisableItemSelectedAction();
            this.stateController = stateController;

            getArticleDetails();

            this.stateController.getRelatedArticles(Article);
        }

        private async void getArticleDetails()
        {
            if (article.bodyText == null)
            {
                Article = await this.stateController.getArticleDetails(Article);
            }

            //If articledetails havent already been fetched, await the code above to get the data, then check if the imagesource is null.
            if (Article.topImages.Count == 0)
            {
                imageView.IsVisible = false;
                imageCaptionLabel.IsVisible = false;
            } else
            {
                imageView.BindingContext = Article.topImages[0];
                imageCaptionLabel.BindingContext = Article.topImages[0];
            }
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
            await Navigation.PushModalAsync(new NavigationPage(new ArticleView(this.stateController, await stateController.getArticleDetails(tabbedArticle))), true);
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
