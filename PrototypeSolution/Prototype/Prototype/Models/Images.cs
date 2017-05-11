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
    public class Image
    {
        public string imageCaption { get; set; }
        public Versions versions { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class Versions
    {
        //public FrontpageLarge380 frontpage_large_380 { get; set; }
        //public D d { get; set; }
        //public MediumFrontpage300 medium_frontpage_300 { get; set; }
        //public E e { get; set; }
        public SmallArticle220 small_article_220 { get; set; }
        public BigArticle460 big_article_460 { get; set; }
        public F f { get; set; }
        //public HugeArticle620 huge_article_620 { get; set; }
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
        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                if (url == value) { return; }
                url = value;
                Notify("Url");
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
        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                if (url == value) { return; }
                url = value;
                Notify("Url");
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
        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                if (url == value) { return; }
                url = value;
                Notify("Url");
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
    public class TopImage
    {
        public bool primary { get; set; }
        public string imageCaption { get; set; }
        public string id { get; set; }
        //public Big big { get; set; }
        public Small small { get; set; }
        public Thumb thumb { get; set; }
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
        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                if (url == value) { return; }
                url = value;
                Notify("Url");
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
        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                if (url == value) { return; }
                url = value;
                Notify("Url");
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
