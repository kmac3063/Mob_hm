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
        public string text = null;
        public Page1()
        {
            InitializeComponent();
        }

        public Page1(string S)
        {
            InitializeComponent();

            TextHolder.Text = text = S;
            Device.BeginInvokeOnMainThread( async () => { 
                await System.Threading.Tasks.Task.Delay(250);
                TextHolder.Focus(); 
            });
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            text = TextHolder.Text;
            Navigation.PopAsync();
        }
    }
}