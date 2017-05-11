using Prototype.ModelControllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class SectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly StateController _stateController;
        private Section section;
        public Section Section
        {
            get { return section; }
            set
            {
                if (section == value) { return; }
                section = value;
                Notify("Section");
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

        public SectionViewModel(StateController stateController, Section section)
        {
            this._stateController = stateController;

            DataTemplate = new SectionTemplateSelector(_stateController);

            //
            if (section.Articles == null)
            {
                //Init download
            }
            else
            {
                //Prepare articleViewModels
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    //Refresh
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
