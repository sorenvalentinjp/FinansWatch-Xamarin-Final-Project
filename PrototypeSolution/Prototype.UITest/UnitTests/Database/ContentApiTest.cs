using NUnit.Framework;
using Prototype.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.UITest.UnitTests.Database
{
    /// <summary>
    /// These test gives little value as they more or less only tests, that we are using the HttpClient properly.
    /// </summary>
    [TestFixture]
    public class ContentApiTest
    {
        ContentApi _contentApi;
        string _validArticleUrl;
        string _validFrontPageArticleUrl;
        string _validLatestArticleUrl;
        string _validSectionUrl;
        string _invalidUrl;

        [TestFixtureSetUp]
        public void Init()
        {
            _contentApi = new ContentApi();
            _validArticleUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9517468";
            _validFrontPageArticleUrl = "https://content.watchmedier.dk/api/finanswatch/content/frontpagearticles";
            _validLatestArticleUrl = "https://content.watchmedier.dk/api/finanswatch/content/latest?hoursago=168&max=10";
            _validSectionUrl = "https://content.watchmedier.dk/api/finanswatch/content/latest?hoursago=500&max=30&section=fw_finansnyt_penge";
            _invalidUrl = "https://thiswebsitedoesnotexisteveeeeeeeeeeeeer.dk";
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            _contentApi.DisposeClient();
        }

        [Test]
        public async void DownloadJsonWithValidUriShouldReturnNotNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_validFrontPageArticleUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public async void DownloadJsonWithInvalidUriShouldReturnNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_invalidUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNullOrEmpty(result);
        }

        [Test]
        public async void DownloadLatestArticlesValidShouldReturnNotNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_validLatestArticleUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public async void DownloadLatestArticlesInvalidShouldReturnNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_invalidUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNullOrEmpty(result);
        }

        [Test]
        public async void DownloadSectionValidShouldReturnNotNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_validSectionUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public async void DownloadSectionInvalidShouldReturnNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_invalidUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNullOrEmpty(result);
        }

        [Test]
        public async void DownloadArticleValidShouldNotReturnNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_validArticleUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public async void DownloadArticleInvalidShouldReturnNullOrEmptyResult()
        {
            //arrange
            Uri uri = new Uri(_invalidUrl);

            //act
            string result = await _contentApi.DownloadJson(uri);

            //assert
            Assert.IsNullOrEmpty(result);
        }
    }
}
