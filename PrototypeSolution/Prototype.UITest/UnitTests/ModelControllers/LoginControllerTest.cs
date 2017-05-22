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
        string _validUser;
        string _validPassword;
        string _invalidPassword;
        string _validTokenResponse;
        string _invalidTokenResponse;
        string _validSubscriberResponse;

        [SetUp]
        public void Setup()
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

            _mockLoginApi.Setup(m => m.DownloadSubscriber(It.IsAny<SubscriberToken>())).Returns(Task.FromResult(_validSubscriberResponse));
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
        public void LoginAsyncInvalidToken()
        {
            //arrange
            bool loginEventSuccededIsInvoked = false;
            bool loginEventErrorOccuredIsInvoked = false;
            _loginController.LoginEventSucceeded += (subscriber) => { loginEventSuccededIsInvoked = true; };
            _loginController.LoginEventErrorOccured += (error) => { loginEventErrorOccuredIsInvoked = true; };

            //act
            _loginController.LoginAsync(_validUser, _invalidPassword);

            //assert
            Assert.False(loginEventSuccededIsInvoked);
            Assert.True(loginEventErrorOccuredIsInvoked);
        }

        [Test]
        public void LogoutEventAction()
        {
            //arrange
            bool loginEventSuccededIsInvoked = false;
            _loginController.LoginEventSucceeded += (subscriber) => { loginEventSuccededIsInvoked = true; };

            //act
            _loginController.LogoutEventAction();

            //assert
            Assert.IsNull(_loginController.Subscriber);
            Assert.True(loginEventSuccededIsInvoked);
        }
    }
}
