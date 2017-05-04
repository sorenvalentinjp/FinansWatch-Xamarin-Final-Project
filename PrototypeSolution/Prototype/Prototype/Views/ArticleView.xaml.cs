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
        private readonly StateController _stateController;
        private Article _article;
        public Article Article
        {
            get { return _article; }
            set
            {
                if (_article == value) { return; }
                _article = value;
                Notify("Article");
            }
        }

        public ArticleView(StateController stateController, Article articleToDisplay)
        {
            InitializeComponent();

            listView.ItemTemplate = new RelatedArticlesTemplateSelector(stateController, this);

            this._stateController = stateController;
            GetArticleDetails(articleToDisplay);

            DisableItemSelectedAction();
            
            
        }

        private async void GetArticleDetails(Article articleToDisplay)
        {
            if (articleToDisplay.bodyText == "")
            {
                articleToDisplay = await this._stateController.GetArticleDetails(articleToDisplay);
            }

            //If articledetails havent already been fetched, await the code above to get the data, then check if the imagesource is null.
            if (articleToDisplay.topImage == null)
            {
                imageView.IsVisible = false;
                imageCaptionLabel.IsVisible = false;
            } 

            articleToDisplay.relatedDetailedArticles = await this._stateController.GetRelatedArticles(articleToDisplay);
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
