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

		private string fileName { get; set; }

		public Command LoginCommand { get; set; }   //登录命令事件
		public Command ToRegisterCommand { get; set; }   //注册命令事件
		public Command ToResetPasswordCommand { get; set; }   //重置密码命令事件
		public Command RememberPwdCommand { get; private set; }   //记住密码
		public Command OpenEyeCommand { get; private set; }

		public LoginViewModel()
		{
			//TelOrEmpNo = "18428333654";
			//Password = "Philip1995641418";

			fileName = Path.Combine(FileSystem.CacheDirectory, "log.dat");
			if (File.Exists(fileName))
			{
				string[] text = File.ReadAllLines(fileName);
				string check = text[0].Substring(6);
				string tel = text[1].Substring(8);
				string pwd = text[2].Substring(9);

				if (check == "Checked")
				{
					TelOrEmpNo = tel;
					Password = pwd;
					IsRememberPwd = true;
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
				RegisterPage registerPage = new RegisterPage();
				Application.Current.MainPage.Navigation.PushAsync(registerPage);
			}, () => { return true; });

			ToResetPasswordCommand = new Command(() =>
			{
				ResetPasswordPage resetPasswordPage = new ResetPasswordPage();
				Application.Current.MainPage.Navigation.PushAsync(resetPasswordPage);
			}, () => { return true; });

			RememberPwdCommand = new Command(() =>
			{
				string text = "";

				if (!string.IsNullOrWhiteSpace(TelOrEmpNo) && !string.IsNullOrWhiteSpace(Password))
				{
					if (IsRememberPwd)
					{
						text = "State:Checked\n" + "Account:" + TelOrEmpNo + "\n" + "Password:" + Password;
						File.WriteAllText(fileName, text);
					}
					else
					{
						text = "State:Unchecked\nAccount:\nPassword:\n";
						File.WriteAllText(fileName, text);
					}
				}
				else
				{
					//await DisplayAlert("错误", "请输入账号及密码！", "OK");
				}
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
