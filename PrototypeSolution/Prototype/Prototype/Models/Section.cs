using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prototype.Models
{
    public class Section : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) { return; }
                _name = value;
                Notify("Name");
            }
        }

        private string _sectionContentUrl;
        public string SectionContentUrl
        {
            get { return _sectionContentUrl; }
            set
            {
                if (_sectionContentUrl == value) { return; }
                _sectionContentUrl = value;
                Notify("Area");
            }
        }

        private IList<Article> _articles;
        public IList<Article> Articles
        {
            get { return _articles; }
            set
            {
                if (_articles == value) { return; }
                _articles = value;
                Notify("Articles");
            }
        }

        public Section(string name, string sectionContentUrl)
        {
            this.Name = name;
            this.SectionContentUrl = sectionContentUrl;
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
