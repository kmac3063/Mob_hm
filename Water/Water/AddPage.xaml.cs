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

        private void picker_product_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id_img = picker_product.SelectedIndex;
            switch (id_img)
            {
                case(0):
                    Product_Image.Source = "Resources/drawable/bottle_of_water.jpg";
                    break;
                case (1):
                    Product_Image.Source = "Resources/drawable/bottle_of_juice.jpg";
                    break;
                case (2):
                    Product_Image.Source = "Resources/drawable/bottle_of_tea.jpg";
                    break;
                case (3):
                    Product_Image.Source = "Resources/drawable/bottle_of_chifir.jpg";
                    break;
            }
            
            My_Stepper.Value = 1;
        }

        private void AddToBucket_Button_CLicked(object sender, EventArgs e)
        {
            var prod_name = picker_product.Items[picker_product.SelectedIndex];
            for (int i = 0; i < My_Stepper.Value; i++)
                products.Add(prod_name);

            var compl_page = new CompletePage(prod_name + " " + My_Stepper.Value + "шт успешно добавлен(ы) в корзину. Так держать!");
            Navigation.PushAsync(compl_page);

            My_Stepper.Value = 1;
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Stepper_Label.Text = "" + e.NewValue;
        }
    }
}