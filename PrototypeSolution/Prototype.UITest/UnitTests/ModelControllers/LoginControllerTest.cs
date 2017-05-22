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
using Prototype.UITest.Mocks;

namespace Prototype.UITest.UnitTests.ModelControllers
{
    /// <summary>
    /// Testing the LoginController by mocking the calls to LoginApi.
    /// </summary>
    [TestFixture]
    public class LoginControllerTest
    {
        LoginController _loginController;
        string _validUser;
        string _validPassword;
        string _invalidPassword;


        [SetUp]
        public void Setup()
        {
            _loginController = new LoginController(LoginApiMockGenerator.GenerateMock().Object);

            _validUser = "validUser";
            _validPassword = "validPassword";
            _invalidPassword = "invalidPassword";

     }

        [Test]
        public void LoginAsyncValidTokenShouldRaiseLoginEventSucceeded()
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
        public void LoginAsyncInvalidTokenShouldRaiseLoginEventErrorOccured()
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
        public void LogoutEventActionShouldRaiseLoginEventSucceeded()
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
