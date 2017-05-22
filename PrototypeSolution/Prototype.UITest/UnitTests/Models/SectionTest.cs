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
    public class SectionTest
    {
        [Test]
        public void SectionPropertyChangedShouldBeFired()
        {
            //arrange
            var section = new Section("myTestSection", "www.myTestSectionUrl.com");

            //assert
            PropertyChangedAsserter.AssertPropertyChanged(section, (x) => x.Name = "newName", "Name");
            PropertyChangedAsserter.AssertPropertyChanged(section, (x) => x.SectionContentUrl = "newUrl", "Area");
            PropertyChangedAsserter.AssertPropertyChanged(section, (x) => x.Articles = new List<Article>(), "Articles");
        }
    }
}
