using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views
{
    public partial class MasterDetailView : MasterDetailPage
    {
        public MasterDetailView()
        {
            InitializeComponent();
            Detail = new NavigationPage(new FrontPageView());
        }

        private void FrontPageAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new FrontPageView());
            IsPresented = false;
        }

        private void AllArticlesAction(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AllArticlesView());
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
