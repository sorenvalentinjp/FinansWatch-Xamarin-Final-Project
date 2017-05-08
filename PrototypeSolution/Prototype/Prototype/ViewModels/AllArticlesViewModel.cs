using Prototype.ModelControllers;
using Prototype.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Prototype.Views.Cells;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class AllArticlesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StateController _stateController;

        private IList<Grouping<string, ArticleViewModel>> _grouped;
        public IList<Grouping<string, ArticleViewModel>> Grouped
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

        private DataTemplate _dataTemplateGroupHeader;
        public DataTemplate DataTemplateGroupHeader
        {
            get { return _dataTemplateGroupHeader; }
            set
            {
                if (_dataTemplateGroupHeader == value) { return; }
                _dataTemplateGroupHeader = value;
                Notify("DataTemplateGroupHeader");
            }
        }

        private DataTemplate _dataTemplate;
        public DataTemplate DataTemplate
        {
            get { return _dataTemplate; }
            set
            {
                if (_dataTemplate == value) { return; }
                _dataTemplate = value;
                Notify("DataTemplate");
            }
        }

        public AllArticlesViewModel(StateController stateController)
        {
            this._stateController = stateController;
            IsRefreshing = false;
            this._stateController.ArticleController.IsRefreshingLatestArticles += IsRefreshingChanged;
            this._stateController.ArticleController.LatestArticlesAreReady += LatestArticlesAreReady;
            this._stateController.ArticleController.GetLatestArticlesAsync();

            this.DataTemplate = new DataTemplate(() => new DateTimeCell(stateController));
            this.DataTemplateGroupHeader = new DataTemplate(() => new DateTimeCellGroupHeader());

        }



        private void LatestArticlesAreReady(List<Grouping<string, ArticleViewModel>> groupedArticles)
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
