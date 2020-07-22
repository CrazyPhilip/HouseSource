using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;
using HouseSource.Utils;
using HouseSource.Views;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using HouseSource.Services;
using Xamarin.Essentials;

namespace HouseSource.ViewModels
{
    public class HouseDetailViewModel : BaseViewModel
    {
        private HouseInfo house;   //Comment
        public HouseInfo House
        {
            get { return house; }
            set { SetProperty(ref house, value); }
        }

        private List<string> photoList;   //Comment
        public List<string> PhotoList
        {
            get { return photoList; }
            set { SetProperty(ref photoList, value); }
        }

        private string houseTitle;   //Comment
        public string HouseTitle
        {
            get { return houseTitle; }
            set { SetProperty(ref houseTitle, value); }
        }

        private bool isCollected;   //是否收藏
        public bool IsCollected
        {
            get { return isCollected; }
            set { SetProperty(ref isCollected, value); }
        }

        private string starSource;   //收藏图标
        public string StarSource
        {
            get { return starSource; }
            set { SetProperty(ref starSource, value); }
        }

        public Command ShareCommand { get; set; }
        public Command CollectCommand { get; set; }
        public Command CallCommand { get; set; }
        public Command<string> CarouselTappedCommand { get; set; }

        public HouseDetailViewModel(HouseInfo houseInfo)
        {
            House = houseInfo;
            HouseTitle = house.DistrictName + " " + house.AreaName + " " + house.EstateName;
            House.PhotoUrl = string.IsNullOrWhiteSpace(House.PhotoUrl) ? "NullPic.jpg" : House.PhotoUrl;

            CheckCollected();

            GetPhotos();

            ShareCommand = new Command(() =>
            {
                var page = new ShareView(House.PropertyID);
                PopupNavigation.Instance.PushAsync(page);
            }, () => { return true; });

            CallCommand = new Command(async () =>
            {
                string action = await Application.Current.MainPage.DisplayActionSheet("联系发布人" + House.tel, "取消", null, "打电话", "发短信");
                if (action == "打电话")
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(House.tel))
                        {
                            PhoneDialer.Open(House.tel);
                        }
                        else
                        {
                            CrossToastPopUp.Current.ShowToastError("电话号码为空", ToastLength.Long);
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
                        if (!string.IsNullOrWhiteSpace(House.tel))
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
                                    Recipients = new List<string>() { House.tel }
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

            CollectCommand = new Command(() =>
            {
                CollectOrCancel();
            }, () => { return true; });

            CarouselTappedCommand = new Command<string>((p) =>
            {
                var page = new PhotoView(p);
                PopupNavigation.Instance.PushAsync(page);
            }, (p) => { return true; });

        }

        private async void GetPhotos()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.GetPhotosByPropertyID(House.PropertyID);
                if (content == "EmptyList")
                {
                    PhotoList = new List<string> { "NullPic.jpg" };
                }
                else
                {
                    string[] array = content.TrimStart('{').TrimEnd('}').Split(',');
                    PhotoList = new List<string>(array);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 收藏或取消收藏
        /// </summary>
        private async void CollectOrCancel()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.CollectOrCancel(House.PropertyID);
                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("未知错误，操作未成功", ToastLength.Short);
                    return;
                }

                if (content.Contains("CollectSuccess"))
                {
                    CrossToastPopUp.Current.ShowToastSuccess("收藏成功", ToastLength.Short);
                    StarSource = "star_yellow.png";
                    IsCollected = true;
                    return;
                }
                else if (content.Contains("CollectFail"))
                {
                    CrossToastPopUp.Current.ShowToastError("收藏失败，请稍后再试", ToastLength.Short);
                    return;
                }
                else if (content.Contains("CancelSuccess"))
                {
                    CrossToastPopUp.Current.ShowToastSuccess("取消收藏成功", ToastLength.Short);
                    StarSource = "star.png";
                    IsCollected = false;
                    return;
                }
                else if (content.Contains("CancelFail"))
                {
                    CrossToastPopUp.Current.ShowToastError("取消收藏失败，请稍后再试", ToastLength.Short);
                    return;
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("未知错误，操作未成功", ToastLength.Short);
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 检查该商品是否被收藏
        /// </summary>
        private async void CheckCollected()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.CheckCollection(House.PropertyID);
                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("查询收藏状态失败", ToastLength.Short);
                    return;
                }

                if (content.Contains("Uncollected"))
                {
                    StarSource = "star.png";
                    IsCollected = false;
                }
                else
                {
                    StarSource = "star_yellow.png";
                    IsCollected = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
