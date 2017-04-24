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
        //private WebClient WebClient;
        public ImageDownloader()
        {
           
        }

        public async Task<ImageSource> downloadImage(string URL)
        {
            return await Task.Run(() =>
            {
                if (URL == null || URL == "") return null;
                WebClient webClient = new WebClient();
                //Download the image async
                var URI = new Uri(URL);
                var imageAsBytes = webClient.DownloadData(URI); // get the downloaded data
                webClient.Dispose();
                return ImageSource.FromStream(() => new MemoryStream(imageAsBytes)); // Create imagesource with downloaded data
            });
        }
        }
}
