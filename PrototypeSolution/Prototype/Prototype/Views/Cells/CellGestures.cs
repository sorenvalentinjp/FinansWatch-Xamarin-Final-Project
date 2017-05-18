using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views.Cells
{
    public static class CellGestures
    {
        public static async void TappedGesture(StateController stateController, ArticleViewModel articleViewModel)
        {
            var newPage = new ArticleDetailsView(stateController, articleViewModel);
            NavigationPage.SetBackButtonTitle(newPage, "");
            await App.Navigation.PushAsync(newPage, true);
        }

        public static async void LongPressingGesture(StateController stateController, ArticleViewModel articleViewModel)
        {
            if (stateController.ArticleController.AddOrRemoveSavedArticle(articleViewModel.Article))
            {
                await App.Navigation.NavigationStack.First().DisplayAlert("", "Artiklen er gemt i læselisten.", "OK");
            }
            else
            {
                await App.Navigation.NavigationStack.First().DisplayAlert("", "Artiklen er fjernet fra læselisten.", "OK");                
            }
        }
    }
}
