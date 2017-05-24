using Foundation;
using Prototype.iOS.CustomRenderers;
using Prototype.Views.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HtmlFormattedLabel), typeof(HtmlFormattedLabelRenderer))]
namespace Prototype.iOS.CustomRenderers
{
    class HtmlFormattedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var view = (HtmlFormattedLabel)Element;
            if (view == null) return;
			if (view.Text == null) return;

            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;
            attr.StringEncoding = NSStringEncoding.UTF8;

            var myHtmlData = NSData.FromString(Element.Text, NSStringEncoding.UTF8);
            Control.Lines = 1000;
            Control.AttributedText = new NSAttributedString(myHtmlData, attr, ref nsError);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                if (Control != null && Element != null && !string.IsNullOrWhiteSpace(Element.Text))
                {
                    var attr = new NSAttributedStringDocumentAttributes();
                    var nsError = new NSError();
                    attr.DocumentType = NSDocumentType.HTML;
                    attr.StringEncoding = NSStringEncoding.UTF8;

                    if (Element.Text != null && Element.Text != "")
                    {
                        var myHtmlData = NSData.FromString(Element.Text, NSStringEncoding.UTF8);
                        Control.Lines = 0;
                        Control.AttributedText = new NSAttributedString(myHtmlData, attr, ref nsError);
                    }
                }
            }
        }
    }
}
