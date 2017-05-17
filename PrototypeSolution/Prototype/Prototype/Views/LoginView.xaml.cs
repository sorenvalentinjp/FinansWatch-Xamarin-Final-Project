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
	    private LoginViewModel _viewModel;
		public LoginView(LoginViewModel viewModel)
		{
			InitializeComponent();

		    _viewModel = viewModel;

            BindingContext = _viewModel;            
		}

	    public void ClearEntrys()
	    {
	        entryEmail.Text = "";
	        entryPassword.Text = "";
	    }

	    protected override void OnDisappearing()
	    {

	            _viewModel.UnsubscribeLoginEvents();  
	    }

	    protected override void OnAppearing()
	    {

	            _viewModel.SubscribeToLoginEvents();
	    }

    }
}
