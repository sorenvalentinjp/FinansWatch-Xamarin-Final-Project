using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Prototype.Database
{
    class ContentAPI : IContentAPI
    {

        public ContentAPI()
        {

        }

        /// <summary>
        /// Downloads a single article and returns it as a task json string
        /// </summary>
        /// <param name="contentURL"></param>
        /// <returns></returns>
        public Task<string> downloadArticle(String contentURL)
        {
            //Full url example: https://content.watchmedier.dk/api/finanswatch/content/article/9517468
            var uri = new Uri(contentURL);

            return downloadJSON(uri);
        }

        /// <summary>
        /// Downloads all front page articles and returns them as a string
        /// </summary>
        /// <returns></returns>
        public Task<string> downloadFrontPageArticles()
        {
            //Full url example: https://content.watchmedier.dk/api/finanswatch/content/frontpagearticles
            var uri = new Uri(Constants.contentAPIUrl + "finanswatch/content/frontpagearticles");

            return downloadJSON(uri);
        }

        /// <summary>
        /// Downloads and returns json using an Uri.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> downloadJSON(Uri uri)
        {
            var toReturn = "";

            //Initialize httpclient using variables stored in the Constants class
            var authData = string.Format("{0}:{1}", Constants.contentAPIUsername, Constants.contentAPIkey);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            using (HttpClient client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        toReturn = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    client.Dispose();
                }
                client.Dispose();
            }

            return toReturn;
        }
    }
}
