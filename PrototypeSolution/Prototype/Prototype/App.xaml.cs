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
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
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
            {
                _stateController = LocalStorage.DeserializeFromJson<StateController>(Application.Current.Properties["stateController"].ToString());
            }
                
            if (_stateController == null)
            {
                _stateController = new StateController();

                //Create sections
                _stateController.ArticleController.Sections.Add(new Section("FORSIDE", "finanswatch/content/frontpagearticles?max=30"));
                _stateController.ArticleController.Sections.Add(new Section("PENGEINSTITUTTER", "finanswatch/content/latest?hoursago=500&max=30&section=fw_finansnyt_penge"));
                _stateController.ArticleController.Sections.Add(new Section("FORSIKRINGER", "finanswatch/content/latest?hoursago=500&max=30&section=fw_forsikring"));
                _stateController.ArticleController.Sections.Add(new Section("PENSION", "finanswatch/content/latest?hoursago=500&max=30&section=fw_pension"));
                _stateController.ArticleController.Sections.Add(new Section("REALKREDIT", "finanswatch/content/latest?hoursago=500&max=30&section=fw_finansnyt_real"));
                _stateController.ArticleController.Sections.Add(new Section("NAVNE OG JOB", "finanswatch/content/latest?hoursago=500&max=30&section=fw_finansliv"));
                _stateController.ArticleController.Sections.Add(new Section("KLUMMER", "finanswatch/content/latest?hoursago=500&max=30&section=fw_klumme"));
            }
               
            MainPage = new NavigationPage(new MasterDetailView(_stateController));

            NavigationPage.SetBackButtonTitle(MainPage, "");

            if (App.Navigation == null)
            {
                App.Navigation = MainPage.Navigation;
            }
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
            {
                _stateController = LocalStorage.DeserializeFromJson<StateController>(Application.Current.Properties["stateController"].ToString());
                _stateController.ArticleController.LocalStorageLoaded();
            }
        }

        public static bool IsSubscriberLoggedIn()
        {
            return _stateController.LoginController.Subscriber != null;
        }
    }
}
