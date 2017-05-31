using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Models
{

    /// <summary>
    /// JSON Generated class that is used in Article
    /// </summary>
    public class TopImage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public bool primary { get; set; }

        private string _imageCaption;
        public string imageCaption
        {
            get { return _imageCaption; }
            set
            {
                if (_imageCaption == value) { return; }
                _imageCaption = value;
                Notify("imageCaption");
            }
        }
        //public string id { get; set; }
        //public Big big { get; set; }
        private Small _small;
        public Small small
        {
            get { return _small; }
            set
            {
                if (_small == value) { return; }
                _small = value;
                Notify("small");
            }
        }
        private Thumb _thumb;
        public Thumb thumb
        {
            get { return _thumb; }
            set
            {
                if (_thumb == value) { return; }
                _thumb = value;
                Notify("thumb");
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

    /// <summary>
    /// JSON Generated class that is used in TopImage
    /// </summary>
    public class Small : INotifyPropertyChanged
    {
        private string _url;
        public string url
        {
            get { return _url; }
            set
            {
                if (_url == value) { return; }
                _url = value;
                Notify("url");
            }
        }

        private byte[] _imageByteArray = new byte[0];
        public byte[] ImageByteArray
        {
            get { return _imageByteArray; }
            set
            {
                if (_imageByteArray == value) { return; }
                _imageByteArray = value;
                Notify("ImageByteArray");
            }
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

    /// <summary>
    /// JSON Generated class that is used in TopImage
    /// </summary>
    public class Thumb : INotifyPropertyChanged
    {
        private string _url;
        public string url
        {
            get { return _url; }
            set
            {
                if (_url == value) { return; }
                _url = value;
                Notify("url");
            }
        }

        private byte[] _imageByteArray = new byte[0];
        public byte[] ImageByteArray
        {
            get { return _imageByteArray; }
            set
            {
                if (_imageByteArray == value) { return; }
                _imageByteArray = value;
                Notify("ImageByteArray");
            }
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
}
