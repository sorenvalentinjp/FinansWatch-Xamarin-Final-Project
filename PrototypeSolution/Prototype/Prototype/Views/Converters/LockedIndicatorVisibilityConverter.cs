using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views.Converters
{
    public class UnlockedIndicatorVisibilityConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isArticleLocked = (bool)value;
            var isSubscriberLoggedIn = App.IsSubscriberLoggedIn();
            return isArticleLocked && isSubscriberLoggedIn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class LockedIndicatorVisibilityConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isArticleLocked = (bool)value;
            var isSubscriberLoggedIn = App.IsSubscriberLoggedIn();
            return isArticleLocked && !isSubscriberLoggedIn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
