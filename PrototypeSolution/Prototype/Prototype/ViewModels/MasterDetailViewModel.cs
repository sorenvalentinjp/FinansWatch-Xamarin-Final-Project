using System;
using System.Collections.Generic;
using System.Text;
using Prototype.ModelControllers;
using Prototype.Views;
using Xamarin.Forms;
using System.Windows.Input;

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


        public MasterDetailViewModel(StateController stateController, MasterDetailPage masterDetail)
        {
            _stateController = stateController;
            _masterDetail = masterDetail;
            _masterDetail.Detail = new FrontPageView(new FrontPageViewModel(stateController));
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
                        this._savedArticlesView = new SavedArticlesView(this._stateController);

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
                        this._sectionView = new SectionView(this._stateController);
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
                        this._loginView = new LoginView(this._stateController);
                    }
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
                        this._searchArticlesView = new SearchArticlesView(this._stateController);
                    }
                    _masterDetail.Detail = this._searchArticlesView;
                    _masterDetail.IsPresented = false;
                });
            }
        }
    }
}
