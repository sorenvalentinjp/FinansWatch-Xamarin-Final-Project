using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.Mocks;
using Prototype.UITest.UnitTests.Helpers;
using Prototype.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.UITest.UnitTests.ViewModels
{
    [TestFixture]
    public class SavedArticlesViewModelTest
    {
        StateController _stateController;
        SavedArticlesViewModel _vm;

        [SetUp]
        public void Setup()
        {
            _stateController = new StateController();
            _vm = new SavedArticlesViewModel(_stateController);
        }

        [Test]
        public void SavedArticlesShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_vm, (x) => x.SavedArticles = new ObservableCollection<ArticleViewModel>(), "SavedArticles");
        }

        [Test]
        public void DataTemplateShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_vm, (x) => x.DataTemplate = new DataTemplate(), "DataTemplate");
        }

        //No need to test this as it is using a method in the ArticleViewModel, that we already tested
        //Also, we cant really do it, because we cant use Moq to simulate the invoke of the event.
        //[Test]
        //public void LoginEventShouldCalculateIfTheArticlesShouldBeLocked()
        //{
            
        //}

        //This test also confirms, that GetSavedArticles is working properly
        [Test]
        public void SavedArticles_CollectionChangedShouldBeUpdatedWhenTheCollectionIsChanged()
        {
            //Arrange
            Assert.True(_vm.SavedArticles.Count == 0);
            Article articleToAdd = new Article();

            //Act
            _stateController.ArticleController.SavedArticles.Add(articleToAdd);

            //Assert
            Assert.True(_vm.SavedArticles.Count == 1);
        }
    }
}
