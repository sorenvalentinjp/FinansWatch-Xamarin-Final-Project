using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Database
{
    interface IContentApi
    {
        Task<string> DownloadFrontPageArticles();
        Task<string> DownloadArticle(string contentUrl);
        Task<string> DownloadJson(Uri uri);
    }
}
