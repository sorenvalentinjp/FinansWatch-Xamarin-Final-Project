using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Prototype.Database;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.UnitTests.Helpers;

namespace Prototype.UITest.UnitTests.ModelControllers
{
    [TestFixture]
    public class ArticleControllerTest
    {
        ArticleController _articleController;

        Mock<IContentApi> _mock;

        string _validSectionUrl;
        string _validArticleUrl;

        string _validArticleResponse;
        string _validLatestArticlesResponse;
        string _validSectionResponse;

        [TestFixtureSetUp]
        public void Init()
        {
            //setting up mock
            _mock = new Mock<IContentApi>();
            _validSectionUrl = "";
            _validArticleUrl = "";

            //setting up for fetching articles
            _validArticleResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/ContentApiArticle.json");
            _validSectionResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/ContentApiSection.json");
            _validLatestArticlesResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/ContentApiLatestArticles.json");

            //Setup methods
            _mock.Setup(m => m.DownloadArticle(_validArticleUrl)).Returns(Task.FromResult(_validArticleResponse));
            _mock.Setup(m => m.DownloadSection(_validSectionUrl)).Returns(Task.FromResult(_validSectionResponse));
            _mock.Setup(m => m.DownloadLatestArticles()).Returns(Task.FromResult(_validLatestArticlesResponse));

            //Create ArticleController with mock LoginApi
            _articleController = new ArticleController(_mock.Object);
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
        public async void GetBucket1FrontpageShouldReturnArticles()
        {
            //Assert
            Assert.IsNotEmpty(await _articleController.GetBucket1FrontPageAsync());
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
