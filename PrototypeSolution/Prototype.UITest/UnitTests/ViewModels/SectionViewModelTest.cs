using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prototype.ModelControllers;
using Prototype.Models;
using Prototype.UITest.Mocks;
using Prototype.UITest.UnitTests.Helpers;
using Prototype.ViewModels;
using Xamarin.Forms;

namespace Prototype.UITest.UnitTests.ViewModels
{
    /// <summary>
    /// SectionViewModel.cs only contains methods that pass calls to StateController.cs and ArticleViewModel.cs
    /// Therefore only tests of propertychanged events is done currently. 
    /// </summary>
    public class SectionViewModelTest
    {
        SectionViewModel _sectionViewModel;

        [SetUp]
        public void Setup()
        {
            _sectionViewModel = new SectionViewModel(StateControllerMockGenerator.GenerateMock(), new Section("Frontpage", "sectionUrl"));
        }

        [Test]
        public void SectionShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_sectionViewModel, (x) => x.Section = new Section("Test", "test"), "Section");
        }

        [Test]
        public void ArticleViewModelsRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_sectionViewModel, (x) => x.ArticleViewModels = new List<ArticleViewModel>(), "ArticleViewModels");
        }

        [Test]
        public void IsRefreshingShouldRaisePropertyChangedEvent()
        {
            _sectionViewModel.IsRefreshing = false;
            PropertyChangedAsserter.AssertPropertyChanged(_sectionViewModel, (x) => x.IsRefreshing = true, "IsRefreshing");
        }

        [Test]
        public void DataTemplateShouldRaisePropertyChangedEvent()
        {
            PropertyChangedAsserter.AssertPropertyChanged(_sectionViewModel, (x) => x.DataTemplate = new DataTemplate(), "DataTemplate");
        }
    }
}
