using Prototype.ModelControllers;
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

        private readonly Page _frontPageView;
        private Page _savedArticlesView;
        private Page _allArticlesView;

        public StateController StateController { get; set; }

        public MasterDetailView(StateController stateController)
        {
            InitializeComponent();
            this.StateController = stateController;           
            this._frontPageView = new FrontPageView(this.StateController);
            Detail = this._frontPageView;
        }

        private void FrontPageAction(object sender, EventArgs e)
        {
            Detail = this._frontPageView;
            IsPresented = false;
        }

        private void AllArticlesAction(object sender, EventArgs e)
        {
            if (this._allArticlesView == null)
                this._allArticlesView = new AllArticlesView(StateController);
            Detail = _allArticlesView;
            IsPresented = false;
        }

        private void SavedArticlesAction(object sender, EventArgs e)
        {
            if(this._savedArticlesView == null)
                this._savedArticlesView = new SavedArticlesView(this.StateController);

            Detail = _savedArticlesView;
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
