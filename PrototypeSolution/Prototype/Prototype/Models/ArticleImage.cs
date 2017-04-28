using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Models
{
    public class ArticleImage
    {
        public string BigUrl { get; set; }
        public string SmallUrl { get; set; }
        public string ThumbUrl { get; set; }
        public string ImageCaption { get; set; }

        public ArticleImage(string bigUrl, string smallUrl, string thumbUrl, string imageCaption)
        {
            BigUrl = bigUrl;
            SmallUrl = smallUrl;
            ThumbUrl = thumbUrl;
            ImageCaption = imageCaption;
        }

    }
}
