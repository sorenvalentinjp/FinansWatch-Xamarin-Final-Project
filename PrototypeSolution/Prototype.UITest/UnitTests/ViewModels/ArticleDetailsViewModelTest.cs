using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.Mocks;
using Prototype.UITest.UnitTests.Helpers;
using Prototype.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prototype.UITest.UnitTests.ViewModels
{
    [TestFixture]
    public class ArticleDetailsViewModelTest
    {
        StateController _stateController;
        ArticleDetailsViewModel _vm;
        
        [SetUp]
        public void Setup()
        {
            Article article = new Article();
            _stateController = StateControllerMockGenerator.GenerateMock();
            ArticleViewModel articleViewModel = new ArticleViewModel(_stateController, article);

            _vm = new ArticleDetailsViewModel(_stateController, articleViewModel);
        }

        [Test]
        public void ArticleViewModelShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_vm, (x) => x.ArticleViewModel = new ArticleViewModel(_stateController, new Article()), "ArticleViewModel");
        }

        [Test]
        public void DataTemplateShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_vm, (x) => x.DataTemplate = new DataTemplate(), "DataTemplate");
        }

        [Test]
        public void RelatedArticleViewModelsShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_vm, (x) => x.RelatedArticleViewModels = new List<ArticleViewModel>(), "RelatedArticleViewModels");
        }
    }
}
