using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.Models;

namespace Prototype.UITest.UnitTests.Helpers
{
    public class ArticleStripperTest
    {
        [Test]
        public void StripArticleShouldStripArticle()
        {
            //Prepare
            Article article = new Article { contentUrl = "https://content.watchmedier.dk/api/finanswatch/content/article/9590291" };

            //Act

            //Assert
            Assert.IsNull(article.topImage);

        }

        [Test]
        public void StripAllHtmlParagraphTagsShouldStrippAllHtmlParagraphs()
        {
            //Prepare

            //Assert

        }

        [Test]
        public void StripRelatedArticlesShouldStripRelatedArticles()
        {
            //Prepare

            //Assert

        }
    }
}
