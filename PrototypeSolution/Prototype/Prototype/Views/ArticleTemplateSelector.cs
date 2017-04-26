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
        public DataTemplate TopArticleNoImgTemplate { get; set; }
        public DataTemplate ArticlesTemplate { get; set; }
        public DataTemplate ArticlesNoImgTemplate { get; set; }

        public ArticleTemplateSelector() { }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if(article.IsTopArticle)
            {
                if (article.ImageBigURL == "")
                    return TopArticleNoImgTemplate;
                else
                    return TopArticleTemplate;
            }
            else
            {
                if (article.ImageSmallURL == "")
                    return ArticlesNoImgTemplate;
                else
                    return ArticlesTemplate;
            }
        }
    }
}
