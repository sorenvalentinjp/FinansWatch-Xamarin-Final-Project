using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Prototype.Database;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.UnitTests.Helpers;

namespace Prototype.UITest.Mocks
{
    public static class LoginApiMockGenerator
    {
        public static Mock<ILoginApi> GenerateMock()
        {
            var mockLoginApi = new Mock<ILoginApi>();

            var validTokenResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/LoginApi_ValidTokenResponse.json");
            var invalidTokenResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/LoginApi_InvalidTokenResponse.json");
            var validUser = "validUser";
            var validPassword = "validPassword";
            var invalidPassword = "invalidPassword";

            mockLoginApi.Setup(m => m.DownloadLoginToken(validUser, validPassword)).Returns(Task.FromResult(validTokenResponse));
            mockLoginApi.Setup(m => m.DownloadLoginToken(validUser, invalidPassword)).Returns(Task.FromResult(invalidTokenResponse));

            //setting up for fetching subscriber using a token
            var validSubscriberResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/LoginApi_ValidSubscriberResponse.json");

            mockLoginApi.Setup(m => m.DownloadSubscriber(It.IsAny<SubscriberToken>())).Returns(Task.FromResult(validSubscriberResponse));

            return mockLoginApi;
        }
    }
}
