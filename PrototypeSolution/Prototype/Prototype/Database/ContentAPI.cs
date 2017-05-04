using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Prototype.Database
{
    internal class ContentApi : IContentApi
    {
        private readonly HttpClient client;

        public ContentApi()
        {
            //Initialize httpclients using variables stored in the Constants class
            client = new HttpClient();
            var authData = $"{Constants.contentAPIUsername}:{Constants.contentAPIkey}";
            //var authData = string.Format("{0}:{1}", Constants.contentAPIUsername, Constants.contentAPIkey);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        /// <summary>
        /// Downloads a single article and returns it as a task json string
        /// </summary>
        /// <param name="contentUrl"></param>
        /// <returns></returns>
        public Task<string> DownloadArticle(string contentUrl)
        {
            //Full url example: https://content.watchmedier.dk/api/finanswatch/content/article/9517468
            var uri = new Uri(contentUrl);

            return DownloadJson(uri);
        }

        /// <summary>
        /// Downloads all front page articles and returns them as a string
        /// </summary>
        /// <returns></returns>
        public Task<string> DownloadFrontPageArticles()
        {
            //Full url example: https://content.watchmedier.dk/api/finanswatch/content/frontpagearticles
            var uri = new Uri(Constants.contentAPIUrl + "finanswatch/content/frontpagearticles?max=30");

            return DownloadJson(uri);
        }

        /// <summary>
        /// Downloads allArticles page articles and returns them as a string
        /// </summary>
        /// <returns></returns>
        public Task<string> DownloadLatestArticles()
        {
            //Full url example: "https://content.watchmedier.dk/api/finanswatch/content/latest?hoursago=168"
            var uri = new Uri(Constants.contentAPIUrl + "finanswatch/content/latest?hoursago=168&max=100");

            return DownloadJson(uri);
        }

        /// <summary>
        /// Downloads and returns json using an Uri.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> DownloadJson(Uri uri)
        {
            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                return "";
            }
        }

        //Not currently used. Not deleted as we might need it later on to check url's
        public async Task<bool> IsValidUrl(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                response.Dispose(); //not sure if this is needed. Does it close the client or just the response message?
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {url} is not valid.");
                Debug.WriteLine($"ERRORMESSAGE: {ex.Message}");
                return false;
            }
            finally
            {
                response.Dispose();
            }
        }
    }
}
