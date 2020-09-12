using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HouseSource.Themes;
using HouseSource.Utils;
using HouseSource.Services;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System.IO;
using RestSharp;
using RestSharp.Extensions;
using Xamarin.Essentials;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using HouseSource.ResponseData;
using Newtonsoft.Json;

namespace HouseSource.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private bool darkModeIsToggled;   //Comment
        public bool DarkModeIsToggled
        {
            get { return darkModeIsToggled; }
            set { SetProperty(ref darkModeIsToggled, value); }
        }

        private string rate;   //Comment
        public string Rate
        {
            get { return rate; }
            set { SetProperty(ref rate, value); }
        }

        private string currentVersion;   //Comment
        public string CurrentVersion
        {
            get { return currentVersion; }
            set { SetProperty(ref currentVersion, value); }
        }

        private string newestVersion;   //Comment
        public string NewestVersion
        {
            get { return newestVersion; }
            set { SetProperty(ref newestVersion, value); }
        }

        public Command ThemeCommand { get; set; }
        public Command ClearCacheCommand { get; set; }
        public Command UpdateCommand { get; set; }

        public SettingViewModel()
        {
            DarkModeIsToggled = GlobalVariables.DarkMode;
            CurrentVersion = AppInfo.VersionString;

            ThemeCommand = new Command(() =>
            {
                GlobalVariables.DarkMode = DarkModeIsToggled;
                Theme theme = GlobalVariables.DarkMode ? Theme.Dark : Theme.Light;

                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    mergedDictionaries.Clear();

                    switch (theme)
                    {
                        case Theme.Dark:
                            mergedDictionaries.Add(new DarkTheme());
                            break;
                        case Theme.Light:
                        default:
                            mergedDictionaries.Add(new LightTheme());
                            break;
                    }
                }
            }, () => { return true; });

            ClearCacheCommand = new Command(() =>
            {
                //LocalDatabaseService localDatabaseService = new LocalDatabaseService();
                //int total = await localDatabaseService.ClearAllData();
                //if (total > 0)
                //{
                //    CrossToastPopUp.Current.ShowToastSuccess("清理完成，共清理" + total.ToString() + "条数据", ToastLength.Long);
                //}
            }, () => { return true; });

            UpdateCommand = new Command(async () =>
            {
                await CheckAppVersionAsync();
            }, () => { return true; });

        }

        private async Task CheckAppVersionAsync()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                string content = await RestSharpService.GetNewestVersion();

                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                    return;
                }

                JObject jObject = JObject.Parse(content);

                if (jObject["Flag"].ToString() == "success")
                {
                    bool action = await Application.Current.MainPage.DisplayAlert("更新", "最新版本：" + jObject["Result"][0]["VersionCode"].ToString() + "\n" + jObject["Result"][0]["Remarks"].ToString(), "确定", "取消");
                    if (action)
                    {
                        await Browser.OpenAsync(jObject["Result"][0]["DownloadUrl"].ToString(), BrowserLaunchMode.SystemPreferred);
                    }
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("获取失败", ToastLength.Short);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
