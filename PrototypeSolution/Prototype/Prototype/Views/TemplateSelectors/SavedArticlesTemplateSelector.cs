using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.Cells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views.TemplateSelectors
{
    public class SavedArticlesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MediumCellFrontPageImageTemplate { get; set; }
        public DataTemplate MediumCellTopImageTemplate { get; set; }
        public DataTemplate MediumCellNoImageTemplate { get; set; }

        public SavedArticlesTemplateSelector(StateController stateController)
        {
            this.MediumCellFrontPageImageTemplate = new DataTemplate(() => new MediumCellFrontPageImage(stateController));
            this.MediumCellTopImageTemplate = new DataTemplate(() => new MediumCellTopImage(stateController));
            this.MediumCellNoImageTemplate = new DataTemplate(() => new MediumCellNoImage(stateController));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.image == null && article.topImage == null)
                return this.MediumCellNoImageTemplate;
            else if (article.image != null)
                return this.MediumCellFrontPageImageTemplate;
            else
                return this.MediumCellTopImageTemplate;
        }
    }
}
