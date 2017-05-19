using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.UnitTests.Helpers;

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
        public void SavedArticlesChangedEventShouldBeFired()
        {
            //Prepare
            bool invoked = false;
            _articleController.SavedArticlesChangedEvent += () => { invoked = true; };
            var article = new Article();

            //Act
            _articleController.AddOrRemoveSavedArticle(article);

            //Assert
            Assert.True(invoked);
        }

        [Test]
        public void GetBucket1FrontpageShouldReturnArticles()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetBucket2ReturnArticles()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetBucket2ShouldPopulateLatestArticlesAndAllSections()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetArticlesAndDetailsForSectionShouldReturnSectionWithArticles()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetArticleDetailsForCollectionShouldReturnArticles()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetLatestArticlesAsyncShouldReturnLatestArticles()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetRelatedArticlesAsyncShouldReturnRelatedArticles()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetArticleDetailsAsyncWithArticleShouldReturnDetailedArticle()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void GetArticleDetailsAsyncWithStringShouldReturnDetailedArticle()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void AddOrRemoveSavedArticleShouldAddArticle()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void AddOrRemoveSavedArticleShouldRemoveArticle()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void DeserializeArticlesFromJsonShouldDeserializeToArticles()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void DeserializeArticleShouldDeserialeArticle()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void PrepareArticleShouldPrepareArticle()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void StripArticleShouldStripArticle()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void StripAllHtmlParagraphTagsShouldStrippAllHtmlParagraphs()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void StripRelatedArticlesShouldStripRelatedArticles()
        {
            //Prepare

            //Assert

        }


    }
}
