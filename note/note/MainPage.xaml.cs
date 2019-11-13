using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace note
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var page1 = new Page1();
            
            page1.Disappearing += (a, b) =>
            {
                if (page1.text != null)
                {
                    Label label = new Label();
                    label.Text = page1.text.Split('\n')[0];
                    label.BackgroundColor = Color.Gray;

                    var tap = new TapGestureRecognizer();
                    tap.Tapped += (a1, b1) =>
                    {
                        var editor = new Page1(page1.text);

                        editor.Disappearing += (a2, b2) =>
                        {
                            page1.text = editor.text;
                            label.Text = editor.text.Split('\n')[0];
                        };

                        Navigation.PushAsync(editor);
                    };

                    label.GestureRecognizers.Add(tap);
                    StackLayout1.Children.Add(label);
                }
            };
            
            Navigation.PushAsync(page1);
        }

    }
}
