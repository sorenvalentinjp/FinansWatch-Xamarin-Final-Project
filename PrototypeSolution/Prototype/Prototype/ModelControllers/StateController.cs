using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.ModelControllers
{
    class StateController
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

    }

}
