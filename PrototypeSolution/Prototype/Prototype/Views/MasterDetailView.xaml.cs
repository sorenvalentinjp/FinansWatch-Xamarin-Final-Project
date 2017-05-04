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
        private Page _sectionView;
        private Page _allArticlesView;
        private Page _loginView;
        private Page _searchArticlesView;

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

            Detail = this._savedArticlesView;
            IsPresented = false;
        }

        private void SectionAction(object sender, EventArgs e)
        {
            if(this._sectionView == null)
            {
                this._sectionView = new SectionView(this.StateController);
            }
            Detail = this._sectionView;
            IsPresented = false;
        }

        private void LoginAction(object sender, EventArgs e)
        {
            if(this._loginView == null)
            {
                this._loginView = new LoginView(this.StateController);
            }
            Detail = this._loginView;
            IsPresented = false;
        }

        private void SearchArticlesAction(object sender, EventArgs e)
        {
            if(this._searchArticlesView == null)
            {
                this._searchArticlesView = new SearchArticlesView(this.StateController);
            }
            Detail = this._searchArticlesView;
            IsPresented = false;
        }
    }
}
