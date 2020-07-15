using HouseSource.Controls;
using HouseSource.Views;
using System;
using Xamarin.Forms;

namespace HouseSource
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LoginPage loginPage = new LoginPage();
            MyNavigationPage myNavigationPage = new MyNavigationPage(loginPage)
            {
                BarBackgroundColor = Color.FromHex("fafafa"),
                BarTextColor = Color.Black
            };

            MainPage = myNavigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
