using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views.Helpers
{
    /// <summary>
    /// This class is used for binding to a ToolBarItem's command property. The command fires an event, that is then handled in MasterDetailViewModel.
    /// This is so we can set the Detail page to AllArticlesView when the ToolBarItem is pressed.
    /// </summary>
    public class ToolBarExtension : IMarkupExtension
    {
        public static event Action AllArticlesShortcutActionOccured;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return ToolbarItemAllArticlesCommand;
        }

        public ICommand ToolbarItemAllArticlesCommand
        {
            get
            {
                return new Command(() =>
                {
                    AllArticlesShortcutActionOccured?.Invoke();
                });
            }
        }
    }
}
