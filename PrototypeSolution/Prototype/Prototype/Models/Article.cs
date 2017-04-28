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

        ArticleImage articleImage;
        public ArticleImage ArticleImage
        {
            get { return articleImage; }
            set
            {
                if (articleImage == value) { return; }
                articleImage = value;
                notify("ArticleImage");
            }
        }

        ArticleImage frontPageImage;
        public ArticleImage FrontPageImage
        {
            get { return frontPageImage; }
            set
            {
                if (frontPageImage == value) { return; }
                frontPageImage = value;
                notify("FrontPageImage");
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
            teaser = "";
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
