using Prototype.ModelControllers;
using Prototype.ViewModels;
using Prototype.Views.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavedArticlesView : ContentPage
	{
	    public SavedArticlesView(SavedArticlesViewModel viewModel)
		{
		    InitializeComponent();

		    BindingContext = viewModel;

            ListViewHelper.DisableItemSelectedAction(listView);
        }
    }
}