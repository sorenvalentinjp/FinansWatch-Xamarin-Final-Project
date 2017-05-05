using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views.Converters
{
    /// <summary>
    /// Returns true if the value is not null, returns false if the value is null
    /// </summary>
    public class NullToBooleanConverter : IValueConverter, IMarkupExtension
    {
        /// <summary>
        /// Returns true if the value is not null, returns false if the value is null
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
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
