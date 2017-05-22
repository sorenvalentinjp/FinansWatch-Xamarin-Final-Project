using NUnit.Framework;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.UITest.UnitTests.Models
{
    public class SubscriberTest
    {
        [Test]
        public void SubscriberShouldHaveAccesToSite()
        {
            //arrange
            var subscriber = new Subscriber();
            var featureAccessList = new FeatureAccessList { access = true, code = "FINANSWATCH" };
            subscriber.featureAccessList = new List<FeatureAccessList>();
            subscriber.featureAccessList.Add(featureAccessList);

            //Act
            var hasAcces = subscriber.HasAccessToSite();

            //Assert
            Assert.True(hasAcces);
        }

        [Test]
        public void SubscriberShouldNotHaveAccesToSite()
        {
            //arrange
            var subscriber = new Subscriber();
            var featureAccessList = new FeatureAccessList { access = true, code = "falsesite" };
            subscriber.featureAccessList = new List<FeatureAccessList>();
            subscriber.featureAccessList.Add(featureAccessList);

            //Act
            var hasAcces = subscriber.HasAccessToSite();

            //Assert
            Assert.False(hasAcces);
        }

        [Test]
        public void SubscriberShouldNotHaveAccesToSite2()
        {
            //arrange
            var subscriber = new Subscriber();
            var featureAccessList = new FeatureAccessList { access = false, code = "finanswatch" };
            subscriber.featureAccessList = new List<FeatureAccessList>();
            subscriber.featureAccessList.Add(featureAccessList);

            //Act
            var hasAcces = subscriber.HasAccessToSite();

            //Assert
            Assert.False(hasAcces);
        }
    }
}
