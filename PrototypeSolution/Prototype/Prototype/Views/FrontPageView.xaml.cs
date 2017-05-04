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
            FrontPageViewModel vm = new FrontPageViewModel(stateController, this);
		    BindingContext = vm;
            ListViewHelper.DisableItemSelectedAction(listView);
        }     
    }
}