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
        public AllArticlesView(StateController stateController)
        {
			InitializeComponent();
            AllArticlesViewModel vm = new AllArticlesViewModel(stateController, this);
            listView.ItemTemplate = new DataTemplate(() => new DateTimeCell(stateController, this));
            listView.GroupHeaderTemplate = new DataTemplate(() => new DateTimeCellGroupHeader());
            BindingContext = vm;
            ListViewHelper.DisableItemSelectedAction(listView);
        }
    }
}