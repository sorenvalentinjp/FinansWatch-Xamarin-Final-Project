using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prototype.Database;
using Prototype.Models;
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

        public async Task<List<Article>> getFrontPageArticles()
        {
            List<Article> articles = new List<Article>();
            //JObject json = JObject.Parse(await contentAPI.downloadFrontPageArticles());
            dynamic json = JsonConvert.DeserializeObject(await contentAPI.downloadFrontPageArticles());

            foreach(var article in json)
            {
                string title = article.titles.FRONTPAGE;
                articles.Add(new Article(title));
            }

            return articles;
        }
    }
}
