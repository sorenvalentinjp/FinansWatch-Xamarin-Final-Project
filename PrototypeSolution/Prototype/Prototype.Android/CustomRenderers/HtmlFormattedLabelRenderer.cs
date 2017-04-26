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
using Xamarin.Forms.Platform.Android;
using Prototype.Views.CustomRenderers;
using Prototype.Droid.CustomRenderers;
using Xamarin.Forms;
using Android.Text;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(HtmlFormattedLabel), typeof(HtmlFormattedLabelRenderer))]
namespace Prototype.Droid.CustomRenderers
{
    public class HtmlFormattedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            Control.SetMaxLines(1000);
            if(Element.Text != null && Element.Text != "")
            {
                Control.SetText(Html.FromHtml(Element.Text.ToString().Trim()), TextView.BufferType.Spannable);
                Control.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
            }                 
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                if (Element.Text != null && Element.Text != "")
                {
                    Control.SetText(Html.FromHtml(Element.Text), TextView.BufferType.Spannable);
                    Control.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
                }
            }
        }
    }
}