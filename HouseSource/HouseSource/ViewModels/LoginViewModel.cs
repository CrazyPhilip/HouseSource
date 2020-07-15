﻿using System;
using Xamarin.Forms;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Newtonsoft.Json.Linq;
using HouseSource.Models;
using Newtonsoft.Json;
using HouseSource.Controls;
using HouseSource.Views;

namespace HouseSource.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
		private string telOrEmpNo;   //手机号或编号
		public string TelOrEmpNo
		{
			get { return telOrEmpNo; }
			set { SetProperty(ref telOrEmpNo, value); }
		}

		private string password;   //密码
		public string Password
		{
			get { return password; }
			set { SetProperty(ref password, value); }
		}

		public Command LoginCommand { get; set; }   //登录命令事件

		public LoginViewModel()
		{
			TelOrEmpNo = "18428333654";
			Password = "Philip1995641418";

			//登录命令事件
			LoginCommand = new Command(() =>
			{
				Login();
			}, () => { return true; });

		}

		/// <summary>
		/// 响应登录事件
		/// </summary>
		private async void Login()
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

				if (string.IsNullOrWhiteSpace(Password))
				{
					CrossToastPopUp.Current.ShowToastError("密码不能为空", ToastLength.Short);
					return;
				}

				string result = await RestSharpService.Login("cd", TelOrEmpNo, Password);

				if (string.IsNullOrWhiteSpace(result))
				{
					CrossToastPopUp.Current.ShowToastError("服务器返回值为空", ToastLength.Short);
					return;
				}
				else
				{
					JObject jObject = JObject.Parse(result);
					

					if (jObject["Msg"].ToString() == "success")
					{
						GlobalVariables.LoggedUser = JsonConvert.DeserializeObject<UserInfo>(result);
						GlobalVariables.IsLogged = true;
						GlobalVariables.LoggedUser.EmpNo = TelOrEmpNo;

						MainPage mainPage = new MainPage();
						
						await Application.Current.MainPage.Navigation.PushAsync(mainPage);
					}
					else
					{
						CrossToastPopUp.Current.ShowToastError("登录错误", ToastLength.Short);
						return;
					}

				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
