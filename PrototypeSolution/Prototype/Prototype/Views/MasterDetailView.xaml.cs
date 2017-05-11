using Prototype.ModelControllers;
using System;
using Prototype.Models;
using Prototype.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailView : MasterDetailPage
	{
	    private readonly StateController _stateController;
        public MasterDetailView(StateController stateController)
        {            
            InitializeComponent();

            _stateController = stateController;

            BindingContext = new MasterDetailViewModel(stateController, this);
        }

    }
}
