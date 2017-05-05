using System;
using System.Collections.Generic;
using System.Text;
using Prototype.ModelControllers;
using Prototype.Views;
using Xamarin.Forms;
using System.Windows.Input;
using Prototype.Models;

namespace Prototype.ViewModels
{
    public class MasterDetailViewModel
    {
        private Page _frontPageView;
        private Page _savedArticlesView;
        private Page _sectionView;
        private Page _allArticlesView;
        private Page _loginView;
        private Page _searchArticlesView;
        private readonly StateController _stateController;
        private readonly MasterDetailPage _masterDetail;
        //We want to direct the user back to the latest visited view after login, hence this variable is needed.
        private Page _latestVisitedView;

        public MasterDetailViewModel(StateController stateController, MasterDetailPage masterDetail)
        {
            _stateController = stateController;
            _stateController.LoginController.LoginSucceeded += LoginSucceeded;
            _masterDetail = masterDetail;
            _frontPageView = new FrontPageView(new FrontPageViewModel(stateController));
            _masterDetail.Detail = _frontPageView;
        }

        //This event fires when the user logs in successfully. The detail's view is then set to direct the user back to the last visisted view.
        private void LoginSucceeded(Subscriber subscriber)
        {
            _masterDetail.Detail = _latestVisitedView;
            _masterDetail.IsPresented = false;
        }

        public ICommand FrontPageAction
        {
            get
            {
                return new Command(() =>
                {
                    _masterDetail.Detail = _frontPageView;
                    _masterDetail.IsPresented = false;
                });
            }
        }

        public ICommand AllArticlesAction
        {
            get
            {
                return new Command(() =>
                {
                    if (this._allArticlesView == null)
                        this._allArticlesView = new AllArticlesView(new AllArticlesViewModel(this._stateController));
                    _masterDetail.Detail = _allArticlesView;
                    _masterDetail.IsPresented = false;
                });
            }
        }

        public ICommand SavedArticlesAction
        {
            get
            {
                return new Command(() =>
                {
                    if (this._savedArticlesView == null)
                        this._savedArticlesView = new SavedArticlesView(new SavedArticlesViewModel(_stateController));

                    _masterDetail.Detail = this._savedArticlesView;
                    _masterDetail.IsPresented = false;
                });
            }
        }

        public ICommand SectionAction
        {
            get
            {
                return new Command(() =>
                {
                    if (this._sectionView == null)
                    {
                        this._sectionView = new SectionView(new SectionViewModel(_stateController));
                    }
                    _masterDetail.Detail = this._sectionView;
                    _masterDetail.IsPresented = false;
                });
            }
        }

        public ICommand LoginAction
        {
            get
            {
                return new Command(() =>
                {
                    if (this._loginView == null)
                    {
                        this._loginView = new LoginView(new LoginViewModel(_stateController));
                    }
                    _latestVisitedView = _masterDetail.Detail;
                    _masterDetail.Detail = this._loginView;
                    _masterDetail.IsPresented = false;
                });
            }
        }

        public ICommand SearchArticlesAction
        {
            get
            {
                return new Command(() =>
                {
                    if (this._searchArticlesView == null)
                    {
                        this._searchArticlesView = new SearchArticlesView(new SearchArticlesViewModel(_stateController));
                    }
                    _masterDetail.Detail = this._searchArticlesView;
                    _masterDetail.IsPresented = false;
                });
            }
        }
    }
}
