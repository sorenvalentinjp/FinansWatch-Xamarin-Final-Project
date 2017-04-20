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
            
        }
    }
}
