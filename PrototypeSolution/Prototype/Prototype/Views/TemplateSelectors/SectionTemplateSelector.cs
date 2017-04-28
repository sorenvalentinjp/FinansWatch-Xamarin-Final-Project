using System;
using System.Collections.Generic;
using System.Text;
using Prototype.Models;
using Xamarin.Forms;

namespace Prototype.Views.TemplateSelectors
{
    /// <summary>
    /// This class defines which template a given article should be using on the frontpage.
    /// </summary>
    public class SectionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ArticleLargeTemplate { get; set; }
        public DataTemplate ArticleLargeNoImageTemplate { get; set; }
        public DataTemplate ArticleMediumTemplate { get; set; }
        public DataTemplate ArticleMediumNoImageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.IsTopArticle)
            {
                if (article.FrontPageImage == null)
                    return ArticleLargeNoImageTemplate;
                else
                    return ArticleLargeTemplate;
            }
            else
            {
                if (article.FrontPageImage == null)
                    return ArticleMediumNoImageTemplate;
                else
                    return ArticleMediumTemplate;
            }
        }
    }
}
