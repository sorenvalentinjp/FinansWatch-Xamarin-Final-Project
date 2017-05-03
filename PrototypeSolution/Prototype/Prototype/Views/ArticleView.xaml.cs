using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
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

        public ArticleView(StateController stateController, Article articleToDisplay)
        {
            InitializeComponent();

            listView.ItemTemplate = new RelatedArticleTemplateSelector(stateController, this);

            this.stateController = stateController;
            getArticleDetails(articleToDisplay);

            DisableItemSelectedAction();
            
            
        }

        private async void getArticleDetails(Article articleToDisplay)
        {
            if (articleToDisplay.bodyText == null)
            {
                articleToDisplay = await this.stateController.getArticleDetails(articleToDisplay);
            }

            //If articledetails havent already been fetched, await the code above to get the data, then check if the imagesource is null.
            if (articleToDisplay.topImage == null)
            {
                imageView.IsVisible = false;
                imageCaptionLabel.IsVisible = false;
            } 

            articleToDisplay.relatedDetailedArticles = await this.stateController.getRelatedArticles(articleToDisplay);
            Article = articleToDisplay;
            BindingContext = Article;
        }

        private void DisableItemSelectedAction()
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
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
