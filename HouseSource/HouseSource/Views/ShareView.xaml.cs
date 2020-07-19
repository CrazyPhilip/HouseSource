using HouseSource.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShareView : PopupPage
    {
        public ShareView(string propertyID)
        {
            InitializeComponent();

            BindingContext = new ShareViewModel(propertyID);
        }


        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        /// 返回键事件
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAsync();
            return false;
        }
    }
}