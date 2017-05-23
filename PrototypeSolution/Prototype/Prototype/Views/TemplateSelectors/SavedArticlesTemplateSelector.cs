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
        public DataTemplate MediumCellNoImageTemplate { get; set; }

        public SavedArticlesTemplateSelector(StateController stateController)
        {
            this.MediumCellTopImageTemplate = new DataTemplate(() => new MediumCellTopImage(stateController));
            this.MediumCellNoImageTemplate = new DataTemplate(() => new MediumCellNoImage(stateController));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)item;

            if (articleViewModel.Article.topImage == null)
                return this.MediumCellNoImageTemplate;
            else
                return this.MediumCellTopImageTemplate;
        }
    }
}
