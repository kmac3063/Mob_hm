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
            nodes = new List<string>();

            foreach (var s in nds)
            {
                string tempS = "";
                string dateS = "";
                bool d = false;
                for (int i = 0; i < s.Length - 2; i++) // ...|data|orient(0/1)
                {

                    if (!d && s[i] == 1)
                    {
                        d = true;
                        continue;
                    }

                    if (d)
                        dateS += s[i];
                    else
                        tempS += s[i];
                }

                addToStackLayout(tempS, Convert.ToDateTime(dateS), s.Last());
            }
        }

        public List<string> nodes;
        const char p = (char)1;

        private void addToStackLayout(string str, DateTime dt, int orient = -1)
        {
            bool inRightStack = rightStLt.Height < leftStLt.Height;
            if (orient == 0)
                inRightStack = false;
            if (orient == 1)
                inRightStack = true;

            Frame frame = new Frame
            {
                BorderColor = Color.Red,
                Margin = 10,
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#ECF8AF")
            };

            StackLayout stackLayout = new StackLayout();
            Label label = new Label
            {
                Text = formatted(str),
                FontSize = 20,
                TextColor = Color.Black
            };
            Label labelDate = new Label
            {
                Text = "" + dt.ToString("MM/dd H:mm"),
                TextColor = Color.Gray,
                VerticalOptions = LayoutOptions.End,
                HorizontalTextAlignment = TextAlignment.Start
            };
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(labelDate);
            frame.Content = stackLayout;

            var tap = new TapGestureRecognizer();
            tap.Tapped += (a1, b1) =>
            {
                var editor = new Page1(str);

                editor.Disappearing += (a2, b2) =>
                {
                    if (editor.text != null && editor.text != "")
                    {
                        nodes.Remove(str + p + dt + p + ToChar(inRightStack));
                        if (inRightStack)
                            rightStLt.Children.Remove(frame);
                        else
                            leftStLt.Children.Remove(frame);

                        str = editor.text;
                        label.Text = formatted(str);

                        dt = DateTime.Now;
                        labelDate.Text = "" + dt.ToString("MM/dd H:mm");

                        if (inRightStack)
                            rightStLt.Children.Add(frame);
                        else
                            leftStLt.Children.Add(frame);

                        nodes.Add(str + p + dt + p + ToChar(inRightStack));
                    }
                };

                Navigation.PushAsync(editor);
            };
            label.GestureRecognizers.Add(tap);

            var pan = new PanGestureRecognizer();
            var startX = frame.X;
            pan.PanUpdated += (panSender, e) => 
            {
                switch (e.StatusType)
                {
                    case GestureStatus.Running:
                        if (inRightStack)
                            frame.TranslationX = Math.Max(0, frame.TranslationX + e.TotalX);
                        else
                            frame.TranslationX = Math.Min(0, frame.TranslationX + e.TotalX);

                        if (Math.Abs(frame.TranslationX) >= frame.Width / 2)
                        {
                            if (inRightStack)
                                rightStLt.Children.Remove(panSender as Frame);
                            else
                                leftStLt.Children.Remove(panSender as Frame);


                            nodes.Remove(str + p + dt + p + ToChar(inRightStack));
                        }
                        break;
                    case GestureStatus.Completed:
                        frame.TranslationX = -(frame.X - startX);
                        break;
                }
            };
            frame.GestureRecognizers.Add(pan);

            if (inRightStack)
                rightStLt.Children.Add(frame);
            else
                leftStLt.Children.Add(frame);

            nodes.Add(str + p + dt + p + ToChar(inRightStack));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var editor = new Page1("");

            editor.Disappearing += (a, b) =>
            {
                if (editor.text != null && editor.text != "")
                {
                    var d = DateTime.Now;
                    addToStackLayout(editor.text, d);
                }
            };

            Navigation.PushAsync(editor);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            nodes.Clear();
            leftStLt.Children.Clear();
            rightStLt.Children.Clear();
        }

        string formatted(string s_)
        {
            const int max_symb = 45;
            const int max_str = 4;

            string s = "";

            int t_symb = 0;
            int t_str = 1;
            foreach (var c in s_)
            {
                if (t_symb <= max_symb && t_str <= max_str)
                {
                    s += c;
                }

                t_symb++;
                t_str += Convert.ToInt32(c == '\n');
                
            }

            return "(" + (t_symb - t_str) + ") " + s;
        }

        char ToChar(bool a)
        {
            if (a == true)
                return (char)1;
            return (char)0;
        }
    }
}
