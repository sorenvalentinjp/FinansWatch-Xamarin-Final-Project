using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.UnitTests.Helpers;

namespace Prototype.UITest.UnitTests.Models
{
    public class Tests
    {
        [Test]
        public void ArticlePropertyChangedShouldBeFired()
        {
            //arrange
            var article = new Article();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.locked = true, "locked");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.IsSaved = true, "IsSaved");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.bodyText = "test", "bodyText");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.contentUrl = "test", "contentUrl");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.homeSectionId = 1, "homeSectionId");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.homeSectionName = "test", "homeSectionName");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.id = 1, "id");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.image = new Image(), "image");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.isTopArticle = true, "isTopArticle");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.lastModified = "19-12-15", "lastModified");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.publishData = new PublishData(), "publishData");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.publishedDateTime = DateTime.Now, "publishedDateTime");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.relatedArticles = new List<RelatedArticle>(), "relatedArticles");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.relatedDetailedArticles = new List<Article>(), "relatedDetailedArticles");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.teasers = new Teasers(), "teasers");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.titles = new Titles(), "titles");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.topImage = new TopImage(), "topImage");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.topImages = new List<TopImage>(), "topImages");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.publishedDate = "19-11-17", "publishedDate");
            PropertyChangedAsserter.AssertPropertyChanged(article, (x) => x.desktopUrl = "someUrl", "desktopUrl");
        }

        [Test]
        public void ArticleShouldEqual()
        {
            //arrange
            var article = new Article {contentUrl = "testUrl"};
            var article2 = new Article { contentUrl = "testUrl" };

            //Assert
            Assert.AreEqual(article2, article);
        }

        [Test]
        public void ArticleShouldNotEqual()
        {
            //arrange
            var article = new Article { contentUrl = "testUrl" };
            var article2 = new Article { contentUrl = "anotherTestUrl" };

            //Assert
            Assert.AreNotEqual(article2, article);
        }

        [Test]
        public void TitlesPropertyChangedShouldBeFired()
        {
            //arrange
            var titles = new Titles();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(titles, (x) => x.DEFAULT = "test", "DEFAULT");
            PropertyChangedAsserter.AssertPropertyChanged(titles, (x) => x.FRONTPAGE = "test", "FRONTPAGE");
            PropertyChangedAsserter.AssertPropertyChanged(titles, (x) => x.KICKER = "test", "KICKER");
        }

        [Test]
        public void TeasersPropertyChangedShouldBeFired()
        {
            //arrange
            var teasers = new Teasers();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(teasers, (x) => x.DEFAULT = "test", "DEFAULT");
            PropertyChangedAsserter.AssertPropertyChanged(teasers, (x) => x.FRONTPAGE = "test", "FRONTPAGE");
        }

        [Test]
        public void MetadataPropertyChangedShouldBeFired()
        {
            //arrange
            var metadata = new Metadata();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(metadata, (x) => x.c_category = "test", "c_category");
            PropertyChangedAsserter.AssertPropertyChanged(metadata, (x) => x.c_name = "test", "c_name");
            PropertyChangedAsserter.AssertPropertyChanged(metadata, (x) => x.color = "test", "color");
            PropertyChangedAsserter.AssertPropertyChanged(metadata, (x) => x.sectionDisplayName = "test", "sectionDisplayName");
        }

        [Test]
        public void TeaserPropertyChangedShouldBeFired()
        {
            //arrange
            var teaser = new Teaser();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(teaser, (x) => x.DEFAULT = "test", "DEFAULT");
            PropertyChangedAsserter.AssertPropertyChanged(teaser, (x) => x.FRONTPAGE = "test", "FRONTPAGE");
        }

        [Test]
        public void RelatedArticlePropertyChangedShouldBeFired()
        {
            //arrange
            var realedArticle = new RelatedArticle();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(realedArticle, (x) => x.url = "test", "url");
        }

        [Test]
        public void PublishDataPropertyChangedShouldBeFired()
        {
            //arrange
            var publishData = new PublishData();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(publishData, (x) => x.publishInfo = "test", "publishInfo");
        }

    }
}

