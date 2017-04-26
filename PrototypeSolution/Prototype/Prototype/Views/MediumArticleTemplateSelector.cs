using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views
{
    /// <summary>
    /// This class defines which template a given article should be using in related articles listviews and similar medium sized cell listviews..
    /// </summary>
    public class MediumArticleTemplateSelector : DataTemplateSelector
    {

        public DataTemplate ArticleMediumTemplate { get; set; }
        public DataTemplate ArticleMediumNoImageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.ImageSmallURL == "")
                return ArticleMediumNoImageTemplate;
            else
                return ArticleMediumTemplate;
        }
    }
}
