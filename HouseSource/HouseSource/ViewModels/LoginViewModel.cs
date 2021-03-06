﻿using System;
using Xamarin.Forms;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Newtonsoft.Json.Linq;
using HouseSource.Models;
using Newtonsoft.Json;
using HouseSource.Views;
using System.IO;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;
using HouseSource.Controls;
using HouseSource.ResponseData;
using System.Collections.Generic;

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

		private bool isRememberPwd;   //是否记住密码
		public bool IsRememberPwd
		{
			get { return isRememberPwd; }
			set { SetProperty(ref isRememberPwd, value); }
		}

		private string eyeSource;   //Comment
		public string EyeSource
		{
			get { return eyeSource; }
			set { SetProperty(ref eyeSource, value); }
		}

		private bool isPassword;   //Comment
		public bool IsPassword
		{
			get { return isPassword; }
			set { SetProperty(ref isPassword, value); }
		}

		private bool isLoading;   //准备中
		public bool IsLoading
		{
			get { return isLoading; }
			set { SetProperty(ref isLoading, value); }
		}

		private string isRememberFileName { get; set; }

		private string autoLoginFileName { get; set; }

		public Command LoginCommand { get; set; }   //登录命令事件
		public Command ToRegisterCommand { get; set; }   //注册命令事件
		public Command ToResetPasswordCommand { get; set; }   //重置密码命令事件
		public Command RememberPwdCommand { get; private set; }   //记住密码
		public Command OpenEyeCommand { get; private set; }

		public LoginViewModel()
		{
			//TelOrEmpNo = "18428333654";
			//Password = "Philip1995641418";

			GetReadPermissionAsync();

			autoLoginFileName = Path.Combine(FileSystem.CacheDirectory, "log_autoLogin.dat");
			if (File.Exists(autoLoginFileName))
			{
				string[] text = File.ReadAllLines(autoLoginFileName);
				string check = text[0].Substring(6);
				string tel = text[1].Substring(8);
				string pwd = text[2].Substring(9);
				string loginDate = text[3].Substring(10);
				DateTime nowDate = DateTime.Now;
				DateTime lastLoginDate = DateTime.Parse(loginDate);
				TimeSpan ts = nowDate - lastLoginDate;
				if (check == "Checked" && ts.TotalDays <= 30)
				{
					TelOrEmpNo = tel;
					Password = pwd;
					IsLoading = true;
					IsRememberPwd = true;
					Device.StartTimer(new TimeSpan(0, 0, 2), () =>
					{
						Login();        // do something every 2 seconds
						Device.BeginInvokeOnMainThread(() =>
						{
							// interact with UI elements]
						});
						return false;   // runs again, or false to stop
					});
				}
			}

			IsPassword = true;
			EyeSource = "closed_eye.png";

			//登录命令事件
			LoginCommand = new Command(() =>
			{
				Login();
			}, () => { return true; });

			ToRegisterCommand = new Command(() =>
			{
				RegisterPage2 registerPage2 = new RegisterPage2();
				//Application.Current.MainPage.Navigation.PushAsync(registerPage);
				//MyNavigationPage myNavigationPage = new MyNavigationPage(registerPage);
				Application.Current.MainPage.Navigation.PushModalAsync(registerPage2);
			}, () => { return true; });

			ToResetPasswordCommand = new Command(() =>
			{
				ResetPasswordPage2 resetPasswordPage2 = new ResetPasswordPage2();
				//Application.Current.MainPage.Navigation.PushAsync(resetPasswordPage);
				//MyNavigationPage myNavigationPage = new MyNavigationPage(resetPasswordPage);
				Application.Current.MainPage.Navigation.PushModalAsync(resetPasswordPage2);
			}, () => { return true; });

			RememberPwdCommand = new Command(() =>
			{
				/*
				string text = "";

				if (!string.IsNullOrWhiteSpace(TelOrEmpNo) && !string.IsNullOrWhiteSpace(Password))
				{
					if (IsRememberPwd)
					{
						text = "State:Checked\n" + "Account:" + TelOrEmpNo + "\n" + "Password:" + Password;
						File.WriteAllText(isRememberFileName, text);
					}
					else
					{
						text = "State:Unchecked\nAccount:\nPassword:\n";
						File.WriteAllText(isRememberFileName, text);
					}
				}
				else
				{
					//await DisplayAlert("错误", "请输入账号及密码！", "OK");
				}
				*/
			}, () => { return true; });

			OpenEyeCommand = new Command(() =>
			{
				if (IsPassword)
				{
					IsPassword = false;
					EyeSource = "open_eye.png";
				}
				else
				{
					IsPassword = true;
					EyeSource = "closed_eye.png";
				}
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
					IsLoading = false;
					return;
				}

				if (string.IsNullOrWhiteSpace(TelOrEmpNo))
				{
					CrossToastPopUp.Current.ShowToastError("手机号或员工号不能为空", ToastLength.Short);
					IsLoading = false;
					return;
				}

				if (string.IsNullOrWhiteSpace(Password))
				{
					CrossToastPopUp.Current.ShowToastError("密码不能为空", ToastLength.Short);
					IsLoading = false;
					return;
				}

				string result = await RestSharpService.Login(TelOrEmpNo, Password);

				if (string.IsNullOrWhiteSpace(result))
				{
					CrossToastPopUp.Current.ShowToastError("服务器返回值为空", ToastLength.Short);
					IsLoading = false;
					return;
				}
				else
				{
					BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(result);

					if (baseResponse.Flag == "success")
					{
						//GlobalVariables.LoggedUser = JsonConvert.DeserializeObject<UserInfo>(result);
						//CrossToastPopUp.Current.ShowToastSuccess(baseResponse.Msg, ToastLength.Short);

						LoginRD loginRD = JsonConvert.DeserializeObject<LoginRD>(baseResponse.Result.ToString());
						loginRD.DBName = loginRD.DBName.TrimStart('[');

						GlobalVariables.LoggedUser = loginRD;
						GlobalVariables.IsLogged = true;
						GlobalVariables.LoggedUser.EmpNo = TelOrEmpNo;
						//提供自动登录的信息  除非用户手动退出登录 LoginState变为False
						string dateNow = DateTime.Now.ToString();
						string text = "";
						if (IsRememberPwd)
						{
							text = "State:Checked\n" + "Account:" + TelOrEmpNo + "\n" + "Password:" + Password + "\n" + "LoginDate:" + dateNow;
							File.WriteAllText(autoLoginFileName, text);
						}

						//MainPage mainPage = new MainPage();
						//await Application.Current.MainPage.Navigation.PushAsync(mainPage);
						MyNavigationPage myNavigationPage = new MyNavigationPage(new MainPage());
						Application.Current.MainPage = myNavigationPage;
					}
					else
					{
						CrossToastPopUp.Current.ShowToastError(baseResponse.Msg, ToastLength.Short);
						IsLoading = false;
						return;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// 获取权限
		/// </summary>
		/// <returns></returns>
		private async void GetReadPermissionAsync()
		{
			var status = await Tools.CheckAndRequestPermissionAsync(new Permissions.StorageRead());
			if (status != PermissionStatus.Granted)
			{
				CrossToastPopUp.Current.ShowToastMessage("获取存储权限：" + status, ToastLength.Long);
				return;
			}
		}
	}
}