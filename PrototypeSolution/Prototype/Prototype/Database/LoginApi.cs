using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Database
{
    internal class LoginApi : ILoginApi
    {
        private readonly HttpClient _client;

        public LoginApi()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        /// <summary>
        /// Downloads the token using an email and password
        /// </summary>
        /// <param name="email">the subscribers email</param>
        /// <param name="password">the subscribers password</param>
        /// <returns>The token represented as a json string</returns>
        public Task<string> DownloadLoginToken(string email, string password)
        {
            string authData = $"{email}:{password}";
            string authDataBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            //TODO: device id is hardcoded for now, but we should fetch the actual device's id later on!
            string deviceId = "1";
            string url = $"{Constants.GetTokenUrl}{deviceId}";

            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Add("JP-Authorization", authDataBase64);
            return DownloadJson(request);
        }

        /// <summary>
        /// Using the aquired token, the subscriber itself can be downloaded.
        /// </summary>
        /// <param name="token">The subscribers token</param>
        /// <returns>The subscriber respresented as a json string</returns>
        public Task<string> DownloadSubscriber(SubscriberToken token)
        {
            string url = $"{Constants.GetUserUrl}{token.token}";
            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            return DownloadJson(request);
        }

        /// <summary>
        /// Downloads the httprequestmessage and returns the result as a string
        /// </summary>
        /// <param name="request">The HttpRequestMessage</param>
        /// <returns>The result as json string</returns>
        public async Task<string> DownloadJson(HttpRequestMessage request)
        {
            try
            {
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                return "";
            }
            finally
            {
                request.Dispose(); //important to dispose the HttpRequestMessage for performance gain
            }
        }
    }
}
