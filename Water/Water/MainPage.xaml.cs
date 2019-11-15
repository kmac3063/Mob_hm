using System;
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

        List<string> products = new List<string>();
        private void Add_Button_Clicked(object sender, EventArgs e)
        {
            var addPage = new AddPage();

            addPage.Disappearing += (a, b) =>
            {
                if (Navigation.NavigationStack.Last() == addPage)
                {
                    products.AddRange(addPage.products);
                    Bucket.Text = "Bucket(" + products.Count + ")";
                }
            };

            Navigation.PushAsync(addPage);
        }

        private void Bucket_Button_Clicked(object sender, EventArgs e)
        {
            var bucketPage = new BucketPage(products);
            bucketPage.Disappearing += (a, b) =>
            {
                products = bucketPage.products;
                Bucket.Text = "Bucket(" + products.Count + ")";
            };

            Navigation.PushAsync(bucketPage);
        }
        
        private void Buy_Button_Clicked(object sender, EventArgs e)
        {
            CompletePage complPage;
            if (products.Count > 0)
            {
                complPage = new CompletePage("Покупка совершена, отлично!");
                products.Clear();
            }
            else
            {
                complPage = new CompletePage("Так корзина ведь пуста!");
            }

            Navigation.PushAsync(complPage);
            Bucket.Text = "Bucket(0)";

        }

        
    }
}