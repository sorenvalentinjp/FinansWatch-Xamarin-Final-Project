using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class SectionViewModel
    {
        private readonly StateController _stateController;

        public SectionViewModel(StateController stateController)
        {
            _stateController = stateController;
        }
    }
}
