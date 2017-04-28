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
        private ArticleController articleController;
        public ObservableCollection<Article> SavedArticles { get; set; }

        public StateController()
        {
            this.articleController = new ArticleController();
            this.SavedArticles = new ObservableCollection<Article>();
        }

        public Task<IList<Article>> getFrontPageArticles()
        {
            return this.articleController.getFrontPageArticles();
        }

        public Task<Article> getArticleDetails(Article article)
        {
            return this.articleController.getArticleDetails(article);
        }

    }

}
