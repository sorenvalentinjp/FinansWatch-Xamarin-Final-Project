using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Prototype.Database;
using Prototype.UITest.UnitTests.Helpers;

namespace Prototype.UITest.Mocks
{
    public static class ContentApiMockGenerator
    {
        public static Mock<IContentApi> GenerateMock()
        {
            //setting up mock
            var mock = new Mock<IContentApi>();
            var validSectionUrl = "https://content.watchmedier.dk/api/finanswatch/content/latest?hoursago=500&max=30&section=fw_finansnyt_penge";

            //setting up for fetching articles
            var validArticleResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/ContentApiArticle.json");
            var validSectionResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/ContentApiSection.json");
            var validLatestArticlesResponse = ReadJsonFile.GetFileFromDisk("/../../JsonFiles/ContentApiLatestArticles.json");

            //Setup methods
            mock.Setup(m => m.DownloadArticle(It.IsAny<string>())).Returns(Task.FromResult(validArticleResponse));
            mock.Setup(m => m.DownloadSection(validSectionUrl)).Returns(Task.FromResult(validSectionResponse));
            mock.Setup(m => m.DownloadLatestArticles()).Returns(Task.FromResult(validLatestArticlesResponse));

            return mock;
        }
    }
}
