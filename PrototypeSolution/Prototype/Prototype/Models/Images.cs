using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Models
{
    /// <summary>
    /// JSON Generated class that is used in Article
    /// </summary>
    public class Image
    {
        public string imageCaption { get; set; }
        public Versions versions { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class Versions
    {
        public FrontpageLarge380 frontpage_large_380 { get; set; }
        public D d { get; set; }
        public MediumFrontpage300 medium_frontpage_300 { get; set; }
        public E e { get; set; }
        public SmallArticle220 small_article_220 { get; set; }
        public BigArticle460 big_article_460 { get; set; }
        public F f { get; set; }
        public HugeArticle620 huge_article_620 { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class FrontpageLarge380
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class D
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class MediumFrontpage300
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class E
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class SmallArticle220
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class BigArticle460
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class F
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in Image
    /// </summary>
    public class HugeArticle620
    {
        public string url { get; set; }
    }


    /// <summary>
    /// JSON Generated class that is used in Article
    /// </summary>
    public class TopImage
    {
        public bool primary { get; set; }
        public string imageCaption { get; set; }
        public string id { get; set; }
        public Big big { get; set; }
        public Small small { get; set; }
        public Thumb thumb { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in TopImage
    /// </summary>
    public class Big
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in TopImage
    /// </summary>
    public class Small
    {
        public string url { get; set; }
    }

    /// <summary>
    /// JSON Generated class that is used in TopImage
    /// </summary>
    public class Thumb
    {
        public string url { get; set; }
    }



}
