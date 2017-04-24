using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Prototype.Models
{
    public class Article : INotifyPropertyChanged
    {
        string title;
        public string Title
        {
            get { return title; }
            set
            {
                if(title == value) { return; }
                title = value;
                notify("Title");
            }
        }

        string contentURL;
        public string ContentURL
        {
            get { return contentURL; }
            set
            {
                if (contentURL == value) { return; }
                contentURL = value;
                notify("ContentURL");
            }
        }

        string bodyText;
        public string BodyText
        {
            get { return bodyText; }
            set
            {
                if (bodyText == value) { return; }
                bodyText = value;
                notify("BodyText");
            }
        }

        string homeSectionName;
        public string HomeSectionName
        {
            get { return homeSectionName; }
            set
            {
                if (homeSectionName == value) { return; }
                homeSectionName = value;
                notify("HomeSectionName");
            }
        }

        Boolean locked;
        public Boolean Locked
        {
            get { return locked; }
            set
            {
                if (locked == value) { return; }
                locked = value;
                notify("Locked");
            }
        }

        DateTime publishedDate;
        public DateTime PublishedDate
        {
            get { return publishedDate; }
            set
            {
                if (publishedDate == value) { return; }
                publishedDate = value;
                notify("PublishedDate");
            }
        }

        string publishInfo;
        public string PublishInfo
        {
            get { return publishInfo; }
            set
            {
                if (publishInfo == value) { return; }
                publishInfo = value;
                notify("PublishInfo");
            }
        }

        int homeSectionId;
        public int HomeSectionId
        {
            get { return homeSectionId; }
            set
            {
                if (homeSectionId == value) { return; }
                homeSectionId = value;
                notify("HomeSectionId");
            }
        }

        int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id == value) { return; }
                id = value;
                notify("Id");
            }
        }

        string imageCaption;
        public string ImageCaption
        {
            get { return imageCaption; }
            set
            {
                if (imageCaption == value) { return; }
                imageCaption = value;
                notify("ImageCaption");
            }
        }

        string teaser;
        public string Teaser
        {
            get { return teaser; }
            set
            {
                if (teaser == value) { return; }
                teaser = value;
                notify("Teaser");
            }
        }

        string imageBigURL;
        public string ImageBigURL
        {
            get { return imageBigURL; }
            set
            {
                if (imageBigURL == value) { return; }
                imageBigURL = value;
                notify("ImageBigURL");
            }
        }

        string imageSmallURL;
        public string ImageSmallURL
        {
            get { return imageSmallURL; }
            set
            {
                if (imageSmallURL == value) { return; }
                imageSmallURL = value;
                notify("ImageSmallURL");
            }
        }

        string imageThumbURL;
        public string ImageThumbURL
        {
            get { return imageThumbURL; }
            set
            {
                if (imageThumbURL == value) { return; }
                imageThumbURL = value;
                notify("ImageThumbURL");
            }
        }

        //added to be able to know if an article is the top article on the frontpage
        Boolean isTopArticle;
        public Boolean IsTopArticle
        {
            get { return isTopArticle; }
            set
            {
                if (isTopArticle == value) { return; }
                isTopArticle = value;
                notify("IsTopArticle");
            }
        }

        public Article() { }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void notify(string propName)
        {
            if(this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
