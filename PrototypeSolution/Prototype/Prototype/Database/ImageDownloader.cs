using Prototype.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.Database
{
    public class ImageDownloader
    {
        private WebClient WebClient;
        public ImageDownloader()
        {
            WebClient = new WebClient();
        }

        public async Task<ImageSource> downloadImage(string URL)
        {
            return await Task.Run(() =>
            {
                //Download the image async
                var URI = new Uri(URL);
                var imageAsBytes = WebClient.DownloadData(URI); // get the downloaded data

                return ImageSource.FromStream(() => new MemoryStream(imageAsBytes)); // Create imagesource with downloaded data
            });
          
        }
        }
}
