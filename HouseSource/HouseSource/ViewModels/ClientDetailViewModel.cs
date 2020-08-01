using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class ClientDetailViewModel : BaseViewModel
    {
        private ClientItemInfo client;   //Comment
        public ClientItemInfo Client
        {
            get { return client; }
            set { SetProperty(ref client, value); }
        }

        public Command<Type> NavigateCommand { get; set; }
        public Command CallCommand { get; set; }

        public ClientDetailViewModel(ClientItemInfo clientItemInfo)
        {
            Client = clientItemInfo;

            NavigateCommand = new Command<Type>(async (pageName) =>
            {
                //Type type = Type.GetType(pageName);
                Page page = (Page)Activator.CreateInstance(pageName, Client.InquiryID, false);
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }, (pageName) => { return true; });

            CallCommand = new Command(async () =>
            {
                string action = await Application.Current.MainPage.DisplayActionSheet("联系客户" + Client.CustMobile, "取消", null, "打电话", "发短信");
                if (action == "打电话")
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(Client.CustMobile))
                        {
                            PhoneDialer.Open(Client.CustMobile);
                        }
                        else
                        {
                            CrossToastPopUp.Current.ShowToastError("电话号码为空", ToastLength.Short);
                        }
                    }
                    catch (FeatureNotSupportedException)
                    {
                        // Phone Dialer is not supported on this device.
                        CrossToastPopUp.Current.ShowToastError("该设备不支持拨号", ToastLength.Long);
                    }
                    catch (Exception)
                    {
                        // Other error has occurred.
                    }
                }
                else if (action == "发短信")
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(Client.CustMobile))
                        {
                            string result = await Application.Current.MainPage.DisplayPromptAsync("短信", "请输入短消息", "发送", "取消", "短消息（140个字以内）", 140, null);

                            if (result == null)
                            {
                                CrossToastPopUp.Current.ShowToastWarning("已取消", ToastLength.Long);
                            }
                            else if (result == "")
                            {
                                CrossToastPopUp.Current.ShowToastWarning("请输入短消息（140字以内）", ToastLength.Long);
                            }
                            else
                            {
                                var message = new SmsMessage()
                                {
                                    Body = result,
                                    Recipients = new List<string>() { Client.CustMobile }
                                };
                                await Sms.ComposeAsync(message);
                            }
                        }
                        else
                        {
                            CrossToastPopUp.Current.ShowToastError("电话号码为空", ToastLength.Long);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }, () => { return true; });
        }
    }
}
