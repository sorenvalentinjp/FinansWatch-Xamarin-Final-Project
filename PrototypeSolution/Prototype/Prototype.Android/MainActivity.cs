using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;

namespace Prototype.Droid
{
	[Activity (Label = "Prototype", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

            CachedImageRenderer.Init();

            global::Xamarin.Forms.Forms.Init (this, bundle);

            //MR.Gesture license key setup
            MR.Gestures.Android.Settings.LicenseKey = "3HU6-9KQV-UPUG-DSHV-WRA8-W6CL-9DTZ-5L5P-PMXM-942C-97HX-7JG6-Y523";
            LoadApplication (new Prototype.App ());

            
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 0, 0));            
        }
	}
}

