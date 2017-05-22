using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.ModelControllers;
using Prototype.Models;

namespace Prototype.UITest.Mocks
{
    public static class ArticleControllerMockGenerator
    {
        public static ArticleController GenerateMock()
        {
            var validSectionUrl = "https://content.watchmedier.dk/api/finanswatch/content/latest?hoursago=500&max=30&section=fw_finansnyt_penge";

            //Create ArticleController with mock ContentApi
            IList<Section> sections = new List<Section>();
            sections.Add(new Section("FORSIDE", validSectionUrl));
            sections.Add(new Section("PENGEINSTITUTTER", validSectionUrl));
            sections.Add(new Section("FORSIKRINGER", validSectionUrl));
            sections.Add(new Section("PENSION", validSectionUrl));
            sections.Add(new Section("REALKREDIT", validSectionUrl));
            sections.Add(new Section("NAVNE OG JOB", validSectionUrl));
            sections.Add(new Section("KLUMMER", validSectionUrl));

            return new ArticleController(ContentApiMockGenerator.GenerateMock().Object, sections);
        }
    }
}
