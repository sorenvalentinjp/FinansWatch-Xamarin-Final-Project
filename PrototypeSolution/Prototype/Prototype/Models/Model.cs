using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Models
{

    public class Article
    {
        public Image image { get; set; }
        public string desktopUrl { get; set; }
        public string homeSectionName { get; set; }
        public string contentUrl { get; set; }
        public Titles titles { get; set; }
        public Teasers teasers { get; set; }
        public int homeSectionId { get; set; }
        public string publishedDate { get; set; }
        public bool locked { get; set; }
        public bool isTopArticle { get; set; }
        public List<object> inlineImages { get; set; }
        public Metadata metadata { get; set; }
        public bool breakingNews { get; set; }
        public object partner { get; set; }
        public string bodyText { get; set; }
        public string c_name { get; set; }
        public string c_category { get; set; }
        public List<object> tags { get; set; }
        public List<RelatedArticle> relatedArticles { get; set; }
        public List<TopImage> topImages { get; set; }
        public object video { get; set; }
        public List<object> quoteBoxes { get; set; }
        public List<object> reviewBoxes { get; set; }
        public List<object> factsBoxes { get; set; }
        public object updateInfo { get; set; }
        public PublishData publishData { get; set; }
        public string lastModified { get; set; }
        public int id { get; set; }

        public Article() { }
        public void addFieldsFromAnotherArticle(Article article)
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
            metadata = article.metadata;
            breakingNews = article.breakingNews;
            partner = article.partner;
            bodyText = article.bodyText;
            c_name = article.c_name;
            c_category = article.c_category;
            tags = article.tags;
            relatedArticles = article.relatedArticles;
            topImages = article.topImages;
            video = article.video;
            quoteBoxes = article.quoteBoxes;
            reviewBoxes = article.reviewBoxes;
            updateInfo = article.updateInfo;
            publishData = article.publishData;
            lastModified = article.lastModified;
            id = article.id;
        }
    }
    public class FrontpageLarge380
    {
        public string url { get; set; }
    }

    public class D
    {
        public string url { get; set; }
    }

    public class MediumFrontpage300
    {
        public string url { get; set; }
    }

    public class E
    {
        public string url { get; set; }
    }

    public class SmallArticle220
    {
        public string url { get; set; }
    }

    public class BigArticle460
    {
        public string url { get; set; }
    }

    public class F
    {
        public string url { get; set; }
    }

    public class HugeArticle620
    {
        public string url { get; set; }
    }

    public class Versions
    {
        public FrontpageLarge380 frontpage_large_380 { get; set; }
        public D d { get; set; }
        public MediumFrontpage300 medium_frontpage_300 { get; set; }
        public E e { get; set; }
        public SmallArticle220 small_article_220 { get; set; }
        public BigArticle460 big_article_460 { get; set; }
        public F f { get; set; }
        public HugeArticle620 huge_article_620 { get; set; }
    }

    public class Image
    {
        public string imageCaption { get; set; }
        public Versions versions { get; set; }
    }

    public class Titles
    {
        public string DEFAULT { get; set; }
        public object KICKER { get; set; }
        public string FRONTPAGE { get; set; }
    }

    public class Teasers
    {
        public string DEFAULT { get; set; }
        public string FRONTPAGE { get; set; }
    }

    public class Metadata
    {
        public string sectionDisplayName { get; set; }
        public string c_name { get; set; }
        public string c_category { get; set; }
        public string color { get; set; }
    }

    public class Teaser
    {
        public string DEFAULT { get; set; }
        public string FRONTPAGE { get; set; }
    }

    public class RelatedArticle
    {
        public string url { get; set; }
        public string title { get; set; }
        public string sectionName { get; set; }
        public string publishedTime { get; set; }
        public Teaser teaser { get; set; }
        public bool locked { get; set; }
    }

    public class Big
    {
        public string url { get; set; }
    }

    public class Small
    {
        public string url { get; set; }
    }

    public class Thumb
    {
        public string url { get; set; }
    }

    public class TopImage
    {
        public bool primary { get; set; }
        public string imageCaption { get; set; }
        public string id { get; set; }
        public Big big { get; set; }
        public Small small { get; set; }
        public Thumb thumb { get; set; }
    }

    public class PublishData
    {
        public List<object> authors { get; set; }
        public string publishInfo { get; set; }
        public string publishedTime { get; set; }
        public string publishedTimeFormatted { get; set; }

    }



}
