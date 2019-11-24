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
        public MainPage(List<string> nds)
        {
            InitializeComponent();
            foreach (var s in nds)
            {
                if (s[0] == 1)
                {
                    addToLeftStackLayout(s.Substring(1));
                }
                else
                {
                    addToRightStackLayout(s.Substring(1));
                }
            }
        }

        public List<string> nodes = new List<string>();
 
        private void addToLeftStackLayout(string str)
        {
            Frame frame = new Frame
            {
                BorderColor = Color.Red,
                Margin = 10,
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#ECF8AF")
            };

            Label label = new Label
            {
                Text = str,
                FontSize = 20,
                TextColor = Color.Black
            };

            frame.Content = label;

            var tap = new TapGestureRecognizer();
            tap.Tapped += (a1, b1) =>
            {
                var editor = new Page1(label.Text);

                editor.Disappearing += (a2, b2) =>
                {
                    if (editor.text != null && editor.text != "")
                    {
                        nodes.Remove(Convert.ToChar(1) + label.Text);
                        label.Text = editor.text;
                        nodes.Add(Convert.ToChar(1) + label.Text);
                    }
                };

                Navigation.PushAsync(editor);
            };
            label.GestureRecognizers.Add(tap);

            var swipe = new SwipeGestureRecognizer();
            swipe.Direction = SwipeDirection.Left;
            swipe.Swiped += async (swipeSender, swipeEventArg) =>
            {
                if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                {
                    leftStLt.Children.Remove(swipeSender as Frame);
                    nodes.Remove(Convert.ToChar(1) + str);
                }
            };
            frame.GestureRecognizers.Add(swipe);

            leftStLt.Children.Add(frame);
        }

        private void addToRightStackLayout(string str)
        {
            Frame frame = new Frame
            {
                BorderColor = Color.Red,
                Margin = 10,
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#ECF8AF")
            };

            Label label = new Label
            {
                Text = str,
                FontSize = 20,
                TextColor = Color.Black
            };
            frame.Content = label;

            var tap = new TapGestureRecognizer();
            tap.Tapped += (a1, b1) =>
            {
                var editor = new Page1(label.Text);

                editor.Disappearing += (a2, b2) =>
                {
                    if (editor.text != null && editor.text != "")
                    {
                        nodes.Remove(Convert.ToChar(2) + label.Text);
                        label.Text = editor.text;
                        nodes.Add(Convert.ToChar(2) + label.Text);
                    }
                };

                Navigation.PushAsync(editor);
            };
            label.GestureRecognizers.Add(tap);
            
            var swipe = new SwipeGestureRecognizer();
            swipe.Direction = SwipeDirection.Right;
            swipe.Swiped += async (swipeSender, swipeEventArg) =>
            {
                if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                {
                    rightStLt.Children.Remove(swipeSender as Frame);
                    nodes.Remove(Convert.ToChar(2) + str);
                }
            };
            frame.GestureRecognizers.Add(swipe);
            
            rightStLt.Children.Add(frame);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var page1 = new Page1();

            page1.Disappearing += (a, b) =>
            {
                if (page1.text != null && page1.text != "")
                {
                    if (leftStLt.Height > rightStLt.Height)
                    {
                        addToRightStackLayout(page1.text);
                        nodes.Add(Convert.ToChar(2) + page1.text);
                    }
                    else
                    {
                        addToLeftStackLayout(page1.text);
                        nodes.Add(Convert.ToChar(1) + page1.text);
                    }
                }
            };

            Navigation.PushAsync(page1);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            nodes.Clear();
            leftStLt.Children.Clear();
            rightStLt.Children.Clear();
        }
    }
}
