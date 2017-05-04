using Prototype.ModelControllers;
using Prototype.ViewModels;
using Prototype.Views.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrontPageView : ContentPage
    {

        public FrontPageView(StateController stateController)
		{
			InitializeComponent();

            var viewModel = new FrontPageViewModel(stateController, this);

		    BindingContext = viewModel;
            
            ListViewHelper.DisableItemSelectedAction(listView);
        }     
    }
}
