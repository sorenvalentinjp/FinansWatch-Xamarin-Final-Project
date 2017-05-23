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
        public void TopImagePropertyChangedShouldBeFired()
        {
            //arrange
            var topImage = new TopImage();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(topImage, (x) => x.small = new Small(), "small");
            PropertyChangedAsserter.AssertPropertyChanged(topImage, (x) => x.thumb = new Thumb(), "thumb");
            PropertyChangedAsserter.AssertPropertyChanged(topImage, (x) => x.imageCaption = "test", "imageCaption");
        }

        public void TopImageVersionsPropertyChangedShouldBeFired()
        {
            //arrange
            var small = new Small();
            var thumb = new Thumb();

            //Assert
            PropertyChangedAsserter.AssertPropertyChanged(small, (x) => x.url = "test", "url");
            PropertyChangedAsserter.AssertPropertyChanged(thumb, (x) => x.url = "test", "url");
        }
    }
}
