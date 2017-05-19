using Moq;
using NUnit.Framework;
using Prototype.Database;
using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.UITest.UnitTests.ModelControllers
{
    [TestFixture]
    public class LoginControllerTest
    {
        LoginController _loginController;
        Mock<ILoginApi> _mockLoginApi;
        string _validTokenResponse;
        string _invalidTokenResponse;
        string _validUser;
        string _validPassword;
        string _invalidPassword;
        SubscriberToken _token;
        string _validSubscriberResponse;

        [SetUp]
        public void init()
        {
            _mockLoginApi = new Mock<ILoginApi>();
            _loginController = new LoginController(_mockLoginApi.Object);

            _validTokenResponse = "{\"token\":\"721cb6c8-c670-4082-a674-f283eac296a1\",\"error\":{\"friendlyErrorText\":\"\",\"code\":0,\"description\":\"\",\"name\":\"\"}}";
            _invalidTokenResponse = "{\"token\":null,\"error\":{\"friendlyErrorText\":\"Login mislykkedes. Forkert email eller adgangskode.\",\"code\":2,\"description\":\"Ugyldigt login forsï¿½g\",\"name\":\"Login fejl\"}}";
            _validUser = "validUser";
            _validPassword = "validPassword";
            _invalidPassword = "invalidPassword";

            _mockLoginApi.Setup(m => m.DownloadLoginToken(_validUser, _validPassword)).Returns(Task.FromResult(_validTokenResponse));
            _mockLoginApi.Setup(m => m.DownloadLoginToken(_validUser, _invalidPassword)).Returns(Task.FromResult(_invalidTokenResponse));

            //setting up for fetching subscriber using a token
            _validSubscriberResponse = "{\"featureAccessList\":[{\"access\":true,\"denyReasonText\":\"\",\"obtainAccessText\":\"\",\"code\":\"FINANSWATCH\",\"expiration\":1524952800000,\"name\":\"Adgang til lï¿½st indhold\"}],\"error\":{\"friendlyErrorText\":\"\",\"code\":0,\"description\":\"\",\"name\":\"\"},\"user\":{\"userId\":123123,\"email\":\"hans.hansen@gmail.com\",\"name\":\"Hans Hansen\"}}";

            Error myError = new Error() { code = -1 };

            _token = new SubscriberToken() { error = myError, token = "721cb6c8-c670-4082-a674-f283eac296a1" };

            _mockLoginApi.Setup(m => m.DownloadSubscriber(_token)).Returns(Task.FromResult(_validSubscriberResponse));
        }

        [Test]
        public void LoginAsyncValidToken()
        {
            //arrange
            bool loginEventSuccededIsInvoked = false;
            bool loginEventErrorOccuredIsInvoked = false;
            _loginController.LoginEventSucceeded += (subscriber) => { loginEventSuccededIsInvoked = true; };
            _loginController.LoginEventErrorOccured += (error) => { loginEventErrorOccuredIsInvoked = true; };

            //act
            _loginController.LoginAsync(_validUser, _validPassword);

            //assert
            Assert.True(loginEventSuccededIsInvoked);
            Assert.False(loginEventErrorOccuredIsInvoked);
        }

        [Test]
        public void LogoutEventAction()
        {
            //arrange
            LoginController loginController = new LoginController() { Subscriber = new Subscriber() };
            bool isInvoked = false;
            loginController.LoginEventSucceeded += (subscriber) => { isInvoked = true; };
            
            //act
            loginController.LogoutEventAction();

            //assert
            Assert.IsNull(loginController.Subscriber);
            Assert.True(isInvoked);
        }
    }
}
