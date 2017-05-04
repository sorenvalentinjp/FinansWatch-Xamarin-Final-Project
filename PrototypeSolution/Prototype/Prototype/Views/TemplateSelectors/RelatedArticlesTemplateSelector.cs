using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.Cells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views.TemplateSelectors
{
    /// <summary>
    /// This class defines which template a given article should be using in related articles listviews and similar medium sized cell listviews..
    /// </summary>
    public class RelatedArticlesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MediumCellTopImageTemplate { get; set; }
        public DataTemplate MediumCellNoImageTemplate { get; set; }

        public RelatedArticlesTemplateSelector(StateController stateController, Page page)
        {
            this.MediumCellTopImageTemplate = new DataTemplate(() => { return new MediumCellTopImage(stateController, page); });
            this.MediumCellNoImageTemplate = new DataTemplate(() => { return new MediumCellNoImage(stateController, page); });            
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.topImage == null)
                return this.MediumCellNoImageTemplate;
            else
                return this.MediumCellTopImageTemplate;
        }
    }
}
