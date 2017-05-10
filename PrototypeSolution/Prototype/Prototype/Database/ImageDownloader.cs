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

        public async Task<StreamImageSource> DownloadImage(string url)
        {
            return await Task.Run(async () =>
            {
                //Download the image async
                var uri = new Uri(url);
                var imageAsBytes = await _client.GetByteArrayAsync(uri); // get the downloaded data

                StreamImageSource streamImageSource = (StreamImageSource) ImageSource.FromStream(() => new MemoryStream(imageAsBytes)); // Create imagesource with downloaded data

                return streamImageSource;

                //return new StreamImageSource{Stream = () => { new MemoryStream(imageAsBytes)}; // Create imagesource with downloaded data
                
                
            });

        }
    }
}
