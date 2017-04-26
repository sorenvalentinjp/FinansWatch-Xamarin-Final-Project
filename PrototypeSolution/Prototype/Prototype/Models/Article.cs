using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

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

        ImageSource imageSourceBig;
        public ImageSource ImageSourceBig
        {
            get { return imageSourceBig; }
            set
            {
                if (imageSourceBig == value) { return; }
                imageSourceBig = value;
                notify("ImageSourceBig");
            }
        }

        ImageSource imageSourceSmall;
        public ImageSource ImageSourceSmall
        {
            get { return imageSourceSmall; }
            set
            {
                if (imageSourceSmall == value) { return; }
                imageSourceSmall = value;
                notify("ImageSourceSmall");
            }
        }

        ImageSource imageSourceThumb;
        public ImageSource ImageSourceThumb
        {
            get { return imageSourceThumb; }
            set
            {
                if (imageSourceThumb == value) { return; }
                imageSourceThumb = value;
                notify("ImageSourceThumb");
            }
        }

        ImageSource imagePlaceholderBig;
        public ImageSource ImagePlaceholderBig
        {
            get { return imagePlaceholderBig; }
            set
            {
                if (imagePlaceholderBig == value) { return; }
                imagePlaceholderBig = value;
                notify("ImagePlaceholderBig");
            }
        }

        ImageSource imagePlaceholderSmall;
        public ImageSource ImagePlaceholderSmall
        {
            get { return imagePlaceholderSmall; }
            set
            {
                if (imagePlaceholderSmall == value) { return; }
                imagePlaceholderSmall = value;
                notify("ImagePlaceholderSmall");
            }
        }

        ImageSource imageTransparentSmall;
        public ImageSource ImageTransparentSmall
        {
            get { return imageTransparentSmall; }
            set
            {
                if (imageTransparentSmall == value) { return; }
                imageTransparentSmall = value;
                notify("ImageTransparentSmall");
            }
        }

        ObservableCollection<Article> relatedArticles;
        public ObservableCollection<Article> RelatedArticles
        {
            get { return relatedArticles; }
            set
            {
                if (relatedArticles == value) { return; }
                relatedArticles = value;
                notify("RelatedArticles");
            }
        }

        public Article() {
            title = "";
            contentURL = "";
            bodyText = "";
            homeSectionName = "";
            publishInfo = "";
            homeSectionId = -1;
            id = -1;
            ImageCaption = "";
            teaser = "";
            ImageBigURL = "";
            imageSmallURL = "";
            imageThumbURL = "";
            imagePlaceholderBig = ImageSource.FromFile("imagePlaceholderBig.png");
            imagePlaceholderSmall = ImageSource.FromFile("imagePlaceholderSmall.png");
            imageTransparentSmall = ImageSource.FromFile("imageTransparentSmall.png");
            isTopArticle = false;
            relatedArticles = new ObservableCollection<Article>();
        }


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
