using Prototype.ModelControllers;
using Prototype.ViewModels;
using Prototype.Views.Cells;
using Prototype.Views.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllArticlesView : ContentPage
    {
        public AllArticlesView(AllArticlesViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();

            ListViewHelper.DisableItemSelectedAction(listView);
        }
    }
}