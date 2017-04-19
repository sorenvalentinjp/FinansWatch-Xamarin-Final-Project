using Prototype.ModelControllers;
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
            label1.Text = await articleController.getFrontPageArticles();
        }
	}
}
