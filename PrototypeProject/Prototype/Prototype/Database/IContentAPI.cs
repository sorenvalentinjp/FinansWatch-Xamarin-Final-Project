using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Database
{
    public interface IContentAPI
    {
        Task<String> downloadFrontPageArticles();
    }
}
