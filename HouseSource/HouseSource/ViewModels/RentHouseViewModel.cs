using HouseSource.Models;
using HouseSource.ResponseData;
using HouseSource.Services;
using HouseSource.Utils;
using HouseSource.Views;
using Newtonsoft.Json;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class RentHouseViewModel : BaseViewModel
    {
        private string[] DistrictList { get; set; }   //区域列表
        private string[] RoomStyleList { get; set; }   //房型列表
        private string[] SalePriceList { get; set; }   //价格列表
        private string[] SquareList { get; set; }   //面积列表
        private string[] StatusList { get; set; }   //状态列表

        private string district;   //区域
        public string District
        {
            get { return district; }
            set { SetProperty(ref district, value); }
        }

        private string roomStyle;   //房型
        public string RoomStyle
        {
            get { return roomStyle; }
            set { SetProperty(ref roomStyle, value); }
        }

        private string salePrice;   //价格
        public string SalePrice
        {
            get { return salePrice; }
            set { SetProperty(ref salePrice, value); }
        }

        private string square;   //面积
        public string Square
        {
            get { return square; }
            set { SetProperty(ref square, value); }
        }

        private string status;   //状态
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        private string searchContent;   //搜索关键字
        public string SearchContent
        {
            get { return searchContent; }
            set { SetProperty(ref searchContent, value); }
        }

        private ObservableCollection<HouseItemInfo> houseItemList;    //售房列表
        public ObservableCollection<HouseItemInfo> HouseItemList
        {
            get { return houseItemList; }
            set { SetProperty(ref houseItemList, value); }
        }

        private List<HouseItemInfo> rentHouseItemList { get; set; }    //租房列表
        private List<HouseInfo> rentHouseList { get; set; }    //租房列表  原始

        private HousePara saleHousePara;   //请求参数
        public HousePara SaleHousePara
        {
            get { return saleHousePara; }
            set { SetProperty(ref saleHousePara, value); }
        }

        private bool isRefreshing;   //Comment
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public Command RefreshCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command<string> SortCommand { get; set; }
        public Command<string> TappedCommand { get; set; }

        public RentHouseViewModel()
        {
            DistrictList = new[] { "全部区域", "青白江区", "郫都区", "金牛区", "成华区", "高新西区", "武侯区", "锦江区", "高新区", "天府新区", "温江区", "新都区", "青羊区", "双流区", "龙泉驿区" };
            RoomStyleList = new[] { "全部房型", "1房", "2房", "3房", "4房及以上" };
            SalePriceList = new[] { "全部价格", "0-30万", "30-50万", "50-100万", "100-150万", "150-200万", "200-300万", "300-500万", "500万以上" };
            SquareList = new[] { "全部面积", "0-20平", "20-50平", "50-100平", "100-150平", "150-200平", "200-250平", "250-500平", "500-800平", "800平以上" };
            StatusList = new[] { "全部状态", "有效", "暂缓", "无效" };

            District = DistrictList[0];
            RoomStyle = RoomStyleList[0];
            SalePrice = SalePriceList[0];
            Square = SquareList[0];
            Status = StatusList[0];

            HouseItemList = new ObservableCollection<HouseItemInfo>();
            rentHouseItemList = new List<HouseItemInfo>();
            rentHouseList = new List<HouseInfo>();

            SaleHousePara = new HousePara
            {
                DistrictName = "区域",
                CountF = "房型",
                Price = "价格",
                Square = "面积",
                PropertyUsage = "用途",
                EstateName = "",
                BuildNo = "",
                RoomNo = "",
                PanType = "有效",
                Floor = "",
                MinPrice = "",
                MaxPrice = "",
                EmpID = GlobalVariables.LoggedUser.EmpID
            };

            SortCommand = new Command<string>(async (t) =>
            {
                switch (t)
                {
                    //区域
                    case "0":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("区域", "取消", null, DistrictList);
                            District = result == null || result == "取消" ? District : result;
                            SaleHousePara.DistrictName = District == "全部区域" ? "区域" : District.TrimEnd('区');
                            GetHouseList();
                        }
                        break;

                    //房型
                    case "1":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("房型", "取消", null, RoomStyleList);
                            RoomStyle = result == null || result == "取消" ? RoomStyle : result;
                            SaleHousePara.CountF = RoomStyle == "全部房型" ? "房型" : RoomStyle;
                            GetHouseList();
                        }
                        break;

                    //价格
                    case "2":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("价格", "取消", null, SalePriceList);
                            SalePrice = result == null || result == "取消" ? SalePrice : result;
                            SaleHousePara.Price = SalePrice == "全部价格" ? "价格" : SalePrice;
                            GetHouseList();
                        }
                        break;

                    //面积
                    case "3":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("面积", "取消", null, SquareList);
                            Square = result == null || result == "取消" ? Square : result;
                            SaleHousePara.Square = Square == "全部面积" ? "面积" : Square;
                            GetHouseList();
                        }
                        break;

                    //状态
                    case "4":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("状态", "取消", null, StatusList);
                            Status = result == null || result == "取消" ? Status : result;
                            SaleHousePara.PanType = Status == "全部状态" ? "状态" : Status;
                            GetHouseList();
                        }
                        break;

                    default:
                        break;
                }
            }, (t) => { return true; });

            SearchCommand = new Command(() =>
            {
                GetHouseList();
            }, () => { return true; });

            TappedCommand = new Command<string>((h) =>
            {
                rentHouseList.ForEach((item) =>
                {
                    if (item.PropertyID == h)
                    {
                        HouseDetailPage houseDetailPage = new HouseDetailPage(item);
                        Application.Current.MainPage.Navigation.PushAsync(houseDetailPage);
                    }
                });
            }, (h) => { return true; });

            RefreshCommand = new Command(() =>
            {
                IsRefreshing = true;
                GetHouseList();
                IsRefreshing = false;
            }, () => { return true; });

            GetHouseList();
        }

        /// <summary>
        /// 获取房源列表
        /// </summary>
        private async void GetHouseList()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                SaleHousePara.SearchContent = SearchContent;
                string content = await RestSharpService.GetHouseAsync(SaleHousePara, "出租");
                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                    return;
                }

                BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

                if (baseResponse.Flag == "success")
                {


                    List<HouseInfo> houseList = JsonConvert.DeserializeObject<List<HouseInfo>>(baseResponse.Result.ToString());

                    List<HouseItemInfo> list = new List<HouseItemInfo>();
                    foreach (var h in houseList)
                    {
                        HouseItemInfo houseItemInfo = new HouseItemInfo
                        {
                            //houseItemInfo.HouseTitle = h.Title == "" ? h.DistrictName + " " + h.AreaName + " " + h.EstateName : h.Title;
                            HouseTitle = h.DistrictName + " " + h.AreaName + " " + h.EstateName,
                            RoomStyle = ((h.CountF.Length == 0 || h.CountF == " ") ? "-" : h.CountF) + "室"
                            + ((h.CountT.Length == 0 || h.CountT == " ") ? "-" : h.CountT) + "厅"
                            + ((h.CountW.Length == 0 || h.CountW == " ") ? "-" : h.CountW) + "卫",
                            Square = (h.Square.Length > 5 ? h.Square.Substring(0, 5) : h.Square) + "㎡",
                            EstateName = h.EstateName,
                            Price = h.Price.Substring(0, h.Price.Length - 2) + "万元",
                            SinglePrice = h.Trade == "出售" ? h.RentPrice.Substring(0, h.RentPrice.Length - 2) + "元/平" : "",
                            PhotoUrl = (h.PhotoUrl == "" ? "NullPic.jpg" : h.PhotoUrl),
                            PropertyID = h.PropertyID
                        };

                        switch (h.Privy)
                        {
                            case "0": houseItemInfo.PanType = "公盘"; break;
                            case "1": houseItemInfo.PanType = "私盘"; break;
                            case "2": houseItemInfo.PanType = "特盘"; break;
                            default: houseItemInfo.PanType = "封盘"; break;
                        }

                        houseItemInfo.PropertyDecoration = h.PropertyDecoration;
                        houseItemInfo.PropertyLook = h.PropertyLook;

                        list.Add(houseItemInfo);
                    }

                    rentHouseList.Clear();
                    rentHouseItemList.Clear();
                    HouseItemList.Clear();

                    rentHouseList.AddRange(houseList);
                    rentHouseItemList.AddRange(list);
                    rentHouseItemList.ForEach(item => { HouseItemList.Add(item); });

                    CrossToastPopUp.Current.ShowToastSuccess(baseResponse.Msg, ToastLength.Short);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastWarning(baseResponse.Msg, ToastLength.Short);
                    return;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
