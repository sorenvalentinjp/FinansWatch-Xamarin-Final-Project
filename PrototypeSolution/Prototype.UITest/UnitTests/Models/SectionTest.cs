using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.Models;
using Prototype.UITest.UnitTests.Helpers;

namespace Prototype.UITest.UnitTests.Models
{
    public class SectionTest
    {
        [Test]
        public void SubscriberShouldHaveAccesToSite()
        {
            //Prepare
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
            //Prepare
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
            //Prepare
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
