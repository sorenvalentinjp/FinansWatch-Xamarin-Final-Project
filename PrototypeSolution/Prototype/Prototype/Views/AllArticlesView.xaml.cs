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
		public AllArticlesView ()
		{
			InitializeComponent ();
            setLabel();
		}

        public async void setLabel()
        {
            StateController st = new StateController();
            List<Article> articles = await st.getFrontPageArticles();
            Article art = await st.getArticleDetails(articles[0]);
            label1.Text = art.PublishedDate.ToString() + ", " + art.Title;
        }

	}
}
