using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleView : ContentPage
	{
        private StateController stateController;
        private Article article;

        public ArticleView(StateController stateController, Article article)
		{
			InitializeComponent();
            DisableItemSelectedAction();
            this.stateController = stateController;
            this.article = article;
            BindingContext = this.article;
            GetArticle();

            if (imageView.Source == null)
            {
                imageView.IsVisible = false;
                imageCaptionLabel.IsVisible = false;
            }


        }

        private async void GetArticle()
        {
            article = await this.stateController.getArticleDetails(article);
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

    }
}
