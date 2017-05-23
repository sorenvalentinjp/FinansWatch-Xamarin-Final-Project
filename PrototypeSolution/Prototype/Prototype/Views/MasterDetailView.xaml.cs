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

            Section frontPageSection = GetFrontPageSection();

            var viewModel = new MasterDetailViewModel(stateController, this, frontPageSection);

            //Set frontpagebutton to navigate to frontPageSection and set first pageload to frontpage section   
            FrontPageButton.Command = new Command(() =>
            {
                viewModel.SectionViewAction(frontPageSection);
            });

            BindingContext = viewModel;


            //Setting up btnSections to toggle the visibility of stacklayout containing the section buttons
            btnSections.Command = new Command(() =>
            {
                StackLayoutSectionButtons.IsVisible = !StackLayoutSectionButtons.IsVisible;
            });

            //Create a button for each section except the frontpage, and make a command that handles navigation on button press.
            foreach (var section in _stateController.ArticleController.Sections)
            {
                if (section != frontPageSection)
                {
                    StackLayoutSectionButtons.Children.Add(new MasterButton
                    {
                        Text = section.Name,
                        Command = new Command(() =>
                        {
                            //Create navigation inside the viewmodel
                            viewModel.SectionViewAction(section);
                        })
                    });
                }
            }
        }

        /// <summary>
        /// The first section in the statecontrollers Sections list, is always the frontpage section
        /// </summary>
        /// <returns></returns>
        private Section GetFrontPageSection()
        {
            return _stateController.ArticleController.Sections.FirstOrDefault();
        }
    }
}