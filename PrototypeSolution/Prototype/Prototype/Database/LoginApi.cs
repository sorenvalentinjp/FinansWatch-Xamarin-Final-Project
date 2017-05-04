using Java.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Database
{
    internal class LoginApi
    {
        private readonly HttpClient _client;

        public LoginApi()
        {
            _client = new HttpClient();

            string email = "michael.m.hansen@jp.dk";
            string password = "1234qwer";

            //get token
            string authData = $"{email}:{password}";
            string authDataBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            string deviceId = "1";
            string url = $"{Constants.GetTokenUrl}{deviceId}";

            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Add("JP-Authorization", authDataBase64);

            string res = DownloadJson(request).Result;

            Console.WriteLine(res);
            //get user access
        }

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
        }
    }
}
