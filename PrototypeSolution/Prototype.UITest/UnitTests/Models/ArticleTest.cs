using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Prototype.ModelControllers;
using Prototype.Models;

namespace Prototype.UITest.UnitTests.Models
{
    public class Tests
    {
        StateController stateController;

        [SetUp]
        public void BeforeEachTest()
        {
            stateController = new StateController();
        }

        [Test]
        public async void ArticleControllerShouldReturnArticles()
        {
            var articles = await stateController.RefreshFrontPage();
            Assert.NotNull(articles);
            Assert.IsTrue(true);
        }

        [Test]
        public  void test()
        {

        }
    }
}

