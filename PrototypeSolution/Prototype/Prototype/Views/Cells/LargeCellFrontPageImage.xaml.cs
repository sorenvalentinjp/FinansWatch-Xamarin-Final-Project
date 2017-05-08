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
	public partial class LargeCellFrontPageImage : MR.Gestures.ViewCell
	{
        private readonly StateController _stateController;

        public LargeCellFrontPageImage(StateController stateController)
		{
			InitializeComponent();
            this._stateController = stateController;
        }

        private void TappedGesture(object sender, MR.Gestures.TapEventArgs e)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)BindingContext;
            CellGestures.TappedGesture(this._stateController, articleViewModel);
        }

        private void LongPressingGesture(object sender, MR.Gestures.LongPressEventArgs e)
        {
            Article article = (Article)BindingContext;
            CellGestures.LongPressingGesture(this._stateController, article);
        }
    }
}
