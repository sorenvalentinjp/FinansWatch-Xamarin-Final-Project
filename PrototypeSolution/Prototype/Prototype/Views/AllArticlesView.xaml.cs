using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllArticlesView : ContentPage
	{
        private StateController StateController;
        private Article Article;

        public AllArticlesView (StateController stateController)
		{
			InitializeComponent ();
            this.StateController = stateController;
            setLabel();
		}

        public async void setLabel()
        {
            StateController st = new StateController();
            List<Article> articles = await st.getFrontPageArticles();
            Article = await st.getArticleDetails(articles[0]);
            label1.Text = Article.PublishedDate.ToString() + ", " + Article.Title;
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ArticleView(StateController, Article));
        }
        }
}
