using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prototype.Views.Cells
{
    public static class CellGestures
    {
        public static async void TappedGesture(ContentPage page, StateController stateController, Article article)
        {
            var newPage = new ArticleView(stateController, article);
            NavigationPage.SetBackButtonTitle(newPage, "");
            await App.Navigation.PushAsync(new ArticleView(stateController, article), true);
        }

        public static async void LongPressingGesture(ContentPage page, StateController stateController, Article article)
        {
            if (stateController.SavedArticles.Contains(article))
            {
                stateController.SavedArticles.Remove(article);
                await page.DisplayAlert("", "Artiklen er fjernet fra læselisten.", "OK");
            }
            else
            {
                stateController.SavedArticles.Add(article);
                await page.DisplayAlert("", "Artiklen er gemt i læselisten.", "OK");
            }
        }
    }
}
