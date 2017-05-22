using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.ModelControllers;

namespace Prototype.UITest.Mocks
{
    public static class StateControllerMockGenerator
    {
        public static StateController GenerateMock()
        {
            return new StateController
            {
                ArticleController = ArticleControllerMockGenerator.GenerateMock(),
                LoginController = new LoginController(LoginApiMockGenerator.GenerateMock().Object)
            };
        }
    }
}
