using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.ModelControllers
{
    public class StateController
    {
        public ArticleController ArticleController;
        public ObservableCollection<Article> SavedArticles { get; set; }

        public StateController()
        {
            this.ArticleController = new ArticleController();
            this.SavedArticles = new ObservableCollection<Article>();
        }

        /// <summary>
        /// Fetches all frontpage articles async and wait for the articleIsReady events to fire
        /// During this operation a 'refresh' icon is displayed.
        /// </summary>
        public void getFrontPageArticles()
        {
            ArticleController.getFrontPageArticlesAsync();
        }

        public Task<IList<Article>> getRelatedArticles(Article article)
        {
            return ArticleController.getRelatedArticlesAsync(article);
        }

        public Task<Article> getArticleDetails(Article article)
        {
            return this.ArticleController.getArticleDetailsAsync(article);
        }


    }

}
