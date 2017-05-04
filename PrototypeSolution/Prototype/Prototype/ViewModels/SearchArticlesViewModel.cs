using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class SearchArticlesViewModel
    {
        private readonly StateController _stateController;

        public SearchArticlesViewModel(StateController stateController, Page page)
        {
            this._stateController = stateController;
        }
    }
}
