using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Prototype
{
	public partial class App : Application
	{

        public App ()
		{
			InitializeComponent();

            StateController stateController = new StateController();

            MainPage = new Views.MasterDetailView(stateController);
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
