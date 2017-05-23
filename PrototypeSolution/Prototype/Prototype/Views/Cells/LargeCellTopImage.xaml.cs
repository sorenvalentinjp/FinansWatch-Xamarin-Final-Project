using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.ViewModels;
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
	public partial class LargeCellTopImage : MR.Gestures.ViewCell
	{
        private readonly StateController _stateController;

        public LargeCellTopImage(StateController stateController)
		{
			InitializeComponent();
            this._stateController = stateController;
        }

        private void TappedGesture(object sender, MR.Gestures.TapEventArgs e)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)BindingContext;
            CellGestures.TappedGesture(_stateController, articleViewModel);
        }

        private void LongPressingGesture(object sender, MR.Gestures.LongPressEventArgs e)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)BindingContext;
            CellGestures.LongPressingGesture(_stateController, articleViewModel);
        }
    }
}
