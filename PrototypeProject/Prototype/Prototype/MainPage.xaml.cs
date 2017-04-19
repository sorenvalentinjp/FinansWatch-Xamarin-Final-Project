using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype
{
	public partial class MainPage : ContentPage
	{
        private ArticleController articleController;
		public MainPage()
		{
			InitializeComponent();
            articleController = new ArticleController();
            setLabelText();
            
		}

        async public void setLabelText()
        {
            List<Article> articles = await articleController.getFrontPageArticles();

            label1.Text = articles[0].title;
        }
	}
}
