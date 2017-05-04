using Prototype.ModelControllers;
using Prototype.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class AllArticlesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StateController _stateController;

        private IList<Grouping<string, Article>> _grouped;
        public IList<Grouping<string, Article>> Grouped
        {
            get { return _grouped; }
            set
            {
                if (_grouped == value) { return; }
                _grouped = value;
                Notify("Grouped");
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

        public AllArticlesViewModel(StateController stateController, Page page)
        {
            this._stateController = stateController;
            IsRefreshing = false;
            this._stateController.ArticleController.IsRefreshingLatestArticles += IsRefreshingChanged;
            this._stateController.ArticleController.LatestArticlesAreReady += LatestArticlesAreReady;
            this._stateController.ArticleController.GetLatestArticlesAsync();
        }

        private void LatestArticlesAreReady(List<Grouping<string, Article>> groupedArticles)
        {
            Grouped = groupedArticles;
        }

        private void IsRefreshingChanged(bool isRefreshing)
        {
            IsRefreshing = isRefreshing;
        }

        /// <summary>
        /// When the user refreshed the view, this.articles is updated.
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    this._stateController.ArticleController.GetLatestArticlesAsync();
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
