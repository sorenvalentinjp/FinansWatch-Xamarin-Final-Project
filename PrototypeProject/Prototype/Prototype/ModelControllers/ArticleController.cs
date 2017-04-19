using Prototype.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.ModelControllers
{
    class ArticleController
    {
        private ContentAPI contentAPI;

        public ArticleController()
        {
            contentAPI = new ContentAPI();
        }

        public Task<string> getFrontPageArticles()
        {

            //Debug.WriteLine("BEFORE-----------------------------------------------");

            //Debug.WriteLine(contentAPI.downloadFrontPageArticles());

            //Debug.WriteLine("AFTER-----------------------------------------------");



            return contentAPI.downloadFrontPageArticles();
        }
    }
}
