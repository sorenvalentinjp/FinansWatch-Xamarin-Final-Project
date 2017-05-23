using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;

namespace Prototype.Views.Helpers
{
    [ContentProperty("Source")]
    public class ImageFromFileExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            return GenerateImageSourceFromFileSource(Source);
        }

        public static ImageSource GenerateImageSourceFromFileSource(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return ImageSource.FromResource(source);
            }
            else return null;
        }
    }
}
