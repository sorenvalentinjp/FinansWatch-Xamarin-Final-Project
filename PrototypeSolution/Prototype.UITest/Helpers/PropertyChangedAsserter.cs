using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Prototype.UITest.UnitTests.Helpers
{
    public static class PropertyChangedAsserter
    {

        public static void AssertPropertyChanged<T>(T instance, Action<T> actionPropertySetter, string propertyName) where T : INotifyPropertyChanged
        {
            string actual = null;
            instance.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                actual = e.PropertyName;
            };
            actionPropertySetter.Invoke(instance);
            Assert.IsNotNull(actual);
            Assert.AreEqual(propertyName, actual);
        }
    }
}
