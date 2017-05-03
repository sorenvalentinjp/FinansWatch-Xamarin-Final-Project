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
    public class MediumArticleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ArticleMediumTemplate { get; set; }
        public DataTemplate ArticleMediumNoImageTemplate { get; set; }

        public MediumArticleTemplateSelector(StateController stateController, ContentPage page)
        {
            this.ArticleMediumTemplate = new DataTemplate(() =>
            {
                return new MediumCellRelatedArticle(stateController, page);
            });

            this.ArticleMediumNoImageTemplate = new DataTemplate(() =>
            {
                return new MediumCellRelatedArticleNoImage(stateController, page);
            });            
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Article article = (Article)item;

            if (article.topImage == null)
                return ArticleMediumNoImageTemplate;
            else
                return ArticleMediumTemplate;
        }
    }
}
