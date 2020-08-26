using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using HouseSource.Models;
using HouseSource.Utils;
using HouseSource.Themes;
using HouseSource.Views;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using HouseSource.Services;
using Newtonsoft.Json.Linq;
using System.IO;
using Xamarin.Essentials;

namespace HouseSource.ViewModels
{
    public class MeViewModel : BaseViewModel
    {
        private UserInfo user;   //用户信息
        public UserInfo User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        private ObservableCollection<Option> optionList;   //选项列表
        public ObservableCollection<Option> OptionList
        {
            get { return optionList; }
            set { SetProperty(ref optionList, value); }
        }

        public Command<string> NavigateCommand { get; set; }    //导航命令事件

        public MeViewModel()
        {
            InitMePage();

            //导航命令事件
            NavigateCommand = new Command<string>(async (pageName) =>
            {
                switch (pageName)
                {
                    case "": { } break;
                    case null: { } break;
                    //登出
                    case "LogOut":
                        {
                            bool action = await Application.Current.MainPage.DisplayAlert("退出登录", "确定要退出登录吗？", "确定", "取消");
                            if (action)
                            {
                                LoginOut();
                            }
                        }
                        break;

                    //业务办理
                    case "Business":
                        {
                            ToBusiness();
                        }
                        break;

                    //夜间模式
                    case "DarkMode":
                        {
                            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                            if (mergedDictionaries != null)
                            {
                                mergedDictionaries.Clear();
                                mergedDictionaries.Add(new DarkTheme());
                            }

                            OptionList[3] = new Option { icon = "sun.png", option = "日间模式", page = "LightMode" };
                        }
                        break;

                    //日间模式
                    case "LightMode":
                        {
                            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                            if (mergedDictionaries != null)
                            {
                                mergedDictionaries.Clear();
                                mergedDictionaries.Add(new LightTheme());
                            }
                            OptionList[3] = new Option { icon = "moon.png", option = "夜间模式", page = "DarkMode" };
                        }
                        break;

                    //默认跳转到其他页面
                    default:
                        {
                            Type type = Type.GetType(pageName);
                            Page page = (Page)Activator.CreateInstance(type);
                            await Application.Current.MainPage.Navigation.PushAsync(page);
                            //await Application.Current.MainPage.Navigation.PushModalAsync(page);
                        }
                        break;
                }
            }, (pageName) => { return true; });

        }

        /// <summary>
        /// 业务办理
        /// </summary>
        private async void ToBusiness()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.GetBusinessHand();
                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("服务器出错", ToastLength.Short);
                    return;
                }
                else
                {
                    JObject jObject = JObject.Parse(content);
                    WebPage webPage = new WebPage(jObject["ShareUrl"].ToString(), "业务办理");
                    await Application.Current.MainPage.Navigation.PushAsync(webPage);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        private void LoginOut()
        {
            GlobalVariables.IsLogged = false;

            string fileName = Path.Combine(FileSystem.CacheDirectory, "log_autoLogin.dat");
            File.Delete(fileName);

            LoginPage loginPage = new LoginPage();
            Application.Current.MainPage = loginPage;
            //Application.Current.MainPage.Navigation.PushAsync(loginPage);
            //Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[0]);
        }

        /// <summary>
        /// 初始化我的页面
        /// </summary>
        private void InitMePage()
        {
            try
            {
                OptionList = new ObservableCollection<Option>
                {
                    new Option { icon = "edit.png", option = "修改个人信息", page = "HouseSource.Views.EditUserInfoPage"},
                    new Option { icon = "star.png", option = "我的收藏", page = "HouseSource.Views.CollectionPage"},
                    new Option { icon = "service.png", option = "业务办理", page = "Business" },
                    new Option { icon = "moon.png", option = "设置", page = "HouseSource.Views.SettingPage" },
                    new Option { icon = "poweroff.png", option = "退出登录", page = "LogOut" }
                };

                User = GlobalVariables.LoggedUser;
                User.PhotoUrl = string.IsNullOrWhiteSpace(User.PhotoUrl) ? "avatar.png" : User.PhotoUrl;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class Option
    {
        public string option { get; set; }
        public string icon { get; set; }
        public string page { get; set; }
    }
}
