using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int c = 0;
        public MainPage()
        {
            InitializeComponent();
        }
        
        private void Button_1_clicked(object sender, EventArgs e)
        {
            c++;
            Button_1.Text = Convert.ToString(c);
        }

        private void Button_2_clicked(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Button_2.BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
}
