using Prototype.ModelControllers;
using Prototype.ViewModels;
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
	public partial class LoginView : ContentPage
	{
		public LoginView(LoginViewModel viewModel)
		{
			InitializeComponent();

            BindingContext = viewModel;
		}

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {

        }
    }
}
