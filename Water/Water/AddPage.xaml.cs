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
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void Back_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public List<string> products = new List<string>();
        Image image = new Image();
        private void picker_product_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Item_St_Lt.Children.Contains(image))
                Item_St_Lt.Children.Remove(image);

            var id_img = picker_product.SelectedIndex;
            switch (id_img)
            {
                case(0)://вода сок чай чифир
                    image.Source = "Resources/drawable/bottle_of_water.jpg";
                    break;
                case (1):
                    image.Source = "Resources/drawable/bottle_of_juice.jpg";
                    break;
                case (2):
                    image.Source = "Resources/drawable/bottle_of_tea.jpg";
                    break;
                case (3):
                    image.Source = "Resources/drawable/bottle_of_chifir.jpg";
                    break;
            }

            Item_St_Lt.Children.Insert(0, image);
        }

        private void AddToBucket_Button_CLicked(object sender, EventArgs e)
        {
            var prod_name = picker_product.Items[picker_product.SelectedIndex];
            products.Add(prod_name);

            var compl_page = new CompletePage(prod_name + " успешно добавлен в корзину. Так держать!");
            Navigation.PushAsync(compl_page);
        }
    }
}