using System;
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

			isRememberFileName = Path.Combine(FileSystem.CacheDirectory, "log_isRemember.dat");
			autoLoginFileName = Path.Combine(FileSystem.CacheDirectory, "log_autoLogin.dat");
			if (File.Exists(isRememberFileName))
			{
				string[] text = File.ReadAllLines(isRememberFileName);
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
			//记住密码和自动登录没关系？ 
			if (File.Exists(autoLoginFileName))
			{
				string[] text = File.ReadAllLines(autoLoginFileName);
				string loginState = text[0].Substring(11);
				string tel = text[1].Substring(8);
				string pwd = text[2].Substring(9);
				string loginDate = text[3].Substring(10);
				DateTime nowDate = DateTime.Now;
				DateTime lastLoginDate = DateTime.Parse(loginDate);
				TimeSpan ts = nowDate - lastLoginDate;
				if (loginState == "True" && ts.TotalDays <= 30) 
				{
					TelOrEmpNo = tel;
					Password = pwd;
					IsLoading = true;
					Device.StartTimer(new TimeSpan(0, 0, 2), () =>
					{
						// do something every 2 seconds
						Login();
						Device.BeginInvokeOnMainThread(() =>
						{
							// interact with UI elements]
						});
						return false; // runs again, or false to stop
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
			//IsLoading = false;
			//await Task.Delay(3000);
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

				string result = await RestSharpService.Login(TelOrEmpNo, Password);

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
						//GlobalVariables.LoggedUser = JsonConvert.DeserializeObject<UserInfo>(result);
						GlobalVariables.LoggedUser = new UserInfo
						{
							DBName = jObject["DBName"].ToString(),
							EmpID = jObject["EmpID"].ToString(),
							EmpName = jObject["EmpName"].ToString(),
							PhotoUrl = jObject["PhotoUrl"].ToString(),
							AccountStyle = jObject["AccountStyle"].ToString(),
							CompanyOrEstateName = jObject["CompanyOrEstateName"].ToString()
						};

						GlobalVariables.IsLogged = true;
						GlobalVariables.LoggedUser.EmpNo = TelOrEmpNo;
						//提供自动登录的信息  除非用户手动退出登录 LoginState变为False
						string dateNow = DateTime.Now.ToString();
						string text = "LoginState:True\n" + "Account:" + TelOrEmpNo + "\n" + "Password:" + Password + "\n" + "LoginDate:" + dateNow;
						File.WriteAllText(autoLoginFileName, text);
					
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
