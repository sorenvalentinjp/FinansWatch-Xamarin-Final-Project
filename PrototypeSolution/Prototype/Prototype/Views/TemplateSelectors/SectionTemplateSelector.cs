using System;
using System.Collections.Generic;
using System.Text;
using Prototype.Models;
using Xamarin.Forms;
using Prototype.ModelControllers;
using Prototype.Views.Cells;

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

        public SectionTemplateSelector(StateController stateController, ContentPage page)
        {
            this.ArticleMediumTemplate = new DataTemplate(() =>
            {
                return new MediumCell(stateController, page);
            });

            this.ArticleMediumNoImageTemplate = new DataTemplate(() =>
            {
                return new MediumCellNoImage(stateController, page);
            });

            this.ArticleLargeTemplate = new DataTemplate(() =>
            {
                return new LargeCell(stateController, page);
            });

            this.ArticleLargeNoImageTemplate = new DataTemplate(() =>
            {
                return new LargeCellNoImage(stateController, page);
            });
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.isTopArticle)
            {
                if (article.image == null)
                    return ArticleLargeNoImageTemplate;
                else
                    return ArticleLargeTemplate;
            }
            else
            {
                if (article.image == null)
                    return ArticleMediumNoImageTemplate;
                else
                    return ArticleMediumTemplate;
            }
        }
    }
}
