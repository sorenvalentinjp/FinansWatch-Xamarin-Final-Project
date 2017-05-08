using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Prototype.ViewModels;
using Xamarin.Forms;
using Prototype.Database;

namespace Prototype
{
    public partial class App : Application
    {
        public static INavigation Navigation { get; set; }
        private static StateController _stateController;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (Application.Current.Properties.ContainsKey("stateController"))
                _stateController = LocalStorage.DeserializeFromJson<StateController>(Application.Current.Properties["stateController"].ToString());
            if (_stateController == null)
                _stateController = new StateController();

            MainPage = new NavigationPage(new MasterDetailView(_stateController));

            NavigationPage.SetBackButtonTitle(MainPage, "");

            if (App.Navigation == null)
                App.Navigation = MainPage.Navigation;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Application.Current.Properties["stateController"] = LocalStorage.SerializeToJson(_stateController);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            if (Application.Current.Properties.ContainsKey("stateController"))
                _stateController = LocalStorage.DeserializeFromJson<StateController>(Application.Current.Properties["stateController"].ToString());
        }

        public static bool IsSubscriberLoggedIn()
        {
            return _stateController.Subscriber != null;
        }
    }
}
