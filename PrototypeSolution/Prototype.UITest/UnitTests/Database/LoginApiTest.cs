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
    [TestFixture]
    public class LoginApiTest
    {
        LoginApi _loginApi;
        string _validUrl;
        string _invalidUrl;

        [TestFixtureSetUp]
        public void Init()
        {
            _loginApi = new LoginApi();
            _validUrl = "https://secure.finanswatch.dk/GetUserAccessInfo?token=";
            _invalidUrl = "https://thiswebsitedoesnotexisteveeeeeeeeeeeeer.dk";
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            _loginApi.DisposeClient();
        }

        [Test]
        public async void DownloadJsonWithValidUri()
        {
            //arrange
            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_validUrl),
                Method = HttpMethod.Get
            };

            //act
            string result = await _loginApi.DownloadJson(request);

            //assert
            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public async void DownloadJsonWithInvalidUri()
        {
            //arrange
            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_invalidUrl),
                Method = HttpMethod.Get
            };

            //act
            string result = await _loginApi.DownloadJson(request);

            //assert
            Assert.IsNullOrEmpty(result);
        }

        [Test]
        public async void DownloadLoginToken()
        {

        }
    }
}
