using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterButton : Button
	{
		public MasterButton ()
		{
			InitializeComponent ();
		}
	}
}
