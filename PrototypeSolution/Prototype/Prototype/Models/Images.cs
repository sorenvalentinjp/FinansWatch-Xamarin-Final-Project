using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Models
{
    /// <summary>
    /// JSON Generated class that is used in Article
    /// </summary>
    public class Image : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private Versions _versions;
        public Versions versions
        {
            get { return _versions; }
            set
            {
                if (_versions == value) { return; }
                _versions = value;
                Notify("versions");
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
    /// JSON Generated class that is used in Image
    /// </summary>
    public class Versions : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        //public FrontpageLarge380 frontpage_large_380 { get; set; }
        //public D d { get; set; }
        //public MediumFrontpage300 medium_frontpage_300 { get; set; }
        //public E e { get; set; }
        private SmallArticle220 _small_article_220;
        public SmallArticle220 small_article_220
        {
            get { return _small_article_220; }
            set
            {
                if (_small_article_220 == value) { return; }
                _small_article_220 = value;
                Notify("small_article_220");
            }
        }

        private BigArticle460 _big_article_460;
        public BigArticle460 big_article_460
        {
            get { return _big_article_460; }
            set
            {
                if (_big_article_460 == value) { return; }
                _big_article_460 = value;
                Notify("big_article_460");
            }
        }

        private F _f;
        public F f
        {
            get { return _f; }
            set
            {
                if (_f == value) { return; }
                _f = value;
                Notify("f");
            }
        }
        //public HugeArticle620 huge_article_620 { get; set; }

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    //public class FrontpageLarge380 : INotifyPropertyChanged
    //{
    //private string url;
    //public string Url
    //{
    //    get { return url; }
    //    set
    //    {
    //        if (url == value) { return; }
    //        url = value;
    //        Notify("Url");
    //    }
    //}

    //public event PropertyChangedEventHandler PropertyChanged;

    //protected void Notify(string propName)
    //{
    //    if (this.PropertyChanged != null)
    //    {
    //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
    //    }
    //}
    //}

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>z
    //public class D : INotifyPropertyChanged
    //{
    //private string url;
    //public string Url
    //{
    //    get { return url; }
    //    set
    //    {
    //        if (url == value) { return; }
    //        url = value;
    //        Notify("Url");
    //    }
    //}

    //public event PropertyChangedEventHandler PropertyChanged;

    //protected void Notify(string propName)
    //{
    //    if (this.PropertyChanged != null)
    //    {
    //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
    //    }
    //}
    //}

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    //public class MediumFrontpage300 : INotifyPropertyChanged
    //{
    //private string url;
    //public string Url
    //{
    //    get { return url; }
    //    set
    //    {
    //        if (url == value) { return; }
    //        url = value;
    //        Notify("Url");
    //    }
    //}

    //public event PropertyChangedEventHandler PropertyChanged;

    //protected void Notify(string propName)
    //{
    //    if (this.PropertyChanged != null)
    //    {
    //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
    //    }
    //}
    //}

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    //public class E : INotifyPropertyChanged
    //{
    //private string url;
    //public string Url
    //{
    //    get { return url; }
    //    set
    //    {
    //        if (url == value) { return; }
    //        url = value;
    //        Notify("Url");
    //    }
    //}

    //public event PropertyChangedEventHandler PropertyChanged;

    //protected void Notify(string propName)
    //{
    //    if (this.PropertyChanged != null)
    //    {
    //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
    //    }
    //}
    //}

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class SmallArticle220 : INotifyPropertyChanged
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
    /// JSON Generated class that is used in Image
    /// </summary>
    public class BigArticle460 : INotifyPropertyChanged
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
    /// JSON Generated class that is used in Image
    /// </summary>
    public class F : INotifyPropertyChanged
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
    /// JSON Generated class that is used in Image
    /// </summary>
    //public class HugeArticle620
    //{
    //    public string url { get; set; }
    //}


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
    //public class Big : INotifyPropertyChanged
    //{
    //    private string url;
    //    public string Url
    //    {
    //        get { return url; }
    //        set
    //        {
    //            if (url == value) { return; }
    //            url = value;
    //            Notify("Url");
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected void Notify(string propName)
    //    {
    //        if (this.PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(propName));
    //        }
    //    }
    //}

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
