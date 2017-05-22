using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.Mocks;
using Prototype.UITest.UnitTests.Helpers;
using Prototype.ViewModels;

namespace Prototype.UITest.UnitTests.ViewModels
{
    public class ArticleViewModelTest
    {
        private ArticleViewModel _articleViewModel;
        private StateController _stateController;

        [SetUp]
        public void Setup()
        {
            _stateController = StateControllerMockGenerator.GenerateMock();
            _articleViewModel = new ArticleViewModel(_stateController, new Article { contentUrl = "testurl", locked = false});
        }

        [Test]
        public void ArticleShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_articleViewModel, (x) => x.Article = new Article(), "Article");
        }

        [Test]
        public void LockedShouldRaisePropertyChangedEvent()
        {
            _articleViewModel.Locked = false;
            PropertyChangedAsserter.AssertPropertyChanged(_articleViewModel, (x) => x.Locked = true, "Locked");
        }

        [Test]
        public void SubscriberHasAccessShouldRaisePropertyChangedEvent()
        {
            _articleViewModel.SubscriberHasAccess = false;
            PropertyChangedAsserter.AssertPropertyChanged(_articleViewModel, (x) => x.SubscriberHasAccess = true, "SubscriberHasAccess");
        }

        [Test]
        public void LockedIndicatorImageVisibleShouldRaisePropertyChangedEvent()
        {
            _articleViewModel.LockedIndicatorImageVisible = false;
            PropertyChangedAsserter.AssertPropertyChanged(_articleViewModel, (x) => x.LockedIndicatorImageVisible = true, "LockedIndicatorImageVisible");
        }

        [Test]
        public void UnlockedIndicatorImageVisibleShouldRaisePropertyChangedEvent()
        {
            _articleViewModel.UnlockedIndicatorImageVisible = false;
            PropertyChangedAsserter.AssertPropertyChanged(_articleViewModel, (x) => x.UnlockedIndicatorImageVisible = true, "UnlockedIndicatorImageVisible");
        }

        [Test]
        public void SavedArticlesChangedShouldSaveArticle()
        {
            //Prepare
            Article article = new Article{contentUrl = "testurl" };
            _stateController.ArticleController.SavedArticles.Add(article);
            _articleViewModel.Article = article;

            //Act
            _articleViewModel.SavedArticlesChanged();

            //Assert
            Assert.IsTrue(article.IsSaved);
        }

        [Test]
        public void SavedArticlesChangedShouldNotSaveArticle()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl" };
            _articleViewModel.Article = article;

            //Act
            _articleViewModel.CalculateIfArticleShouldBeLocked();

            //Assert
            Assert.IsFalse(article.IsSaved);
        }

        [Test]
        public void CalculateIfArticleShouldNotGiveSubcriberAccess()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl", locked = true};
            var subscriber = new Subscriber();
            var featureAccessList = new FeatureAccessList { access = false, code = "FINANSWATCH" };
            subscriber.featureAccessList = new List<FeatureAccessList>();
            subscriber.featureAccessList.Add(featureAccessList);
            _stateController.LoginController.Subscriber = subscriber;
            _articleViewModel.Article = article;

            //Act
            _articleViewModel.CalculateIfArticleShouldBeLocked();

            //Assert
            Assert.IsFalse(_articleViewModel.SubscriberHasAccess);
        }

        [Test]
        public void CalculateIfArticleShouldBeLockedShouldGiveSubscriberAccess()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl", locked = true };
            var subscriber = new Subscriber();
            var featureAccessList = new FeatureAccessList { access = true, code = "FINANSWATCH" };
            subscriber.featureAccessList = new List<FeatureAccessList>();
            subscriber.featureAccessList.Add(featureAccessList);
            _stateController.LoginController.Subscriber = subscriber;
            _articleViewModel.Article = article;

            //Act
            _articleViewModel.CalculateIfArticleShouldBeLocked();

            //Assert
            Assert.IsTrue(_articleViewModel.SubscriberHasAccess);
        }

        [Test]
        public void CalculateIfArticleShouldBeLockedShouldGiveSubscriberAccess2()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl", locked = false };
            var subscriber = new Subscriber();
            var featureAccessList = new FeatureAccessList { access = false, code = "FINANSWATCH" };
            subscriber.featureAccessList = new List<FeatureAccessList>();
            subscriber.featureAccessList.Add(featureAccessList);
            _stateController.LoginController.Subscriber = subscriber;
            _articleViewModel.Article = article;

            //Act
            _articleViewModel.CalculateIfArticleShouldBeLocked();

            //Assert
            Assert.IsTrue(_articleViewModel.SubscriberHasAccess);
        }

        [Test]
        public void CalculateIfArticleShouldBeLockedShouldGiveSubscriberAccess3()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl", locked = false };
            var subscriber = new Subscriber();
            var featureAccessList = new FeatureAccessList { access = true, code = "FINANSWATCH" };
            subscriber.featureAccessList = new List<FeatureAccessList>();
            subscriber.featureAccessList.Add(featureAccessList);
            _stateController.LoginController.Subscriber = subscriber;
            _articleViewModel.Article = article;

            //Act
            _articleViewModel.CalculateIfArticleShouldBeLocked();

            //Assert
            Assert.IsTrue(_articleViewModel.SubscriberHasAccess);
        }

        [Test]
        public async void GetArticleDetailsShouldGetArticleDetails()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl" };
            _articleViewModel.Article = article;

            //Act
            var articleViewModel = await _articleViewModel.GetArticleDetails();

            //Assert
            Assert.IsNullOrEmpty(articleViewModel.Article.bodyText);
        }

        [Test]
        public async void GetArticleDetailsShouldGetRelatedArticlesAndReturnEmptyList()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl" };
            _articleViewModel.Article = article;

            //Act
            var detailedArticle = await _articleViewModel.GetArticleDetails();

            //Assert
            CollectionAssert.IsEmpty(_articleViewModel.Article.relatedDetailedArticles);
        }

        [Test]
        public async void GetArticleDetailsShouldNotGetArticleDetails()
        {
            //Prepare
            Article article = new Article { contentUrl = "testurl", bodyText = "Article bodytext here"};
            _articleViewModel.Article = article;

            //Act
            var detailedArticle = await _articleViewModel.GetArticleDetails();

            //Assert
            Assert.IsNotNullOrEmpty(detailedArticle.Article.bodyText);
        }
    }
}
