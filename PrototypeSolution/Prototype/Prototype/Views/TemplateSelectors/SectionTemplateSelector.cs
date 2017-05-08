using System;
using System.Collections.Generic;
using System.Text;
using Prototype.Models;
using Xamarin.Forms;
using Prototype.ModelControllers;
using Prototype.ViewModels;
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

        public SectionTemplateSelector(StateController stateController)
        {
            this.MediumCellFrontPageImageTemplate = new DataTemplate(() => new MediumCellFrontPageImage(stateController));
            this.MediumCellNoImageTemplate = new DataTemplate(() => new MediumCellNoImage(stateController));
            this.LargeCellFrontPageImageTemplate = new DataTemplate(() => new LargeCellFrontPageImage(stateController));
            this.LargeCellNoImageTemplate = new DataTemplate(() => new LargeCellNoImage(stateController));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)item;

            if (articleViewModel.Article.isTopArticle)
            {
                if (articleViewModel.Article.image == null)
                    return this.LargeCellNoImageTemplate;
                else
                    return this.LargeCellFrontPageImageTemplate;
            }
            else
            {
                if (articleViewModel.Article.image == null)
                    return this.MediumCellNoImageTemplate;
                else
                    return this.MediumCellFrontPageImageTemplate;
            }
        }
    }
}
