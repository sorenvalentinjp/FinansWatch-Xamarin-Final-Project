using System;
using System.Threading.Tasks;

namespace Prototype.Database
{
    public interface IContentApi
    {
        /// <summary>
        /// Downloads a single article and returns it as a task json string
        /// </summary>
        /// <param name="contentUrl"></param>
        /// <returns></returns>
        Task<string> DownloadArticle(string contentUrl);

        /// <summary>
        /// Downloads allArticles page articles and returns them as a string
        /// </summary>
        /// <returns></returns>
        Task<string> DownloadLatestArticles();

        Task<string> DownloadSection(string sectionContentUrl);

        /// <summary>
        /// Downloads and returns json using an Uri.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<string> DownloadJson(Uri uri);

        void DisposeClient();
    }
}