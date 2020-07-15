﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using HouseSource.Models;
using HouseSource.Utils;
using HouseSource.Themes;

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
        /// 登出
        /// </summary>
        private void LoginOut()
        {

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
                    new Option { icon = "moon.png", option = "夜间模式", page = "DarkMode" },
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
