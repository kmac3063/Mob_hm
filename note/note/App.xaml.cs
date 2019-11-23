using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace note
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var nodes = new List<string>();
            var filePath = "note/data/nodes.txt";

            try
            {
                var f = File.OpenText(filePath);
                string nodesText = f.ReadToEnd() + Convert.ToChar(1); //  Фиктивный элемент

                int j = 0;
                for (int i = 1; i < nodesText.Length; i++)
                {
                    if (nodesText[i] == 1 || nodesText[i] == 2)
                    {
                        nodes.Add(nodesText.Substring(j, i - j));
                        j = i;
                    }

                }

                f.Close();
            }
            catch
            {

            }
            

            var page = new MainPage(nodes);
            page.Disappearing += (a, b) =>
            {
                if (MainPage.Navigation.NavigationStack.Last() == page)
                {
                    
                    string text = "";
                    foreach(var s in page.nodes)
                    {
                        text += s;
                    }

                    File.CreateText(filePath).Write(text);
                }
            };

            MainPage = new NavigationPage(page);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
