using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views.Cells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateTimeCell : MR.Gestures.ViewCell
	{
        private StateController stateController;
        private ContentPage page;

        public DateTimeCell (StateController stateController, ContentPage page)
		{
			InitializeComponent ();
            this.stateController = stateController;
            this.page = page;

        }

        private void TappedGesture(object sender, MR.Gestures.TapEventArgs e)
        {
            Article article = (Article)BindingContext;
            CellGestures.TappedGesture(this.page, this.stateController, article);
        }

        private void LongPressingGesture(object sender, MR.Gestures.LongPressEventArgs e)
        {
            Article article = (Article)BindingContext;
            CellGestures.LongPressingGesture(this.page, this.stateController, article);
        }
    }
}
