using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace AppsolutelyFabulous.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new AppsolutelyFabulous.App());
        }
    }
}
