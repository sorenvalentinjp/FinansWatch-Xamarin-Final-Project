using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;

namespace Prototype.UITest.UnitTests.ModelControllers
{
    public class ArticleControllerTest
    {
        ArticleController _articleController;
        [SetUp]
        public void Setup()
        {
            _articleController = new ArticleController();
        }

        [Test]
        public void Bucket1IsReadyEventShouldBeFired()
        {
            //Prepare

            //Assert

        }
        [Test]
        public void Bucket2IsReadyEventShouldBeFired()
        {
            //Prepare

            //Assert

        }
        [Test]
        public void SavedArticlesChangedEventShouldBeFired()
        {
            //Prepare

            //Assert

        }
        [Test]
        public void IsRefreshingFrontPageEventShouldBeFired()
        {
            //Prepare

            //Assert

        }
        [Test]
        public void IsRefreshingLatestArticlesShouldBeFired()
        {
            //Prepare

            //Assert

        }
    }
}
