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
            
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(docsPath, "nodes.txt");

            StreamReader f;
            string nodesText;
            try
            {
                f = File.OpenText(filePath);
                nodesText = f.ReadToEnd() + Convert.ToChar(1); //  Фиктивный элемент
                f.Close();
            }
            catch{
                nodesText = "";
            }

            int j = 0;
            for (int i = 1; i < nodesText.Length; i++)
            {
                if (nodesText[i] == 1 || nodesText[i] == 2)
                {
                    nodes.Add(nodesText.Substring(j, i - j));
                    j = i;
                }

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

                    writeToFile(filePath, text);
                }
            };

            MainPage = new NavigationPage(page);
        }

        public async Task writeToFile(string filePath, string text)
        {
            using (var writer = File.CreateText(filePath))
            {
                await writer.WriteAsync(text);
            }
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
