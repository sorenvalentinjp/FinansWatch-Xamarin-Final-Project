using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.UITest.UnitTests.Helpers
{
    public static class ReadJsonFile
    {
        public static string GetFileFromDisk(string path)
        {
            var absPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(absPath + path);
        }
    }
}
