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
    public partial class MediumCellTopImage : MR.Gestures.ViewCell
    {
        private readonly StateController _stateController;

        public MediumCellTopImage(StateController stateController)
        {
            InitializeComponent();
            this._stateController = stateController;
        }

        private void TappedGesture(object sender, MR.Gestures.TapEventArgs e)
        {
            Article article = (Article)BindingContext;
            CellGestures.TappedGesture(this._stateController, article);
        }

        private void LongPressingGesture(object sender, MR.Gestures.LongPressEventArgs e)
        {
            Article article = (Article)BindingContext;
            CellGestures.LongPressingGesture(this._stateController, article);
        }
    }
}
