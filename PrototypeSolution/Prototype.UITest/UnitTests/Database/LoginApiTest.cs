using Moq;
using NUnit.Framework;
using Prototype.Database;
using Prototype.Models;
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
        Mock<ILoginApi> _mock;

        LoginApi _loginApi;
        string _validUrl;
        string _invalidUrl;

        string _validTokenResponse;
        string _invalidTokenResponse;
        string _validUser;
        string _validPassword;
        string _invalidPassword;

        SubscriberToken _token;
        string _validSubscriberResponse;

        [TestFixtureSetUp]
        public void Init()
        {
            _loginApi = new LoginApi();
            _validUrl = "https://secure.finanswatch.dk/GetUserAccessInfo?token=";
            _invalidUrl = "https://thiswebsitedoesnotexisteveeeeeeeeeeeeer.dk";

            //setting up mock
            _mock = new Mock<ILoginApi>();

            //setting up for fetching token
            _validTokenResponse = "{\"token\":\"721cb6c8-c670-4082-a674-f283eac296a1\",\"error\":{\"friendlyErrorText\":\"\",\"code\":0,\"description\":\"\",\"name\":\"\"}}";
            _invalidTokenResponse = "{\"token\":null,\"error\":{\"friendlyErrorText\":\"Login mislykkedes. Forkert email eller adgangskode.\",\"code\":2,\"description\":\"Ugyldigt login forsï¿½g\",\"name\":\"Login fejl\"}}";
            _validUser = "validUser";
            _validPassword = "validPassword";
            _invalidPassword = "invalidPassword";
            
            _mock.Setup(m => m.DownloadLoginToken(_validUser, _validPassword)).Returns(Task.FromResult(_validTokenResponse));
            _mock.Setup(m => m.DownloadLoginToken(_validUser, _invalidPassword)).Returns(Task.FromResult(_invalidTokenResponse));

            //setting up for fetching subscriber using a token
            _validSubscriberResponse = "{\"featureAccessList\":[{\"access\":true,\"denyReasonText\":\"\",\"obtainAccessText\":\"\",\"code\":\"FINANSWATCH\",\"expiration\":1524952800000,\"name\":\"Adgang til lï¿½st indhold\"}],\"error\":{\"friendlyErrorText\":\"\",\"code\":0,\"description\":\"\",\"name\":\"\"},\"user\":{\"userId\":123123,\"email\":\"hans.hansen@gmail.com\",\"name\":\"Hans Hansen\"}}";
            _token = new SubscriberToken() { error = null, token = "721cb6c8-c670-4082-a674-f283eac296a1" };

            _mock.Setup(m => m.DownloadSubscriber(_token)).Returns(Task.FromResult(_validSubscriberResponse));
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
            //arrange
            string responseValid;
            string responseInvalid;

            //act
            responseValid = await _mock.Object.DownloadLoginToken(_validUser, _validPassword);
            responseInvalid = await _mock.Object.DownloadLoginToken(_validUser, _invalidPassword);

            //assert
            Assert.IsTrue(responseValid.Equals(_validTokenResponse));
            Assert.IsTrue(responseInvalid.Equals(_invalidTokenResponse));
        }

        [Test]
        public async void DownloadSubscriber()
        {
            //arrange
            string responseValid;

            //act
            responseValid = await _mock.Object.DownloadSubscriber(_token);

            //assert
            Assert.IsTrue(responseValid.Equals(_validSubscriberResponse));
        }
    }
}
