using System;
using System.Threading.Tasks;

namespace Prototype.Database
{
    /// <summary>
    /// We use this interface to be able to mock the ContentApi class.
    /// </summary>
    public interface IContentApi
    {
        /// <summary>
        /// Downloads a single article and returns it as a task json string.
        /// </summary>
        /// <param name="contentUrl"></param>
        /// <returns></returns>
        Task<string> DownloadArticle(string contentUrl);

        /// <summary>
        /// Downloads allArticles page articles and returns them as a string.
        /// </summary>
        /// <returns></returns>
        Task<string> DownloadLatestArticles();

        /// <summary>
        /// Downloads articles belonging to a section and returns them as a string.
        /// </summary>
        /// <param name="sectionContentUrl"></param>
        /// <returns></returns>
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