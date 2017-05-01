using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Prototype
{
	public partial class App : Application
	{
        public StateController StateController { get; set; }

        public App ()
		{
			InitializeComponent();

            this.StateController = new StateController();

            MainPage = new Views.MasterDetailView(this.StateController);
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
