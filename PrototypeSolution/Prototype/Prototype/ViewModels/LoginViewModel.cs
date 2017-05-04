using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class LoginViewModel
    {
        private readonly StateController _stateController;

        public LoginViewModel(StateController stateController)
        {
            this._stateController = stateController;
        }
    }
}
