using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavedArticlesView : ContentPage, INotifyPropertyChanged
	{
        private ObservableCollection<Article> savedArticles;
        public ObservableCollection<Article> SavedArticles
        {
            get { return savedArticles; }
            set
            {
                if(savedArticles == value) { return; }
                savedArticles = value;
                Notify("SavedArticles");
            }
        }

        private StateController stateController;

        public SavedArticlesView(StateController stateController)
		{
			InitializeComponent();

            listView.ItemTemplate = new SavedArticlesTemplateSelector(stateController, this);

            DisableItemSelectedAction();
            BindingContext = this;
            this.stateController = stateController;
            this.SavedArticles = this.stateController.SavedArticles;
        }

        /// <summary>
        /// We dont want the user to be able to select the articles. Its on by default, so this method is nessecary to counter that.
        /// </summary>
        private void DisableItemSelectedAction()
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        //Because INotifyProportyChanged is implemented
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
