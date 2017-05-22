using Moq;
using NUnit.Framework;
using Prototype.Database;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.UnitTests.Helpers;
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
        public void Init()
        {
            _mockLoginApi = new Mock<ILoginApi>();
            _loginController = new LoginController(_mockLoginApi.Object);

            _validTokenResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/LoginApi_ValidTokenResponse.json");
            _invalidTokenResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/LoginApi_InvalidTokenResponse.json");
            _validUser = "validUser";
            _validPassword = "validPassword";
            _invalidPassword = "invalidPassword";

            _mockLoginApi.Setup(m => m.DownloadLoginToken(_validUser, _validPassword)).Returns(Task.FromResult(_validTokenResponse));
            _mockLoginApi.Setup(m => m.DownloadLoginToken(_validUser, _invalidPassword)).Returns(Task.FromResult(_invalidTokenResponse));

            //setting up for fetching subscriber using a token
            _validSubscriberResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/LoginApi_ValidSubscriberResponse.json");

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
