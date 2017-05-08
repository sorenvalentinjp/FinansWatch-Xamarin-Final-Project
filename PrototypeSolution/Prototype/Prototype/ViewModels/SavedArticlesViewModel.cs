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
            //because our statecontroller's SavedArticles property contains Article objects, we cant reference that collection directly (we use ArticleViewModels here). Therefore we mus t create this event listener to get notified when the collection changes
            _stateController.SavedArticles.CollectionChanged += SavedArticles_CollectionChanged;
            this.DataTemplate = new SavedArticlesTemplateSelector(_stateController);
        }

        private void SavedArticles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.SavedArticles = GetSavedArticles();
        }

        //Helper method to get the statecontroller's SavedArticles
        private ObservableCollection<ArticleViewModel> GetSavedArticles()
        {
            ObservableCollection<ArticleViewModel> toReturn = new ObservableCollection<ArticleViewModel>();
            foreach (var article in _stateController.SavedArticles)
                toReturn.Add(new ArticleViewModel(_stateController, article));
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
