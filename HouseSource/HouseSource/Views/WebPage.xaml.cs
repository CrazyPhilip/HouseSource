using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HouseSource.ViewModels;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        public WebPage(string url, string title)
        {
            InitializeComponent();

            hybridWebView.Uri = url;
            //urlLabel.Text = hybridWebView.Uri;
            urlLabel.Text = "Url已加密...";
            this.Title = title;
        }

        /// <summary>
        /// 回退按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void backButton_Clicked(object sender, EventArgs e)
        {
            if (hybridWebView.CanGoBack)
            {
                hybridWebView.GoBack();
                urlLabel.Text = hybridWebView.Uri;
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        /// <summary>
        /// 前进按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forwardButton_Clicked(object sender, EventArgs e)
        {
            if (hybridWebView.CanGoForward)
            {
                hybridWebView.GoForward();
                urlLabel.Text = hybridWebView.Uri;
            }
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshButton_Clicked(object sender, EventArgs e)
        {
            hybridWebView.Reload();
            urlLabel.Text = hybridWebView.Uri;
        }

        /// <summary>
        /// 加载中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hybridWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            indicatorView.IsVisible = true;
        }

        /// <summary>
        /// 加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hybridWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            indicatorView.IsVisible = false;
        }

        /// <summary>
        /// 实体返回键
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            if (hybridWebView.CanGoBack)
            {
                hybridWebView.GoBack();
                urlLabel.Text = hybridWebView.Uri;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}