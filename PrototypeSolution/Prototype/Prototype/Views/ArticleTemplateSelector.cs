using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views
{
    public class ArticleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TopArticleTemplate { get; set; }
        public DataTemplate ArticlesTemplate { get; set; }

        public ArticleTemplateSelector()
        {

        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Article)item).Title.Length < 40 ? TopArticleTemplate : ArticlesTemplate;
        }
    }
}
