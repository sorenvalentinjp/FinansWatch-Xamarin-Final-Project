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
	public partial class SearchArticlesView : ContentPage
	{
		public SearchArticlesView(StateController stateController)
		{
			InitializeComponent();
            SearchArticlesViewModel vm = new SearchArticlesViewModel(stateController, this);
            BindingContext = vm;
		}
	}
}
