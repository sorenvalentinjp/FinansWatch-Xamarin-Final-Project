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

        private ObservableCollection<Article> _savedArticles;
        public ObservableCollection<Article> SavedArticles
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

        public SavedArticlesViewModel(StateController stateController, Page page)
        {
            this._stateController = stateController;
            this.SavedArticles = this._stateController.SavedArticles;
            this.DataTemplate = new SavedArticlesTemplateSelector(this._stateController, page);
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
