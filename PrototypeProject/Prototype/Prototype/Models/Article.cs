using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Models
{
    class Article
    {
        public string title { get; set; }

        public Article(string title)
        {
            this.title = title;
        }
    }
}
