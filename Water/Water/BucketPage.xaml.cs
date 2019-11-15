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

        private void addToBucket(List<string> pr, ref StackLayout stackLayout) {
            while (Bucket_St_Lt.Children.Count > 1)
            {
                stackLayout.Children.RemoveAt(stackLayout.Children.Count - 1);
            }

            var names = new string[4] {"Water", "Juice", "Tea", "Чифир"};
            var count = new int[4];

            foreach (var name in pr)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (name == names[i])
                        count[i]++;
                }
            }

            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                if (count[i] == 0) continue;

                var stackLayout1 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center
                };


                var img = new Image
                {
                    WidthRequest = 60
                };
                switch (i)
                {
                    case (0):
                        img.Source = "Resources/drawable/bottle_of_water.jpg";
                        break;
                    case (1):
                        img.Source = "Resources/drawable/bottle_of_juice.jpg";
                        break;
                    case (2):
                        img.Source = "Resources/drawable/bottle_of_tea.jpg";
                        break;
                    case (3):
                        img.Source = "Resources/drawable/bottle_of_chifir.jpg";
                        break;
                }

                var label = new Label
                {
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center,
                    Text = names[i] + " x " + count[i],
                };


                stackLayout1.Children.Add(img);
                stackLayout1.Children.Add(label);

                var frame = new Frame
                {
                    BorderColor = Color.FromRgb(r.Next() % 256, r.Next() % 256, r.Next() % 256),
                    CornerRadius = 20,
                    WidthRequest = 100,
                    Padding = 0,
                    Content = stackLayout1
                };

                var tap = new TapGestureRecognizer();
                tap.Tapped += (a, b) =>
                {
                    var name = label.Text.Split(' ')[0];
                    var c = Int32.Parse(label.Text.Split(' ')[2]);
                    if (c > 0)
                    {
                        c--;
                        label.Text = name + " x " + c;
                        products.Remove(name);
                    }
                };
                frame.GestureRecognizers.Add(tap);

                stackLayout.Children.Add(frame);
            }
        }
        public BucketPage(List<string> products_)
        {
            InitializeComponent();

            products = products_;
            addToBucket(products, ref Bucket_St_Lt);
        }

        private void Add_Button_Clicked(object sender, EventArgs e)
        {
            var addPage = new AddPage();

            addPage.Disappearing += (a, b) =>
            {
                if (Navigation.NavigationStack.Last() == addPage)
                {
                    products.AddRange(addPage.products); 
                    addToBucket(products, ref Bucket_St_Lt);
                }
            };

            Navigation.PushAsync(addPage);
        }

        private void Buy_Button_Clicked(object sender, EventArgs e)
        {
            if (Bucket_St_Lt.Children.Count == 1)
            {
                var pg = new CompletePage("Корзина ведь пуста!");
                Navigation.PushAsync(pg);
                return;
            }

            products.Clear();
            Navigation.PopAsync();

            var compl_page = new CompletePage("Покупка совершена! Спасибо!");
            Navigation.PushAsync(compl_page);
        }
        
    }
}
