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
	public partial class FrontPageView : ContentPage
	{
		public FrontPageView ()
		{
			InitializeComponent ();
            setLabel();
		}

        public async void setLabel()
        {
            StateController stateController = new StateController();
            List<Article> articles = await stateController.getFrontPageArticles();
            label1.Text = articles[0].Title;
        }
	}
}
