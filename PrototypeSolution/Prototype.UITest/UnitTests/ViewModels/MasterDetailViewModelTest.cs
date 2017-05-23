using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.Models;
using Prototype.UITest.Mocks;
using Prototype.UITest.UnitTests.Helpers;
using Prototype.ViewModels;
using Xamarin.Forms;

namespace Prototype.UITest.UnitTests.ViewModels
{
    /// <summary>
    /// MasterDetailViewModel.cs only contains methods that manipulate the view and methods that pass calls to statecontroller.cs.
    /// Therefore no testing is done currently. 
    /// We would have tested propertychanged events, but NUnit does not allow us to instantiate a MasterDetailPage without starting Xamarin.Forms.
    /// </summary>
    public class MasterDetailViewModelTest
    {
    }
}
