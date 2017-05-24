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

            if(Device.RuntimePlatform == "iOS")
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    this.Height = App.ScreenHeight * 0.6;
                }
                else if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.Height = App.ScreenHeight * 0.8;
                }
            }
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
