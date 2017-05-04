using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.Views.TemplateSelectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.ViewModels;
using Prototype.Views.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticleView : ContentPage
    {

        public ArticleView(StateController stateController, Article articleToDisplay)
        {
            InitializeComponent();

            ListViewHelper.DisableItemSelectedAction(listView);

            BindingContext = new ArticleViewModel(stateController, articleToDisplay);
        }
    }
}
