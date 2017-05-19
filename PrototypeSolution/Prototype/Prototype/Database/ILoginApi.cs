using System.Net.Http;
using System.Threading.Tasks;
using Prototype.Models;

namespace Prototype.Database
{
    internal interface ILoginApi
    {
        /// <summary>
        /// Downloads the token using an email and password
        /// </summary>
        /// <param name="email">the subscribers email</param>
        /// <param name="password">the subscribers password</param>
        /// <returns>The token represented as a json string</returns>
        Task<string> DownloadLoginToken(string email, string password);

        /// <summary>
        /// Using the aquired token, the subscriber itself can be downloaded.
        /// </summary>
        /// <param name="token">The subscribers token</param>
        /// <returns>The subscriber respresented as a json string</returns>
        Task<string> DownloadSubscriber(SubscriberToken token);

        /// <summary>
        /// Downloads the httprequestmessage and returns the result as a string
        /// </summary>
        /// <param name="request">The HttpRequestMessage</param>
        /// <returns>The result as json string</returns>
        Task<string> DownloadJson(HttpRequestMessage request);
    }
}