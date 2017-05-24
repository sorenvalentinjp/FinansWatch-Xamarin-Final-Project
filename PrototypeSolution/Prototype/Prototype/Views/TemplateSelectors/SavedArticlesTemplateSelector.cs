using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.ViewModels;
using Prototype.Views.Cells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views.TemplateSelectors
{
    public class SavedArticlesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MediumCellTopImageTemplate { get; set; }

        public SavedArticlesTemplateSelector(StateController stateController)
        {
            this.MediumCellTopImageTemplate = new DataTemplate(() => new MediumCellTopImage(stateController));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)item;

                return this.MediumCellTopImageTemplate;
        }
    }
}
