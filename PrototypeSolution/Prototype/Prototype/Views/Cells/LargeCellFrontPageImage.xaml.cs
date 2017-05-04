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
	public partial class LargeCellFrontPageImage : MR.Gestures.ViewCell
	{
        private readonly StateController _stateController;
        private readonly ContentPage _page;

        public LargeCellFrontPageImage(StateController stateController, ContentPage page)
		{
			InitializeComponent();
            this._stateController = stateController;
            this._page = page;
        }

        private void TappedGesture(object sender, MR.Gestures.TapEventArgs e)
        {
            Article article = (Article)BindingContext;
            CellGestures.TappedGesture(this._page, this._stateController, article);
        }

        private void LongPressingGesture(object sender, MR.Gestures.LongPressEventArgs e)
        {
            Article article = (Article)BindingContext;
            CellGestures.LongPressingGesture(this._page, this._stateController, article);
        }
    }
}
