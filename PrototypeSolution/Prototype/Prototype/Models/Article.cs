using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Models
{
    class Article
    {
        public string Title { get; set; }
        public string ContentURL { get; set; }
        public string BodyText { get; set; }
        public string HomeSectionName { get; set; }
        public Boolean Locked { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string PublishInfo { get; set; }
        public int HomeSectionId { get; set; }
        public int Id { get; set; }
        public string ImageCaption { get; set; }
        public string Teaser { get; set; }
        public string ImageBigURL { get; set; }
        public string ImageSmallURL { get; set; }

        public Article()
        {

        }
    }
}
