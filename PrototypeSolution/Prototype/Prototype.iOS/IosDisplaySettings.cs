using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Prototype.iOS
{
    public class IosDisplaySettings : IDisplaySettings
    {
        public int GetHeight()
        {
            return (int)UIScreen.MainScreen.Bounds.Height;
        }

        public int GetWidth()
        {
            return (int)UIScreen.MainScreen.Bounds.Width;
        }
    }
}