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
        public DataTemplate LargeCellFrontPageImageTemplate { get; set; }
        public DataTemplate LargeCellNoImageTemplate { get; set; }
        public DataTemplate MediumCellFrontPageImageTemplate { get; set; }
        public DataTemplate MediumCellNoImageTemplate { get; set; }

        public SectionTemplateSelector(StateController stateController, ContentPage page)
        {
            this.MediumCellFrontPageImageTemplate = new DataTemplate(() => { return new MediumCellFrontPageImage(stateController, page); });
            this.MediumCellNoImageTemplate = new DataTemplate(() => { return new MediumCellNoImage(stateController, page); });
            this.LargeCellFrontPageImageTemplate = new DataTemplate(() => { return new LargeCellFrontPageImage(stateController, page); });
            this.LargeCellNoImageTemplate = new DataTemplate(() => { return new LargeCellNoImage(stateController, page); });
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.isTopArticle)
            {
                if (article.image == null)
                    return this.LargeCellNoImageTemplate;
                else
                    return this.LargeCellFrontPageImageTemplate;
            }
            else
            {
                if (article.image == null)
                    return this.MediumCellNoImageTemplate;
                else
                    return this.MediumCellFrontPageImageTemplate;
            }
        }
    }
}
