using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllArticlesView : ContentPage, INotifyPropertyChanged
    {
        private StateController StateController;

        private IList<Grouping<string, Article>> grouped;
        public IList<Grouping<string, Article>> Grouped
        {
            get { return grouped; }
            set
            {
                if (grouped == value) { return; }
                grouped = value;
                Notify("Grouped");
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing == value) { return; }
                isRefreshing = value;
                Notify("IsRefreshing");
            }
        }

        public AllArticlesView (StateController stateController)
		{
			InitializeComponent ();
            this.StateController = stateController;
            this.StateController.ArticleController.latestArticlesAreReady += ArticleController_latestArticlesAreReady;
            this.StateController.ArticleController.isRefreshing += ArticleController_isRefreshing;
            BindingContext = this;
            this.StateController.ArticleController.getLatestArticlesAsync();
            DisableItemSelectedAction();
        }

        private void DisableItemSelectedAction()
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        private void ArticleController_isRefreshing(bool obj)
        {
            IsRefreshing = isRefreshing;
        }

        private void ArticleController_latestArticlesAreReady(IList<Article> articles)
        {            


            var todayDate = DateTime.Now.Date;
            var todayDateString = todayDate.ToString("dd. MMMM", CultureInfo.InvariantCulture);
            var yesterdayString = todayDate.AddDays(-1).ToString("dd. MMMM", CultureInfo.InvariantCulture);


            var sorted = from article in articles
                         orderby article.PublishedDate descending
                         group article by article.PublishedDate.Date.ToString("dd. MMMM", CultureInfo.InvariantCulture) into articleGroup
                         select new Grouping<string, Article>(articleGroup.Key, articleGroup);

            Grouped = new List<Grouping<string, Article>>(sorted);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}


