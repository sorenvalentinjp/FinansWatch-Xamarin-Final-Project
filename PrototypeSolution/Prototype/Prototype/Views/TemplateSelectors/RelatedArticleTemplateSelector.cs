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
    public class RelatedArticleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RelatedArticleCell { get; set; }
        public DataTemplate RelatedArticleCellNoImage { get; set; }

        public RelatedArticleTemplateSelector(StateController stateController, ContentPage page)
        {
            this.RelatedArticleCell = new DataTemplate(() =>
            {
                return new RelatedArticleCell(stateController, page);
            });

            this.RelatedArticleCellNoImage = new DataTemplate(() =>
            {
                return new RelatedArticleCellNoImage(stateController, page);
            });            
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.topImage == null)
                return RelatedArticleCellNoImage;
            else
                return RelatedArticleCell;
        }
    }
}
