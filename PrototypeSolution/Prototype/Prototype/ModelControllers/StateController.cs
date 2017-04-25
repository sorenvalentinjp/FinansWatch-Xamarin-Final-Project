using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.ModelControllers
{
    public class StateController
    {
        private ArticleController articleController;

        public StateController()
        {
            articleController = new ArticleController();
        }

        public Task<List<Article>> getFrontPageArticles()
        {
            return articleController.getFrontPageArticles();
        }

        public Task<Article> getArticleDetails(Article article)
        {
            return articleController.getArticleDetails(article);
        }
    }

}
