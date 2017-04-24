using Prototype.ModelControllers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailView : MasterDetailPage
    {
        public StateController StateController { get; set; }

        public MasterDetailView(StateController stateController)
        {
            InitializeComponent();
            this.StateController = stateController;
            this.StateController.getFrontPageArticles();
            Detail = new NavigationPage(new FrontPageView(this.StateController));
        }

        private void FrontPageAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new FrontPageView(this.StateController));
            IsPresented = false;
        }

        private void AllArticlesAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AllArticlesView(StateController));
            IsPresented = false;
        }

        private void SavedArticlesAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new SavedArticlesView());
            IsPresented = false;
        }

        private void SectionAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new SectionView());
            IsPresented = false;
        }

        private void LoginAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new LoginView());
            IsPresented = false;
        }

        private void SearchArticlesAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new SearchArticlesView());
            IsPresented = false;
        }
    }
}
