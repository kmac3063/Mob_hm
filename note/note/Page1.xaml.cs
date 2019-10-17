using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace note
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        public string text = null;

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            text = TextHolder.Text;
            Navigation.PopAsync();
        }
    }
}