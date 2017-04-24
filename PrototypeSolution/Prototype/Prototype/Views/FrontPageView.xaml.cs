﻿using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FrontPageView : ContentPage
	{
        private Article TopArticle { get; set; }
        private ObservableCollection<Article> Articles { get; set; }
        private StateController StateController { get; set; }

        public FrontPageView ()
		{
			InitializeComponent ();

            StateController = new StateController();
            Articles = new ObservableCollection<Article>();

            getFrontPageArticles();


            //-----test - simple objects to test binding is working
            //List<Article> articles = new List<Article>();
            //Article a1 = new Article();
            //a1.Title = "Title 1";
            //Article a2 = new Article();
            //a2.Title = "Title 2";
            //articles.Add(a1);
            //articles.Add(a2);
            //this.BindingContext = articles;
        }

        public async void getFrontPageArticles()
        {
            List<Article> fetchedArticles = await StateController.getFrontPageArticles();
            this.TopArticle = fetchedArticles[0];

            stackTopArticle.BindingContext = this.TopArticle;

            fetchedArticles.RemoveAt(0);
            this.Articles = new ObservableCollection<Article>(fetchedArticles);
            lstView.BindingContext = this.Articles;
        }
    }
}