using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace HouseSource.ViewModels
{
    class ResetPasswordViewModel : BaseViewModel
    {
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

        public ResetPasswordViewModel()
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
                        JObject jObject = JObject.Parse(ifExist);
                        if (jObject["Msg"].ToString() == "NoExist")
                        {
                            CrossToastPopUp.Current.ShowToastSuccess("手机号未注册", ToastLength.Long);
                        }
                        else  // 获取验证码前 需要先检查手机号 是否未注册
                        {
                            string result = await RestSharpService.SendCode(TelOrEmpNo);
                            if (string.IsNullOrWhiteSpace(result))
                            {
                                CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                            }
                            else
                            {
                                authCode = result;
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
                    ResetPassword();
                }
            }, () => { return true; });


        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
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



            return true;
        }

        /// <summary>
        ///重置密码
        /// </summary>
        private async void ResetPassword()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.ResetPassword(TelOrEmpNo, Password);

                if (!string.IsNullOrWhiteSpace(content))
                {
                    JObject jObject = JObject.Parse(content);
                    if (jObject["Msg"].ToString() == "success")
                    {
                        CrossToastPopUp.Current.ShowToastSuccess("密码重置成功", ToastLength.Long);
                    }
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("密码重置失败，请稍后重试", ToastLength.Short);
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
