using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.Models;

namespace Prototype.UITest.UnitTests.Models
{
    public class GroupingTest
    {

        [Test]
        public void GroupingShouldContain()
        {
            //Prepare
            var article1 = new Article();
            var article2 = new Article();
            var article3 = new Article();
            var article4 = new Article();

            var articleList = new List<Article>();
            var articleList2 = new List<Article>();

            articleList.Add(article1);
            articleList.Add(article2);

            articleList2.Add(article3);
            articleList2.Add(article4);

            string groupingKey = "Group1";
            var grouping = new Grouping<string, Article>(groupingKey, articleList);

            string groupingKey2 = "Group2";
            var grouping2 = new Grouping<string, Article>(groupingKey2, articleList2);

            //Assert
            Assert.AreEqual(grouping.Count, 2);
            Assert.AreEqual(grouping2.Count, 2);

            Assert.Contains(article1, grouping);
            Assert.Contains(article2, grouping);
            Assert.Contains(article3, grouping2);
            Assert.Contains(article4, grouping2);

        }
    }
}
