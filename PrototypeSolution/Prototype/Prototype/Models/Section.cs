using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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

        private string _uniqueName;
        public string UniqueName
        {
            get { return _uniqueName; }
            set
            {
                if (_uniqueName == value) { return; }
                _uniqueName = value;
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

        public Section(string name, string uniqueName)
        {
            this.Name = name;
            this.UniqueName = uniqueName;
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
