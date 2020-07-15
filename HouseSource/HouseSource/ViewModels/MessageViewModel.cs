using HouseSource.Models;
using HouseSource.ResponseData;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using HouseSource.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using HouseSource.Views;

namespace HouseSource.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private ObservableCollection<NewsInfo> newsList;   //消息列表
        public ObservableCollection<NewsInfo> NewsList
        {
            get { return newsList; }
            set { SetProperty(ref newsList, value); }
        }

        public Command<NewsInfo> NavigateCommand { get; set; }

        public MessageViewModel()
        {
            InitMessagePageAsync();

            NavigateCommand = new Command<NewsInfo>((n) =>
            {
                MessageDetailPage messageDetailPage = new MessageDetailPage(n);
                Application.Current.MainPage.Navigation.PushAsync(messageDetailPage);
            }, (n) => { return true; });

        }

        /// <summary>
        /// 初始化消息列表页面
        /// </summary>
        private async void InitMessagePageAsync()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                NewsRD newsRD = await RestSharpService.GetNewsAsync();

                NewsList = new ObservableCollection<NewsInfo>(newsRD.News);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
