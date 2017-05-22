using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.Mocks;
using Prototype.UITest.UnitTests.Helpers;
using Prototype.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.UITest.UnitTests.ViewModels
{
    [TestFixture]
    public class LoginViewModelTest
    {
        LoginViewModel _vm;

        [SetUp]
        public void Setup()
        {
            _vm = new LoginViewModel(new StateController());
        }

        [Test]
        public void EmailShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_vm, (x) => x.Email = "newEmail", "Email");
        }

        [Test]
        public void PasswordShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_vm, (x) => x.Password = "newPassword", "Password");
        }
    }
}
