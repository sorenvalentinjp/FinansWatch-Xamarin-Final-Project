using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views.Helpers
{
    public static class ListViewHelper
    {
        /// <summary>
        /// We dont want the user to be able to select the articles. Its on by default, so this method is nessecary to counter that.
        /// </summary>
        public static void DisableItemSelectedAction(ListView listView)
        {
            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }
    }
}
