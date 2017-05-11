using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Prototype.Models;
using Prototype.ViewModels;
using Prototype.Views.Components;
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

            var viewModel = new MasterDetailViewModel(stateController, this);

            BindingContext = viewModel;



            foreach (var section in _stateController.Sections)
            {
                StackLayoutButtons.Children.Add(new MasterButton
                {
                    Text = section.Name,
                    Command = new Command(() =>
                            {
                                viewModel.SectionAction(section);
                            })
                });
            }
        }
    }
}