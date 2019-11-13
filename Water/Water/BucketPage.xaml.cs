using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Water
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class BucketPage : ContentPage
    {
        public List<string> products = new List<string>();
        public BucketPage(List<string> products_)
        {
            InitializeComponent();

            products = products_;
            foreach (var name in products){
                var frame = new Frame
                {
                    BorderColor = Color.IndianRed,
                    CornerRadius = 20,
                    HorizontalOptions = LayoutOptions.Center,// TO DO
                    Content = new Label
                    {
                        Text = name,
                        Margin = 10
                    }
                };

                Bucket_St_Lt.Children.Add(frame);
            }
        }

        private void Add_Button_Clicked(object sender, EventArgs e)
        {
            var addPage = new AddPage();

            addPage.Disappearing += (a, b) =>
            {
                while (Bucket_St_Lt.Children.Count > 1)
                {
                    Bucket_St_Lt.Children.RemoveAt(Bucket_St_Lt.Children.Count - 1);
                }
                
                products.AddRange(addPage.products);

                foreach (var name in products)
                {
                    var frame = new Frame
                    {
                        BorderColor = Color.IndianRed,
                        CornerRadius = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        Content = new Label
                        {
                            Text = name,
                            Margin = 10
                        }
                    };
                    
                    Bucket_St_Lt.Children.Add(frame);
                }
            };

            Navigation.PushAsync(addPage);
        }

        private void Buy_Button_Clicked(object sender, EventArgs e)
        {
            while (Bucket_St_Lt.Children.Count > 1)
            {
                Bucket_St_Lt.Children.RemoveAt(Bucket_St_Lt.Children.Count - 1);
            }

            var compl_page = new CompletePage("Покупка совершена! Спасибо!");
            Navigation.PushAsync(compl_page);

            products.Clear();
        }
        
    }
}
