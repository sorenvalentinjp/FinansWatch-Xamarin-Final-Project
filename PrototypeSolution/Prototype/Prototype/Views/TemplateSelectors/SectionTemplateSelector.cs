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
        public DataTemplate LargeCellTopImageTemplate { get; set; }
        public DataTemplate MediumCellTopImageTemplate { get; set; }
        public DataTemplate MediumCellNoImageTemplate { get; set; }

        public SectionTemplateSelector(StateController stateController)
        {
            this.MediumCellTopImageTemplate = new DataTemplate(() => new MediumCellTopImage(stateController));
            this.LargeCellTopImageTemplate = new DataTemplate(() => new LargeCellTopImage(stateController));
            this.MediumCellNoImageTemplate = new DataTemplate(() => new MediumCellNoImage(stateController));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)item;

            if (articleViewModel.Article.isTopArticle)
            {
                    return this.LargeCellTopImageTemplate;
            }
            else
            {
                if(articleViewModel.Article.topImage != null)
                {
                    return this.MediumCellTopImageTemplate;
                }
                else
                {
                    return this.MediumCellNoImageTemplate;
                }
            }
        }
    }
}
