using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views.Cells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateTimeCell : MR.Gestures.ViewCell
	{
        private readonly StateController _stateController;

        public DateTimeCell (StateController stateController)
		{
			InitializeComponent ();
            this._stateController = stateController;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                this.Height = App.ScreenHeight / 10;
            }
            else if (Device.Idiom == TargetIdiom.Phone)
            {
                this.Height = App.ScreenHeight / 7;
            }
        }

        private void TappedGesture(object sender, MR.Gestures.TapEventArgs e)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)BindingContext;
            CellGestures.TappedGesture(this._stateController, articleViewModel);            
        }

        private void LongPressingGesture(object sender, MR.Gestures.LongPressEventArgs e)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)BindingContext;
            CellGestures.LongPressingGesture(this._stateController, articleViewModel);
        }
    }
}
