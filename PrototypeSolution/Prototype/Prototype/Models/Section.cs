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

        private int _id;
        public int Id
        { 
            get { return _id; }
            set
            {
                if (_id == value) { return; }
                _id = value;
                Notify("Id");
            }
        }

        private string _area;
        public string Area
        {
            get { return _area; }
            set
            {
                if (_area == value) { return; }
                _area = value;
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

        public Section(string name, int id, string area)
        {
            this.Name = name;
            this.Id = id;
            this.Area = area;
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
