using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Prototype.Database;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.Mocks;
using Prototype.UITest.UnitTests.Helpers;
using Prototype.ViewModels;
using Xamarin.Forms;

namespace Prototype.UITest.UnitTests.ViewModels
{
    public class AllArticlesViewModelTest
    {
        private AllArticlesViewModel _allArticlesViewModel;

        [SetUp]
        public void Setup()
        {
            _allArticlesViewModel = new AllArticlesViewModel(StateControllerMockGenerator.GenerateMock());    
        }

        [Test]
        public void GroupedShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_allArticlesViewModel, (x) => x.Grouped = new List<Grouping<string, ArticleViewModel>>(), "Grouped");
        }

        [Test]
        public void IsRefreshingShouldRaisePropertyChangedEvent()
        {
            _allArticlesViewModel.IsRefreshing = true;
            PropertyChangedAsserter.AssertPropertyChanged(_allArticlesViewModel, (x) => x.IsRefreshing = false, "IsRefreshing");
        }

        [Test]
        public void DataTemplateGroupHeaderShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_allArticlesViewModel, (x) => x.DataTemplateGroupHeader = new DataTemplate(), "DataTemplateGroupHeader");
        }

        [Test]
        public void DataTemplateShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_allArticlesViewModel, (x) => x.DataTemplate = new DataTemplate(), "DataTemplate");
        }

        [Test]
        public async void GroupArticlesShouldGroupArticles()
        {
            //Prepare 
            Article articleToday = new Article {publishedDateTime = DateTime.Now};
            Article articleYesterday = new Article {publishedDateTime = DateTime.Now.AddDays(-1)};
            IList<Article> articles = new List<Article>();
            articles.Add(articleToday);
            articles.Add(articleYesterday);

            //Act
            var grouped = await _allArticlesViewModel.GroupArticles(articles);
            var todayArray = grouped.ToArray()[0];
            var yesterdayArray = grouped.ToArray()[1];

            //Assert 
            Assert.AreEqual(todayArray.Count, 1);
            Assert.AreEqual(yesterdayArray.Count, 1);
            Assert.AreEqual(todayArray[0].Article, articleToday);
            Assert.AreEqual(yesterdayArray[0].Article, articleYesterday);
        }


    }
}
