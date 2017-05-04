﻿using Prototype.ModelControllers;
using System;
using Prototype.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailView : MasterDetailPage
    {
        public MasterDetailView(StateController stateController)
        {
            InitializeComponent();

            BindingContext = new MasterDetailViewModel(stateController, this);
        }
    }
}
