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
        public void GetFrontPageArticles()
        {
            ArticleController.GetFrontPageArticlesAsync();
        }

        public Task<IList<Article>> GetRelatedArticles(Article article)
        {
            return ArticleController.GetRelatedArticlesAsync(article);
        }

        public Task<Article> GetArticleDetails(Article article)
        {
            return this.ArticleController.GetArticleDetailsAsync(article);
        }


    }

}
