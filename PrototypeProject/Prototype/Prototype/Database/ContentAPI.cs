﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace Prototype.Database
{
    class ContentAPI : IContentAPI
    {
        private HttpClient client;

        public ContentAPI()
        {
            //Initialize httpclient using variables stored in the Constants class
            var authData = string.Format("{0}:{1}", Constants.contentAPIUsername, Constants.contentAPIkey);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        /// <summary>
        /// Downloads all front page articles and returns them as a string
        /// </summary>
        /// <returns></returns>
        public async Task<string> downloadFrontPageArticles()
        {
            var toReturn = "";

            //Full url: https://content.watchmedier.dk/api/finanswatch/content/frontpagearticles
            var uri = new Uri(Constants.contentAPIUrl + "finanswatch/content/frontpagearticles");

            try
            {
                var response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    toReturn = await response.Content.ReadAsStringAsync();
                }
            } catch(Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return toReturn;
        }
    }
}
