using Xamarin.Forms;

namespace Learn_project
{
    class StartPage : ContentPage
    {
        public StartPage()
        {
            Label header = new Label() { Text = "Прива", HorizontalOptions = LayoutOptions.Center};
            this.Content = header;
        }
    }
}
