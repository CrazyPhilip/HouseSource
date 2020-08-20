using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;

namespace HouseSource.Controls
{
    public class MyNavigationPage : Xamarin.Forms.NavigationPage
    {
        public MyNavigationPage(Page page)
        {
            On<Android>().SetBarHeight(100);
            BarBackgroundColor = Color.SteelBlue;
            BarTextColor = Color.White;
            PushAsync(page);
        }
    }
}
