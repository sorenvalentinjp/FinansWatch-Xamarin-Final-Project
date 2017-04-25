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

        public AllArticlesView (StateController stateController)
		{
			InitializeComponent ();
            this.StateController = stateController;
		}


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ArticleView(StateController, Article));
        }
        }
}
