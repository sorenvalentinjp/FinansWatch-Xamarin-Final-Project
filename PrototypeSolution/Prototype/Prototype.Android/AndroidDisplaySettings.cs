using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;

namespace Prototype.Droid
{
    public class AndroidDisplaySettings : IDisplaySettings
    {
        public int GetHeight()
        {
            return Resources.System.DisplayMetrics.HeightPixels;
        }

        public int GetWidth()
        {
            return Resources.System.DisplayMetrics.WidthPixels;
        }
    }
}