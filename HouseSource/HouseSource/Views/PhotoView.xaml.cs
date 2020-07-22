using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoView : PopupPage
    {
        public PhotoView(string source)
        {
            InitializeComponent();

            cacheImage.Source = source;
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