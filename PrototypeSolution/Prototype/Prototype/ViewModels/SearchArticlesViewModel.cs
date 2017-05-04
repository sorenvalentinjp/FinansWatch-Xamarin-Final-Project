using Prototype.ModelControllers;

namespace Prototype.ViewModels
{
    public class SearchArticlesViewModel
    {
        private readonly StateController _stateController;

        public SearchArticlesViewModel(StateController stateController)
        {
            this._stateController = stateController;
        }
    }
}
