using Prototype.ModelControllers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailView : MasterDetailPage
    {

        private Page frontPageView;
        private Page savedArticlesView;
        private Page allArticlesView;

        public StateController StateController { get; set; }

        public MasterDetailView(StateController stateController)
        {
            InitializeComponent();
            this.StateController = stateController;

            this.frontPageView = new FrontPageView(this.StateController);

            Detail = this.frontPageView;
        }

        private void FrontPageAction(object sender, EventArgs e)
        {
            Detail = this.frontPageView;
            IsPresented = false;
        }

        private void AllArticlesAction(object sender, EventArgs e)
        {
            if (this.allArticlesView == null)
                this.allArticlesView = new AllArticlesView(StateController);
            Detail = allArticlesView;
            IsPresented = false;
        }

        private void SavedArticlesAction(object sender, EventArgs e)
        {
            if(this.savedArticlesView == null)
                this.savedArticlesView = new SavedArticlesView(this.StateController);

            Detail = savedArticlesView;
            IsPresented = false;
        }

        private void SectionAction(object sender, EventArgs e)
        {
            Detail = new SectionView();
            IsPresented = false;
        }

        private void LoginAction(object sender, EventArgs e)
        {
            Detail = new LoginView();
            IsPresented = false;
        }

        private void SearchArticlesAction(object sender, EventArgs e)
        {
            Detail = new SearchArticlesView();
            IsPresented = false;
        }
    }
}
