using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.Models;
using Prototype.Helpers;

namespace Prototype.UITest.UnitTests.Helpers
{
    public class ArticleStripperTest
    {
        [Test]
        public void StripArticleTeasersShouldNotThrowExceptionWhenTeasersIsNull()
        {
            //Arrange
            Article article = new Article();

            //Act
            ArticleStripper.StripArticleTeasers(article);

            //Assert
            //no exception should be thrown
        }

        [Test]
        public void StripArticleTeasersShouldStripTheArticlesTeasers()
        {
            //Arrange
            Article article = new Article();
            Teasers teaser = new Teasers() { FRONTPAGE = "front<p>Page</p>Teaser", DEFAULT = "default<p>Teaser</p>" };
            article.teasers = teaser;

            //Act
            ArticleStripper.StripArticleTeasers(article);

            //Assert
            Assert.True(article.teasers.FRONTPAGE.Equals("frontPageTeaser"));
            Assert.True(article.teasers.DEFAULT.Equals("defaultTeaser"));
        }

        [Test]
        public void StripArticleBodyTextShouldNotThrowExceptionWhenBodyTextIsNull()
        {
            //Arrange
            Article article = new Article();

            //Act
            ArticleStripper.StripArticleBodyText(article);

            //Assert
            //no exception should be thrown
        }

        [Test]
        public void StripArticleBodyTextShouldStripTheArticlesBodyText()
        {
            //Arrange
            Article article = new Article() { bodyText = "This<ul>string contains</ul> unordered list html<ul>tags</ul>" };

            //Act
            ArticleStripper.StripArticleBodyText(article);

            //Assert
            Assert.True(article.bodyText.Equals("This unordered list html"));
        }

        [Test]
        public void StripAllHtmlParagraphTagsShouldNotThrowExceptionWhenInputIsNull()
        {
            //Arrange
            string str = null;

            //Act
            ArticleStripper.StripAllHtmlParagraphTags(str);

            //Assert
            //no exception should be thrown
        }

        [Test]
        public void StripAllHtmlParagraphTagsShouldStrippAllHtmlParagraphs()
        {
            //Arrange
            string str = "<p>This</p> string contains <p>some</p> html <p>paragraph tags</p>";

            //Act
            string res = ArticleStripper.StripAllHtmlParagraphTags(str);

            //Assert
            Assert.True(res.Equals("This string contains some html paragraph tags"));
        }

        [Test]
        public void StropRelatedArticlesShouldNotThrowExceptionWhenInputIsNull()
        {
            //Arrange
            string str = null;

            //Act
            ArticleStripper.StripRelatedArticles(str);

            //Assert
            //no exception should be thrown
        }

        [Test]
        public void StripRelatedArticlesShouldStripRelatedArticles()
        {
            //Arrange
            string str = "This<ul>string contains</ul> unordered list html<ul>tags</ul>";

            //Act
            string res = ArticleStripper.StripRelatedArticles(str);

            //Assert
            Assert.True(res.Equals("This unordered list html"));
        }
    }
}
