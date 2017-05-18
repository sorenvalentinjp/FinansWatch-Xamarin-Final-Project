using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.Models;
using Prototype.UITest.UnitTests.Helpers;

namespace Prototype.UITest.UnitTests.Models
{
    public class ImageTest
    {
        [Test]
        public void ImagePropertyChangedShouldBeFired()
        {
            //Prepare
            var image = new Image();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(image, (x) => x.versions = new Versions(), "versions");
            PropertyChangedAsserter.AssertPropertyChanged(image, (x) => x.imageCaption = "test", "imageCaption");
        }

        [Test]
        public void VersionsPropertyChangedShouldBeFired()
        {
            //Prepare
            var versions = new Versions();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(versions, (x) => x.big_article_460 = new BigArticle460(), "big_article_460");
            PropertyChangedAsserter.AssertPropertyChanged(versions, (x) => x.f = new F(), "f");
            PropertyChangedAsserter.AssertPropertyChanged(versions, (x) => x.small_article_220 = new SmallArticle220(), "small_article_220");
        }

        [Test]
        public void ImageVersionsPropertyChangedShouldBeFired()
        {
            //Prepare
            var big_article_460 = new BigArticle460();
            var f = new F();
            var small_article_220 = new SmallArticle220();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(big_article_460, (x) => x.url = "test", "url");
            PropertyChangedAsserter.AssertPropertyChanged(f, (x) => x.url = "test", "url");
            PropertyChangedAsserter.AssertPropertyChanged(small_article_220, (x) => x.url = "test", "url");

        }

        [Test]
        public void TopImagePropertyChangedShouldBeFired()
        {
            //Prepare
            var topImage = new TopImage();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(topImage, (x) => x.small = new Small(), "small");
            PropertyChangedAsserter.AssertPropertyChanged(topImage, (x) => x.thumb = new Thumb(), "thumb");
            PropertyChangedAsserter.AssertPropertyChanged(topImage, (x) => x.imageCaption = "test", "imageCaption");
        }

        public void TopImageVersionsPropertyChangedShouldBeFired()
        {
            //Prepare
            var small = new Small();
            var thumb = new Thumb();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(small, (x) => x.url = "test", "url");
            PropertyChangedAsserter.AssertPropertyChanged(thumb, (x) => x.url = "test", "url");
        }
    }
}
