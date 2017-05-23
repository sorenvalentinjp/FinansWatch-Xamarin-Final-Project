using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Prototype.Views.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter, IMarkupExtension
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var byteArray = (byte[])value;
            if (byteArray != null)
            {
                return (StreamImageSource)Xamarin.Forms.ImageSource.FromStream(
                    () => new MemoryStream(byteArray));
            }
            else
            {
                return ImageFromFileExtension.GenerateImageSourceFromFileSource("imageTransparentSmall.png");
            }
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
