using Prototype.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.Database
{
    public class ImageDownloader
    {
        private readonly HttpClient _client;
        public ImageDownloader()
        {
            _client = new HttpClient();
        }
        
        public async Task<byte[]> DownloadImage(string url)
        {
            //Download the image async
            var uri = new Uri(url);
            return await _client.GetByteArrayAsync(uri); // get the downloaded data            
        }
    }
}