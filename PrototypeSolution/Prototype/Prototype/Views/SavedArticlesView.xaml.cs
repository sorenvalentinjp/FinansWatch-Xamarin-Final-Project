using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.ViewModels;
using Prototype.Views.Helpers;
using Prototype.Views.TemplateSelectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavedArticlesView : ContentPage, INotifyPropertyChanged
	{
	    public SavedArticlesView(StateController stateController)
		{
		    InitializeComponent();
            SavedArticlesViewModel vm = new SavedArticlesViewModel(stateController, this);
            BindingContext = vm;
            ListViewHelper.DisableItemSelectedAction(listView);
        }
    }
}