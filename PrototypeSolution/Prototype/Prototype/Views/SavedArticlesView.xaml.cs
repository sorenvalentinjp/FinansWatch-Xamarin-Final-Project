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
	    public SavedArticlesView(StateController stateController)
		{
		    InitializeComponent();
            SavedArticlesViewModel vm = new SavedArticlesViewModel(stateController);
            BindingContext = vm;
            ListViewHelper.DisableItemSelectedAction(listView);
        }
    }
}