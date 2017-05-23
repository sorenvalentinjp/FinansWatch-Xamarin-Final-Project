using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class SavedArticlesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StateController _stateController;

        private ObservableCollection<ArticleViewModel> _savedArticles;
        public ObservableCollection<ArticleViewModel> SavedArticles
        {
            get { return _savedArticles; }
            set
            {
                if (_savedArticles == value) { return; }
                _savedArticles = value;
                Notify("SavedArticles");
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

        public SavedArticlesViewModel(StateController stateController)
        {
            this._stateController = stateController;
            this.SavedArticles = GetSavedArticles(); //to populate the list when the view loads
            _stateController.ArticleController.SavedArticles.CollectionChanged += SavedArticles_CollectionChanged;
            this.DataTemplate = new SavedArticlesTemplateSelector(_stateController);
            this._stateController.LoginController.LoginEventSucceeded += LoginEvent;
        }

        /// <summary>
        /// If the user just logged in, recalculate if the article should display as locked
        /// </summary>
        /// <param name="subscriber"></param>
        private void LoginEvent(Subscriber subscriber)
        {
            foreach (var articleViewModel in SavedArticles)
            {
                articleViewModel.CalculateIfArticleShouldBeLocked();
            }
        }

        /// <summary>
        /// Every time the ArticleController's SavedArticles change, this event is invoked.
        /// This way we update the list stored in SavedArticlesViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavedArticles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.SavedArticles = GetSavedArticles();
        }

        /// <summary>
        /// Gets the saved articles stored in the ArticleController and creates an ArticleViewModel for each one.
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ArticleViewModel> GetSavedArticles()
        {
            ObservableCollection<ArticleViewModel> toReturn = new ObservableCollection<ArticleViewModel>();
            foreach (var article in _stateController.ArticleController.SavedArticles)
            {
                toReturn.Add(new ArticleViewModel(_stateController, article));
            }
                
            return toReturn;
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
