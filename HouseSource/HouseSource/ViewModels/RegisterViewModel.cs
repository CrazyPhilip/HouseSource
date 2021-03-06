﻿using System;
using Xamarin.Forms;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using HouseSource.Views;
using Newtonsoft.Json;
using HouseSource.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using HouseSource.ResponseData;
using HouseSource.Controls;

namespace HouseSource.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private ObservableCollection<Area> areaList;   //Comment
        public ObservableCollection<Area> AreaList
        {
            get { return areaList; }
            set { SetProperty(ref areaList, value); }
        }

        private ObservableCollection<District> districtList;   //Comment
        public ObservableCollection<District> DistrictList
        {
            get { return districtList; }
            set { SetProperty(ref districtList, value); }
        }

        private string townName;   //Comment  //区域名
        public string TownName
        {
            get { return townName; }
            set { SetProperty(ref townName, value); }
        }

        private Area dBName;   //Comment
        public Area DBName
        {
            get { return dBName; }
            set { SetProperty(ref dBName, value); }
        }

        private string telOrEmpNo;   //手机号
        public string TelOrEmpNo
        {
            get { return telOrEmpNo; }
            set { SetProperty(ref telOrEmpNo, value); }
        }

        private string code;   //验证码
        public string Code
        {
            get { return code; }
            set { SetProperty(ref code, value); }
        }

        private string password;   //密码
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string realName;   //真实名字
        public string RealName
        {
            get { return realName; }
            set { SetProperty(ref realName, value); }
        }

        private bool isEnable;   //可否点击
        public bool IsEnable
        {
            get { return isEnable; }
            set { SetProperty(ref isEnable, value); }
        }

        private Color buttonColor;   //发送验证码按钮颜色
        public Color ButtonColor
        {
            get { return buttonColor; }
            set { SetProperty(ref buttonColor, value); }
        }

        private string authCodeButtonText;   //comment
        public string AuthCodeButtonText
        {
            get { return authCodeButtonText; }
            set { SetProperty(ref authCodeButtonText, value); }
        }

        private string authCode { get; set; }

        public MyTimer myTimer { get; set; }

        public Command SendCodeCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public Command ReturnCommand { get; set; }
        public Command<int> GetAreaCommand { get; set; }

        public RegisterViewModel()
        {
            AuthCodeButtonText = "发送验证码";
            IsEnable = true;
            ButtonColor = Color.FromHex("6ECBB8");

            SendCodeCommand = new Command(async () =>
            {
                try
                {
                    if (!Tools.IsNetConnective())
                    {
                        CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(TelOrEmpNo))
                    {
                        CrossToastPopUp.Current.ShowToastError("手机号或员工号不能为空", ToastLength.Short);
                        return;
                    }

                    if (!Tools.IsPhoneNumber(TelOrEmpNo))
                    {
                        CrossToastPopUp.Current.ShowToastError("手机号格式不正确", ToastLength.Short);
                        return;
                    }

                    string ifExist = await RestSharpService.CheckIfRegister(TelOrEmpNo);

                    if (!string.IsNullOrWhiteSpace(ifExist))
                    {
                        BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(ifExist);
                        string res = (string)baseResponse.Result;
                        if (res == "TelExist")
                        {
                            CrossToastPopUp.Current.ShowToastSuccess("手机号已经注册", ToastLength.Long);
                        }
                        else    // 获取验证码前 需要先检查手机号 是否已注册
                        {
                            string result = await RestSharpService.SendCode(TelOrEmpNo);
                            if (string.IsNullOrWhiteSpace(result))
                            {
                                CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                            }
                            else
                            {
                                BaseResponse myResponse = JsonConvert.DeserializeObject<BaseResponse>(result);
                                CrossToastPopUp.Current.ShowToastSuccess(result, ToastLength.Long);
                                authCode = (string)myResponse.Result;
                                myTimer = new MyTimer { EndDate = DateTime.Now.Add(new TimeSpan(900000000)) };
                                LoadAsync();
                                CrossToastPopUp.Current.ShowToastSuccess("请注意查收短信！", ToastLength.Short);
                            }
                        }
                    }
                    else  
                    {
                        CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                        return;
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }, () => { return true; });

            RegisterCommand = new Command(() =>
            {
                if (CheckInput())
                {
                    Register();
                }
            }, () => { return true; });

            GetAreaCommand = new Command<int>((index) =>
            {
                GetAreaByDistrict(DistrictList[index].town);  //直接放参数，没有拿到这个值
            }, (index) => { return true; });

            ReturnCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

            GetDistrictsByCity("cd");
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if(DBName == null)
            {
                CrossToastPopUp.Current.ShowToastError("区域不能为空", ToastLength.Short);
                return false;
            }
            if (string.IsNullOrWhiteSpace(DBName.dbName))
            {
                CrossToastPopUp.Current.ShowToastError("区域不能为空", ToastLength.Short);
                return false;
            }

            if (string.IsNullOrWhiteSpace(TelOrEmpNo))
            {
                CrossToastPopUp.Current.ShowToastError("手机号或员工号不能为空", ToastLength.Short);

                return false;
            }

            if (!Tools.IsPhoneNumber(TelOrEmpNo))
            {
                CrossToastPopUp.Current.ShowToastError("手机号格式不正确", ToastLength.Short);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Code) || !authCode.Contains(Code))
            {
                CrossToastPopUp.Current.ShowToastError("验证码有误", ToastLength.Short);
                return false;
            }

            if (Password.Length < 6)
            {
                CrossToastPopUp.Current.ShowToastError("密码太短", ToastLength.Short);
                return false;
            }

            if (string.IsNullOrWhiteSpace(RealName))
            {
                CrossToastPopUp.Current.ShowToastError("真实姓名为空", ToastLength.Short);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 注册
        /// </summary>
        private async void Register()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                //string content1 = await RestSharpService.CheckIfRegister(TelOrEmpNo);
                //if (string.IsNullOrWhiteSpace(content1))
                //{
                //    CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                //    return;
                //}
                //BaseResponse baseResponse1 = JsonConvert.DeserializeObject<BaseResponse>(content1);
                //if (baseResponse1.Flag == "success")
                //{
                //    if (baseResponse1.Result.ToString() == "TelExist")
                //    {
                //        CrossToastPopUp.Current.ShowToastError("电话号码已注册", ToastLength.Short);
                //        return;
                //    }
                //}

                string content = await RestSharpService.Register(DBName.dbName, TelOrEmpNo, Password, RealName, "");

                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("注册失败", ToastLength.Short);
                    return;
                }

                BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
                if (baseResponse.Flag == "success")
                {
                    CrossToastPopUp.Current.ShowToastSuccess("注册成功", ToastLength.Long);
                    // 注册成功后 ，自动保存全局的登录信息，并跳转界面 ，content中有除 PhotoUrl的其他变量

                    GlobalVariables.LoggedUser = JsonConvert.DeserializeObject<LoginRD>(baseResponse.Result.ToString());
                    //Console.Write(GlobalVariables.LoggedUser.ToString());
                    GlobalVariables.LoggedUser.PhotoUrl = null;  //注册成功后的headPic为空
                    GlobalVariables.IsLogged = true;
                    GlobalVariables.LoggedUser.EmpNo = TelOrEmpNo;
                    GlobalVariables.LoggedUser.DBName = DBName.dbName;

                    //MainPage mainPage = new MainPage();
                    //await Application.Current.MainPage.Navigation.PushAsync(mainPage);
                    MyNavigationPage myNavigationPage = new MyNavigationPage(new MainPage());
                    Application.Current.MainPage = myNavigationPage;
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError(baseResponse.Msg, ToastLength.Short);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取区域列表
        /// </summary>
        private async void GetDistrictsByCity(string cityName)
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.GetDistrictsByCity(cityName);

                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("服务器返回值为空", ToastLength.Short);
                    return;
                }
                else
                {
                    BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
                    if (baseResponse.Flag == "success")
                    {
                        List<District> list = JsonConvert.DeserializeObject<List<District>>(baseResponse.Result.ToString());
                        DistrictList = new ObservableCollection<District>(list);
                    }
                    else
                    {
                        DistrictList = new ObservableCollection<District>();
                    }


                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取街区列表
        /// </summary>
        private async void GetAreaByDistrict(string districtName)
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.GetAreaByDistrict(districtName);

                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("服务器返回值为空", ToastLength.Short);
                    return;
                }
                else
                {
                    BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
                    if (baseResponse.Flag == "success")
                    {
                        List<Area> list = JsonConvert.DeserializeObject<List<Area>>(baseResponse.Result.ToString());
                        AreaList = new ObservableCollection<Area>(list);
                    }
                    else
                    {
                        AreaList = new ObservableCollection<Area>();
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 计时器
        public void LoadAsync()
        {
            IsEnable = false;
            ButtonColor = Color.LightGray;
            myTimer.Start();
            myTimer.Ticked += OnCountdownTicked;
            myTimer.Completed += OnCountdownCompleted;
            //return Task.CompletedTask;
        }

        public Task UnloadAsync()
        {
            myTimer.Ticked -= OnCountdownTicked;
            myTimer.Completed -= OnCountdownCompleted;
            return Task.CompletedTask;
        }

        void OnCountdownTicked()
        {
            AuthCodeButtonText = string.Format("{0}秒后再试", myTimer.RemainTime.TotalSeconds.ToString().Split('.')[0]);
        }

        void OnCountdownCompleted()
        {
            AuthCodeButtonText = "发送验证码";
            IsEnable = true;
            ButtonColor = Color.FromHex("6ECBB8");
            UnloadAsync();
        }

        #endregion
    }
}
