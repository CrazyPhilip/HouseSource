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
using HouseSource.ResponseData;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace HouseSource.ViewModels
{
    public class HouseDetailViewModel2 : BaseViewModel
    {
        private HouseInfo2 house;   //Comment
        public HouseInfo2 House
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

        private ObservableCollection<Comment> commentList;   //Comment
        public ObservableCollection<Comment> CommentList
        {
            get { return commentList; }
            set { SetProperty(ref commentList, value); }
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

        private bool visible;   //Comment
        public bool Visible
        {
            get { return visible; }
            set { SetProperty(ref visible, value); }
        }

        public Command ShareCommand { get; set; }
        public Command CollectCommand { get; set; }
        public Command CallCommand { get; set; }
        public Command<string> CarouselTappedCommand { get; set; }

        public HouseDetailViewModel2(HouseInfo2 houseInfo)
        {
            House = houseInfo;
            //HouseTitle = house.DistrictName + " " + house.AreaName + " " + house.EstateName;
            //House.PhotoUrl = string.IsNullOrWhiteSpace(House.proCoverUrl) ? "NullPic.jpg" : House.PhotoUrl;
            //Visible = House.EmpID == GlobalVariables.LoggedUser.EmpID;

            //CheckCollected();

            GetPhotos();

            GetComments();

            ShareCommand = new Command(() =>
            {
                var page = new ShareView(House.proId.ToString());
                PopupNavigation.Instance.PushAsync(page);
            }, () => { return true; });

            CallCommand = new Command(async () =>
            {
                string action = await Application.Current.MainPage.DisplayActionSheet("联系发布人" + House.proEmployee1Phone, "取消", null, "打电话", "发短信");
                if (action == "打电话")
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(House.proEmployee1Phone))
                        {
                            PhoneDialer.Open(House.proEmployee1Phone);
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
                        if (!string.IsNullOrWhiteSpace(House.proEmployee1Phone))
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
                                    Recipients = new List<string>() { House.proEmployee1Phone }
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
                //CollectOrCancel();
            }, () => { return true; });

            CarouselTappedCommand = new Command<string>((p) =>
            {
                var page = new PhotoView(p);
                PopupNavigation.Instance.PushAsync(page);
            }, (p) => { return true; });

        }

        /// <summary>
        /// 获取图片列表
        /// </summary>
        private void GetPhotos()
        {
            try
            {
                /*
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
                }*/

                if (string.IsNullOrWhiteSpace(House.proPhotoUrl))
                {
                    PhotoList = new List<string> { "NullPic.jpg" };
                }
                else
                {
                    PhotoList = new List<string>(House.proPhotoUrl.Split(','));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
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

                string content = IsCollected ? await RestSharpService.DeleteCollect(House.proId.ToString(), GlobalVariables.LoggedUser.telephone, "510100", "1")
                    : await RestSharpService.Collect(House.proId.ToString(), GlobalVariables.LoggedUser.telephone, "510100", "1");

                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("未知错误，操作未成功", ToastLength.Short);
                    return;
                }

                JObject jObject = JObject.Parse(content);
                if (jObject["code"].ToString() == "200")
                {
                    IsCollected = !IsCollected;
                    StarSource = IsCollected ? "star_yellow.png" : "star.png";
                    CrossToastPopUp.Current.ShowToastSuccess(jObject["message"].ToString(), ToastLength.Short);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError(jObject["message"].ToString(), ToastLength.Short);
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

                string content = await RestSharpService.CheckCollection(GlobalVariables.LoggedUser.telephone, House.proId.ToString(), "chengdu");
                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("查询收藏状态失败", ToastLength.Short);
                    return;
                }

                JObject jObject = JObject.Parse(content);

                if ((bool)jObject["data"]["res"])
                {
                    StarSource = "star_yellow.png";
                    IsCollected = true;
                }
                else
                {
                    StarSource = "star.png";
                    IsCollected = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        */

        /// <summary>
        /// 获取评论
        /// </summary>
        private async void GetComments()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.GetComents("chengdu", House.proId.ToString());
                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                    return;
                }

                JObject jObject = JObject.Parse(content);

                if (jObject["code"].ToString() == "200")
                {
                    List<Comment> list = JsonConvert.DeserializeObject<List<Comment>>(jObject["data"].ToString());

                    CommentList = new ObservableCollection<Comment>(list);
                }
                else
                {
                    CommentList = new ObservableCollection<Comment>();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
