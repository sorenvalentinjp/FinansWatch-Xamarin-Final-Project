using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Prototype.Database;
using Xamarin.Forms;

namespace Prototype.Models
{
    /// <summary>
    /// JSON Generated class
    /// </summary>
    public class Article : IEquatable<Article>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Generated fields start
        private Image _image;
        public Image image
        {
            get { return _image; }
            set
            {
                if (_image == value) { return; }
                _image = value;
                Notify("image");
            }
        }
    //public string desktopUrl { get; set; }
    public string homeSectionName { get; set; }
        public string contentUrl { get; set; }
        public Titles titles { get; set; }
        public Teasers teasers { get; set; }
        public int homeSectionId { get; set; }
        public string publishedDate { get; set; }
        public DateTime publishedDateTime { get; set; }
        public bool locked { get; set; }
        public bool isTopArticle { get; set; }
        public List<object> inlineImages { get; set; }
        //public Metadata metadata { get; set; }
        //public bool breakingNews { get; set; }
        //public object partner { get; set; }
        public string bodyText { get; set; }
        //public string c_name { get; set; }
        //public string c_category { get; set; }
        //public List<object> tags { get; set; }
        public List<RelatedArticle> relatedArticles { get; set; }
        public List<TopImage> topImages { get; set; }
        //public object video { get; set; }
        //public List<object> quoteBoxes { get; set; }
        //public List<object> reviewBoxes { get; set; }
        //public List<object> factsBoxes { get; set; }
        //public object updateInfo { get; set; }
        public PublishData publishData { get; set; }
        public string lastModified { get; set; }
        public int id { get; set; }
        //Generated fields end


        //Manually made fields start

        private bool _isSaved;
        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                if (_isSaved == value) { return; }
                _isSaved = value;
                Notify("IsSaved");
            }
        }

        public IList<Article> relatedDetailedArticles { get; set; }
        //Used because we want to acces the first topImage in the topImages list. 
        private TopImage _topImage;
        public TopImage topImage
        {
            get { return _topImage; }
            set
            {
                if (_topImage == value) { return; }
                _topImage = value;
                Notify("topImage");
            }
        }

        //Manually made fields end
        public Article()
        {
        }

        //Implementation of IEquatable<Article> so we can call contains on a Collection<Article>. This is to avoid dublicates in eg. savedArticles in the statecontroller.
        public bool Equals(Article other)
        {
            return null != other && contentUrl == other.contentUrl;
        }

        public override int GetHashCode()
        {
            if (contentUrl != null)
            {
                return contentUrl.GetHashCode();
            }
            else
            {
                return -1;
            }
            
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Article);
        }

        //-----------------> we should override public override int gethashcode() also!!

        public void AddFieldsFromAnotherArticle(Article article)
        {
            //image = article.image;
            //desktopUrl = article.desktopUrl;
            //homeSectionName = article.homeSectionName;
            //contentUrl = article.contentUrl;
            //titles = article.titles;
            //teasers = article.teasers;
            //homeSectionId = article.homeSectionId;
            //publishedDate = article.publishedDate;
            //locked = article.locked;
            //isTopArticle = article.isTopArticle;
            inlineImages = article.inlineImages;
            //metadata = article.metadata;
            //breakingNews = article.breakingNews;
            //partner = article.partner;
            bodyText = article.bodyText;
            //c_name = article.c_name;
            //c_category = article.c_category;
            //tags = article.tags;
            relatedArticles = article.relatedArticles;
            topImages = article.topImages;
            //video = article.video;
            //quoteBoxes = article.quoteBoxes;
            //reviewBoxes = article.reviewBoxes;
            //updateInfo = article.updateInfo;
            publishData = article.publishData;
            lastModified = article.lastModified;
            id = article.id;
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
    /// JSON Generated class that is used in Article
    /// </summary>
    public class Titles
    {
        public string DEFAULT { get; set; }
        public object KICKER { get; set; }
        public string FRONTPAGE { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Article
    /// </summary>
    public class Teasers : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _DEFAULT;
        public string DEFAULT
        {
            get { return _DEFAULT; }
            set
            {
                if (_DEFAULT == value) { return; }
                _DEFAULT = value;
                Notify("DEFAULT");
            }
        }

        private string _FRONTPAGE;
        public string FRONTPAGE
        {
            get { return _FRONTPAGE; }
            set
            {
                if (_FRONTPAGE == value) { return; }
                _FRONTPAGE = value;
                Notify("FRONTPAGE");
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
    /// JSON Generated class that is used in Article
    /// </summary>
    public class Metadata : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _sectionDisplayName;
        public string sectionDisplayName
        {
            get { return _sectionDisplayName; }
            set
            {
                if (_sectionDisplayName == value) { return; }
                _sectionDisplayName = value;
                Notify("sectionDisplayName");
            }
        }

        private string _c_name;
        public string c_name
        {
            get { return _c_name; }
            set
            {
                if (_c_name == value) { return; }
                _c_name = value;
                Notify("c_name");
            }
        }

        private string _c_category;
        public string c_category
        {
            get { return _c_category; }
            set
            {
                if (_c_category == value) { return; }
                _c_category = value;
                Notify("c_category");
            }
        }

        private string _color;
        public string color
        {
            get { return _color; }
            set
            {
                if (_color == value) { return; }
                _color = value;
                Notify("color");
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
    /// JSON Generated class that is used in Article
    /// </summary>
    public class Teaser : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _DEFAULT;
        public string DEFAULT
        {
            get { return _DEFAULT; }
            set
            {
                if (_DEFAULT == value) { return; }
                _DEFAULT = value;
                Notify("DEFAULT");
            }
        }

        private string _FRONTPAGE;
        public string FRONTPAGE
        {
            get { return _FRONTPAGE; }
            set
            {
                if (_FRONTPAGE == value) { return; }
                _FRONTPAGE = value;
                Notify("FRONTPAGE");
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
    /// JSON Generated class that is used in Article
    /// </summary>
    public class RelatedArticle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
        //public string title { get; set; }
        //public string sectionName { get; set; }
        //public string publishedTime { get; set; }
        //public Teaser teaser { get; set; }
        //public bool locked { get; set; }

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    /// <summary>
    /// JSON Generated class that is used in Article
    /// </summary>
    public class PublishData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public List<object> authors { get; set; }
        private string _publishInfo;
        public string publishInfo
        {
            get { return _publishInfo; }
            set
            {
                if (_publishInfo == value) { return; }
                _publishInfo = value;
                Notify("publishInfo");
            }
        }
        //public string publishedTime { get; set; }
        //public string publishedTimeFormatted { get; set; }

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
