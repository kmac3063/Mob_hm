﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Water
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void Home_Button_Clicked(object sender, EventArgs e)
        {
           /* var homePage = new homePage();
            Navigation.PushAsync(addPage);*/
        }

        List<string> products = new List<string>();
        private void Add_Button_Clicked(object sender, EventArgs e)
        {
            var addPage = new AddPage();

            addPage.Disappearing += (a, b) =>
            {
                products = addPage.products;
                Bucket.Text = "Bucket(" + products.Count + ")";
            };

            Navigation.PushAsync(addPage);
        }

        private void Bucket_Button_Clicked(object sender, EventArgs e)
        {
            var bucketPage = new BucketPage(products);
            Navigation.PushAsync(bucketPage);
        }
        
        private void Buy_Button_Clicked(object sender, EventArgs e)
        {
            var buyPage = new BuyPage();
            Navigation.PushAsync(buyPage);
        }

        
    }
}