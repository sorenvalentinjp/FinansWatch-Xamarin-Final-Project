using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;
        StateController stateController;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            stateController = new StateController();
        }

        [Test]
        public async void ArticleControllerShouldReturnArticles()
        {
            IList<Article> latestArticles = await stateController.ArticleController.GetLatestArticlesAsync();
            Assert.IsTrue(latestArticles.Count > 0);
        }
    }
}

