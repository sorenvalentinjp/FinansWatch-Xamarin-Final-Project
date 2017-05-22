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

namespace Prototype.UITest.UnitTests.ModelControllers
{
    [TestFixture]
    public class ArticleControllerTest
    {
        ArticleController _articleController;

        [SetUp]
        public void Setup()
        {
            _articleController = ArticleControllerMockGenerator.GenerateMock();
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
            //Prepare
            IList<Article> articles = new List<Article>();

            //Act
            articles = await _articleController.GetBucket1FrontPageAsync();

            //Assert
            Assert.IsNotEmpty(articles);
        }

        [Test]
        public async void GetBucket2ShouldPopulateLatestArticlesAndAllSections()
        {
            //Prepare
            await  _articleController.GetBucket1FrontPageAsync();

            //Act
            await _articleController.GetBucket2Async();

            //Assert
            foreach (var section in _articleController.Sections)
            {
                Assert.IsNotEmpty(section.Articles);
            }
            Assert.IsNotEmpty(_articleController.LatestArticles);
        }

        [Test]
        public async void GetArticlesAndDetailsForSectionShouldReturnSectionWithArticles()
        {
            //Act
            IList<Article> articles = await _articleController.GetArticlesAndDetailsForSectionAsync(_articleController.Sections.FirstOrDefault());
            
            //Assert
            Assert.IsNotEmpty(articles);
            Assert.IsNotNull(articles.FirstOrDefault().bodyText);


        }

        [Test]
        public async void GetArticleDetailsForCollectionShouldReturnArticles()
        {
            //Prepare
            IList<Article> articles = await _articleController.GetSectionArticlesAsync(_articleController.Sections.FirstOrDefault());

            //Act
            articles = await _articleController.GetArticleDetailsForCollectionAsync(articles);

            //Assert
            Assert.IsNotEmpty(articles);
            Assert.IsNotNull(articles.FirstOrDefault().bodyText);

        }

        [Test]
        public async void GetLatestArticlesAsyncShouldReturnLatestArticles()
        {
            //Prepare
            IList<Article> articles = await _articleController.GetLatestArticlesAsync();

            //Assert
            Assert.IsNotEmpty(articles);
        }

        [Test]
        public async void GetRelatedArticlesAsyncShouldReturnRelatedArticles()
        {
            //Prepare
            Article article = new Article {contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291" };
            article = await _articleController.GetArticleDetailsAsync(article);

            //Act
            IList<Article> relatedArticles = await _articleController.GetRelatedArticlesAsync(article);

            //Assert
            Assert.IsNotEmpty(relatedArticles);

        }

        [Test]
        public async void GetArticleDetailsAsyncWithArticleShouldReturnDetailedArticle()
        {
            //Prepare
            Article article = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291" };

            //Act
            article = await _articleController.GetArticleDetailsAsync(article);

            //Assert
            Assert.IsNotNull(article.bodyText);

        }

        [Test]
        public async void GetArticleDetailsAsyncWithStringShouldReturnDetailedArticle()
        {
            //Prepare
            Article article = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291" };

            //Act
            article = await _articleController.GetArticleDetailsAsync(article.contentUrl);

            //Assert
            Assert.IsNotNull(article.bodyText);
        }

        [Test]
        public void AddOrRemoveSavedArticleShouldAddArticle()
        {
            //Prepare
            Article article = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291" };
            Article article2 = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9591111" };

            //Act
            _articleController.AddOrRemoveSavedArticle(article);
            _articleController.AddOrRemoveSavedArticle(article2);

            //Assert
            Assert.Contains(article, _articleController.SavedArticles);
            Assert.Contains(article2, _articleController.SavedArticles);


        }

        [Test]
        public void AddOrRemoveSavedArticleShouldRemoveArticle()
        {
            //Prepare
            Article article = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291" };
            Article article2 = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9591111" };
            _articleController.AddOrRemoveSavedArticle(article);
            _articleController.AddOrRemoveSavedArticle(article2);

            //Act
            _articleController.AddOrRemoveSavedArticle(article);
            _articleController.AddOrRemoveSavedArticle(article2);

            //Assert
            CollectionAssert.DoesNotContain(_articleController.SavedArticles, article);
            CollectionAssert.DoesNotContain(_articleController.SavedArticles, article2);

        }

        [Test]
        public async void PrepareArticleShouldSetTopImage()
        {
            //Prepare
            List<TopImage> topImages = new List<TopImage>();
            TopImage topImage = new TopImage();
            topImages.Add(topImage);
            Article article = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291", topImages = topImages };

            //Act
            _articleController.PrepareArticle(article);

            //Assert
            Assert.IsNotNull(article.topImage);

        }

        [Test]
        public async void PrepareArticleShouldNotSetTopImage()
        {
            //Prepare
            Article article = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291"};

            //Act
            _articleController.PrepareArticle(article);

            //Assert
            Assert.IsNull(article.topImage);
        }       
    }
}
