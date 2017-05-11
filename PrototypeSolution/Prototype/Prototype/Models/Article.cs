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
        private string _homeSectionName;
        public string homeSectionName
        {
            get { return _homeSectionName; }
            set
            {
                if (_homeSectionName == value) { return; }
                _homeSectionName = value;
                Notify("homeSectionName");
            }
        }

        private string _contentUrl;
        public string contentUrl
        {
            get { return _contentUrl; }
            set
            {
                if (_contentUrl == value) { return; }
                _contentUrl = value;
                Notify("contentUrl");
            }
        }

        private Titles _titles;
        public Titles titles
        {
            get { return _titles; }
            set
            {
                if (_titles == value) { return; }
                _titles = value;
                Notify("titles");
            }
        }
        private Teasers _teasers;
        public Teasers teasers
        {
            get { return _teasers; }
            set
            {
                if (_teasers == value) { return; }
                _teasers = value;
                Notify("teasers");
            }
        }
        public int _homeSectionId;
        public int homeSectionId
        {
            get { return _homeSectionId; }
            set
            {
                if (_homeSectionId == value) { return; }
                _homeSectionId = value;
                Notify("homeSectionId");
            }
        }
        private string _publishedDate;
        public string publishedDate
        {
            get { return _publishedDate; }
            set
            {
                if (_publishedDate == value) { return; }
                _publishedDate = value;
                Notify("publishedDate");
            }
        }
        private DateTime _publishedDateTime;
        public DateTime publishedDateTime
        {
            get { return _publishedDateTime; }
            set
            {
                if (_publishedDateTime == value) { return; }
                _publishedDateTime = value;
                Notify("publishedDateTime");
            }
        }
        private bool _locked;
        public bool locked
        {
            get { return _locked; }
            set
            {
                if (_locked == value) { return; }
                _locked = value;
                Notify("locked");
            }
        }
        private bool _isTopArticle;
        public bool isTopArticle
        {
            get { return _isTopArticle; }
            set
            {
                if (_isTopArticle == value) { return; }
                _isTopArticle = value;
                Notify("isTopArticle");
            }
        }
        private List<object> _inlineImages;
        public List<object> inlineImages
        {
            get { return _inlineImages; }
            set
            {
                if (_inlineImages == value) { return; }
                _inlineImages = value;
                Notify("inlineImages");
            }
        }
        //public Metadata metadata { get; set; }
        //public bool breakingNews { get; set; }
        //public object partner { get; set; }
        private string _bodyText;
        public string bodyText
        {
            get { return _bodyText; }
            set
            {
                if (_bodyText == value) { return; }
                _bodyText = value;
                Notify("bodyText");
            }
        }
        //public string c_name { get; set; }
        //public string c_category { get; set; }
        //public List<object> tags { get; set; }
        private List<RelatedArticle> _relatedArticles;
        public List<RelatedArticle> relatedArticles
        {
            get { return _relatedArticles; }
            set
            {
                if (_relatedArticles == value) { return; }
                _relatedArticles = value;
                Notify("relatedArticles");
            }
        }
        private List<TopImage> _topImages;
        public List<TopImage> topImages
        {
            get { return _topImages; }
            set
            {
                if (_topImages == value) { return; }
                _topImages = value;
                Notify("topImages");
            }
        }
        //public object video { get; set; }
        //public List<object> quoteBoxes { get; set; }
        //public List<object> reviewBoxes { get; set; }
        //public List<object> factsBoxes { get; set; }
        //public object updateInfo { get; set; }
        private PublishData _publishData;
        public PublishData publishData
        {
            get { return _publishData; }
            set
            {
                if (_publishData == value) { return; }
                _publishData = value;
                Notify("publishData");
            }
        }
        private string _lastModified;
        public string lastModified
        {
            get { return _lastModified; }
            set
            {
                if (_lastModified == value) { return; }
                _lastModified = value;
                Notify("lastModified");
            }
        }
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

        public IList<Article> _relatedDetailedArticles;
        public IList<Article> relatedDetailedArticles
        {
            get { return _relatedDetailedArticles; }
            set
            {
                if (_relatedDetailedArticles == value) { return; }
                _relatedDetailedArticles = value;
                Notify("relatedDetailedArticles");
            }
        }
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
    public class Titles : INotifyPropertyChanged
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
        private object _KICKER;
        public object KICKER
        {
            get { return _KICKER; }
            set
            {
                if (_KICKER == value) { return; }
                _KICKER = value;
                Notify("KICKER");
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
