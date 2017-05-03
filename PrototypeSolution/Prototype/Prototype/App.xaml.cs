using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Prototype
{
	public partial class App : Application
	{
        public static INavigation Navigation { get; set; }

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new MasterDetailView(new StateController()));
            NavigationPage.SetBackButtonTitle(MainPage, "");


            if (App.Navigation == null)
            {
                App.Navigation = MainPage.Navigation;
            }
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
    }
}
