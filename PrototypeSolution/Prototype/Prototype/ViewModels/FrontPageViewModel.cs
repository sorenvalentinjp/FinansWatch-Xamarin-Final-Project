using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class FrontPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IList<ArticleViewModel> _articles;
        public IList<ArticleViewModel> Articles
        {
            get { return _articles; }
            set
            {
                if (_articles == value) { return; }
                _articles = value;
                Notify("Articles");
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing == value) { return; }
                _isRefreshing = value;
                Notify("IsRefreshing");
            }
        }

        private DataTemplate _dateTemplate;
        public DataTemplate DataTemplate
        {
            get { return _dateTemplate; }
            set
            {
                if (_dateTemplate == value) { return; }
                _dateTemplate = value;
                Notify("DataTemplate");
            }
        }

        private readonly StateController _stateController;

        public FrontPageViewModel(StateController stateController)
        {
            this._stateController = stateController;
                        
            IsRefreshing = false;
            this._stateController.ArticleController.IsRefreshingFrontPage += IsRefreshingChanged;

            //bucket1 event
            _stateController.Bucket1IsReady += Bucket1IsReady;

            DataTemplate = new SectionTemplateSelector(_stateController);

            //
            if(_stateController.FrontPageArticles == null)
            {
                _stateController.GetBucket1();
            }
            else
            {
                Bucket1IsReady(_stateController.FrontPageArticles);
            }
        }

        private void Bucket1IsReady(IList<Article> newArticles)
        {
            IList<ArticleViewModel> articles = new List<ArticleViewModel>();
            foreach (var article in newArticles)
            {
                articles.Add(new ArticleViewModel(_stateController, article));
            }
            Articles = articles;
        }

        public void GetBucket2()
        {
            _stateController.GetBucket2();
        }

        //IsRefreshing is used to display an 'busy' icon while the listview is refreshing its content.
        private void IsRefreshingChanged(bool isRefreshing)
        {
            IsRefreshing = isRefreshing;
        }

        /// <summary>
        /// When the user refreshed the view, the corresponding method in the statecontroller is called.
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IList<Article> refreshedArticles = await _stateController.RefreshFrontPage();
                    Bucket1IsReady(refreshedArticles);
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
