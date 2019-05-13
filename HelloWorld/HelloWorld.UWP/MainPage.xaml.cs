using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace HelloWorld.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new HelloWorld.App());
        }
    }
}
