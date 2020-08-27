using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace HouseSource.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            //var page = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(p => p is LoginPage);
            //Application.Current.MainPage.Navigation.RemovePage(page);
        }

        /// <summary>
        /// 返回键事件，禁止返回
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
