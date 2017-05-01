using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using FFImageLoading.Forms.Touch;

namespace Prototype.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
            CachedImageRenderer.Init();

            //MR.Gesture license key setup
            MR.Gestures.iOS.Settings.LicenseKey = "3HU6-9KQV-UPUG-DSHV-WRA8-W6CL-9DTZ-5L5P-PMXM-942C-97HX-7JG6-Y523";

            LoadApplication (new Prototype.App ());

            return base.FinishedLaunching (app, options);
		}
	}
}
