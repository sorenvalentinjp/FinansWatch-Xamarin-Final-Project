using System;
using System.Collections.Generic;
using System.Text;
using Prototype.ModelControllers;
using Prototype.Views;
using Xamarin.Forms;
using System.Windows.Input;
using Prototype.Models;
using System.ComponentModel;
using Prototype.Views.Helpers;
using System.Linq;

namespace Prototype.ViewModels
{
    public class MasterDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Page _savedArticlesView;
        private Page _allArticlesView;
        private LoginView _loginView;
        private Page _searchArticlesView;
        private Page _sectionSelectorView;
        private readonly StateController _stateController;

        private IList<SectionView> _sectionViews;

        private MasterDetailPage _masterDetail;
        public MasterDetailPage MasterDetail
        {
            get { return _masterDetail; }
            set
            {
                if (_masterDetail == value) { return; }
                _masterDetail = value;
                Notify("MasterDetail");
            }
        }

        //We want to direct the user back to the latest visited view after login, hence this variable is needed.
        private Page _latestVisitedView;
        //When the user is logged in, the text of the login button should be changed automatically
        private string _loginButtonText;
        public string LoginButtonText
        {
            get { return _loginButtonText; }
            set
            {
                if (_loginButtonText == value) { return; }
                _loginButtonText = value;
                Notify("LoginButtonText");
            }
        }

        public MasterDetailViewModel(StateController stateController, MasterDetailPage masterDetail, Section frontPageSection)
        {
            _stateController = stateController;
            _stateController.LoginController.LoginEventSucceeded += LoginSucceeded;
            ToolBarExtension.AllArticlesShortcutActionOccured += AllArticlesShortcutActionOccured;
            _masterDetail = masterDetail;

            _sectionViews = new List<SectionView>();

            //Set frontpageSection
            SectionViewModel frontPageSectionViewModel = new SectionViewModel(stateController, frontPageSection);
            SectionView frontPageSectionView = new SectionView(frontPageSectionViewModel);
            _sectionViews.Add(frontPageSectionView);

            _masterDetail.Detail = frontPageSectionView;
            SetLogInButtonText();

            //If this is the first time the app is loaded, start bucket downloads and build the model layer
            if (frontPageSection.Articles == null)
            {
                InitBucketsDownload();
            }
            else
            {
                frontPageSectionViewModel.BucketIsReady();
            }
            
        }

        /// <summary>
        /// Initiates download of bucket 1 and 2.
        /// </summary>
        private async void InitBucketsDownload()
        {
            await _stateController.GetBucket1();
            _stateController.GetBucket2();
        }

        /// <summary>
        /// Downloads bucket 2.
        /// </summary>
        public void GetBucket2()
        {
            _stateController.GetBucket2();
        }

        /// <summary>
        /// This event fires when the user logs in successfully. The detail's view is then set to direct the user back to the last visisted view.
        /// </summary>
        /// <param name="subscriber"></param>
        private void LoginSucceeded(Subscriber subscriber)
        {
            //Set login button text to "LOG UD"
            SetLogInButtonText();

            // If the user clicked log in from an article, pop and go back to the article
            if (App.Navigation.NavigationStack.Count > 1)
            {
                App.Navigation.PopAsync();
            }
            //If the user is logging in from the master menu go back to the latest visisted view
            else
            {
                if(subscriber != null)
                {
                    _masterDetail.Detail = _latestVisitedView;
                }

                _masterDetail.IsPresented = false;
            }
        }

        /// <summary>
        /// This event fires when the user taps the shorcut button placed in the navigation bar to show LatestArticlesView
        /// </summary>
        private void AllArticlesShortcutActionOccured()
        {
            if (this._allArticlesView == null)
                this._allArticlesView = new LatestArticlesView(new LatestArticlesViewModel(this._stateController));

            _masterDetail.Detail = _allArticlesView;
        }

        /// <summary>
        /// Sets the text of the log in button depending on if the subscriber is logged in or not.
        /// </summary>
        private void SetLogInButtonText()
        {
            if (_stateController.LoginController.Subscriber == null)
                LoginButtonText = "LOG IND";
            else
                LoginButtonText = "LOG UD";
        }

        /// <summary>
        /// Directs the user to LatestArticlesView
        /// </summary>
        public ICommand AllArticlesAction
        {
            get
            {
                return new Command(() =>
                {
                    if (this._allArticlesView == null)
                        this._allArticlesView = new LatestArticlesView(new LatestArticlesViewModel(this._stateController));

                    _masterDetail.Detail = _allArticlesView;
                    _masterDetail.IsPresented = false;
                });
            }
        }

        /// <summary>
        /// Directs the user to SavedArticlesView
        /// </summary>
        public ICommand SavedArticlesAction
        {
            get
            {
                return new Command(() =>
                {
                    if (_savedArticlesView == null)
                    {
                        _savedArticlesView = new SavedArticlesView(new SavedArticlesViewModel(_stateController));
                    }

                    _masterDetail.Detail = _savedArticlesView;

                    _masterDetail.IsPresented = false;
                });
            }
        }

        /// <summary>
        /// This method is used in MasterDetailView.xaml.cs to create a button for each section
        /// Directs the user to the SectionView
        /// </summary>
        /// <param name="section">The section to display</param>
        /// <returns></returns>
        public SectionView SectionViewAction(Section section)
        {
            //If a view for this section already exists, save it in var so we can navigate directly to it.
            SectionView sectionView = _sectionViews.FirstOrDefault(
                (s) => ((SectionViewModel) s.BindingContext).Section.Equals(section));

            if (sectionView == null)
            {
                //Create new sectionview with this section and navigate
                sectionView = new SectionView(new SectionViewModel(_stateController, section));
                this._sectionViews.Add(sectionView);
            }

            //Navigate to section view
            _masterDetail.Detail = sectionView;
            _masterDetail.IsPresented = false;

            return sectionView;
        }

        /// <summary>
        /// Contains logged to log in (a new view is presented) or to log the user out right away
        /// </summary>
        public ICommand LoginAction
        {
            get
            {
                return new Command(async () =>
                {
                    //If logget out, then present the log in view
                    if (LoginButtonText == "LOG IND")
                    {
                        _loginView = new LoginView(new LoginViewModel(_stateController));

                        _latestVisitedView = _masterDetail.Detail;
                        _masterDetail.Detail = this._loginView;
                        _masterDetail.IsPresented = false;
                    }

                    //Else log the user out after he confirms the log out action
                    else
                    {
                        _masterDetail.IsPresented = false;
                        var answer = await App.Navigation.NavigationStack.First().DisplayAlert("", "Er du sikker på, at du vil logge ud?", "Log ud", "Afbryd");

                        if (answer)
                        {
                            _stateController.LoginController.LogoutEventAction(); //this method fires the logout event which is then notifying ArticleViewModel
                            SetLogInButtonText();
                            //Clear entrys in loginview
                            if (_loginView != null)
                            {
                                _loginView.ClearEntrys();
                            }                           
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Directs the user to SearchArticlesView
        /// </summary>
        public ICommand SearchArticlesAction
        {
            get
            {
                return new Command(() =>
                {
                    if (this._searchArticlesView == null)
                        this._searchArticlesView = new SearchArticlesView(new SearchArticlesViewModel(_stateController));

                    _masterDetail.Detail = this._searchArticlesView;
                    _masterDetail.IsPresented = false;
                });
            }
        }

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}