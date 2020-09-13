using HouseSource.Models;
using HouseSource.ResponseData;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using HouseSource.Views;
using Xamarin.Forms.Internals;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HouseSource.ViewModels
{
    public class AllHouseListViewModel2 : BaseViewModel
    {
        #region 筛选器
        private string[] DistrictList { get; set; }   //区域列表

        private string[] RoomStyleList { get; set; }   //房型列表

        private string[] SalePriceList { get; set; }   //价格列表

        private string[] SquareList { get; set; }   //面积列表

        /*
        private List<string> usageList;   //用途列表
        public List<string> UsageList
        {
            get { return usageList; }
            set { SetProperty(ref usageList, value); }
        }

        private List<string> panTypeList;   //楼盘类型列表
        public List<string> PabTypeList
        {
            get { return panTypeList; }
            set { SetProperty(ref panTypeList, value); }
        }
        */
        #endregion

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

        private bool isLoading;   //是否正在加载
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        private bool isEnable;   //是否能加载下一页
        public bool IsEnable
        {
            get { return isEnable; }
            set { SetProperty(ref isEnable, value); }
        }

        private ObservableCollection<HouseInfo2> houseItemList;    //房列表
        public ObservableCollection<HouseInfo2> HouseItemList
        {
            get { return houseItemList; }
            set { SetProperty(ref houseItemList, value); }
        }

        private string pageNum;   //Comment
        public string PageNum
        {
            get { return pageNum; }
            set { SetProperty(ref pageNum, value); }
        }

        private string pageSize;   //Comment
        public string PageSize
        {
            get { return pageSize; }
            set { SetProperty(ref pageSize, value); }
        }

        private string totalPage;   //Comment
        public string TotalPage
        {
            get { return totalPage; }
            set { SetProperty(ref totalPage, value); }
        }

        private string total;   //Comment
        public string Total
        {
            get { return total; }
            set { SetProperty(ref total, value); }
        }

        private HousePara saleHousePara;   //请求参数
        public HousePara SaleHousePara
        {
            get { return saleHousePara; }
            set { SetProperty(ref saleHousePara, value); }
        }

        private string searchContent;   //Comment
        public string SearchContent
        {
            get { return searchContent; }
            set { SetProperty(ref searchContent, value); }
        }

        public Command SearchCommand { get; set; }
        public Command LoadMoreCommand { get; set; }
        public Command<string> SortCommand { get; set; }
        public Command<long> TappedCommand { get; set; }

        public AllHouseListViewModel2()
        {
            PageSize = "20";
            PageNum = "1";

            DistrictList = new[] { "全部区域", "青白江区", "郫都区", "金牛区", "成华区", "高新西区", "武侯区", "锦江区", "高新区", "天府新区", "温江区", "新都区", "青羊区", "双流区", "龙泉驿区" };
            RoomStyleList = new[] { "全部房型", "1房", "2房", "3房", "4房及以上" };
            SalePriceList = new[] { "全部价格", "0-30万", "30-50万", "50-100万", "100-150万", "150-200万", "200-300万", "300-500万", "500万以上" };
            SquareList = new[] { "全部面积", "0-20平", "20-50平", "50-100平", "100-150平", "150-200平", "200-250平", "250-500平", "500-800平", "800平以上" };

            //UsageList = new List<string> { "全部用途", "住宅", "商住", "商铺", "网店", "写字楼", "厂房", "写厂", "铺厂", "仓库", "地皮", "车位", "其他" };
            //PanTypeList = new List<string> { "全部类型", "公盘", "私盘", "特盘", "封盘" };

            District = DistrictList[0];
            RoomStyle = RoomStyleList[0];
            SalePrice = SalePriceList[0];
            Square = SquareList[0];

            HouseItemList = new ObservableCollection<HouseInfo2>();

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
                EmpID = ""
            };

            SortCommand = new Command<string>((t) =>
            {
                /*
                switch (t)
                {
                    //分类
                    case "0":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("分类", "取消", null, SortTypeList);
                            SortType = result == null || result == "取消" ? SortType : result;

                            if (SortType == "出售")
                            {
                                if (saleHouseItemList.Count == 0)
                                {
                                    GetHouseList();
                                }
                                else
                                {
                                    HouseItemList.Clear();
                                    saleHouseItemList.ForEach(item => { HouseItemList.Add(item); });
                                }
                            }
                            else if (SortType == "出租")
                            {
                                if (rentHouseItemList.Count == 0)
                                {
                                    GetHouseList();
                                }
                                else
                                {
                                    HouseItemList.Clear();
                                    rentHouseItemList.ForEach(item => { HouseItemList.Add(item); });
                                }
                            }
                        }
                        break;

                    //区域
                    case "1":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("区域", "取消", null, DistrictList);
                            District = result == null || result == "取消" ? District : result;
                            SaleHousePara.DistrictName = District == "全部区域" ? "区域" : District.TrimEnd('区');
                            GetHouseList();
                        }
                        break;

                    //房型
                    case "2":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("区域", "取消", null, RoomStyleList);
                            RoomStyle = result == null || result == "取消" ? RoomStyle : result;
                            SaleHousePara.CountF = RoomStyle == "全部房型" ? "房型" : RoomStyle;
                            GetHouseList();
                        }
                        break;

                    //价格
                    case "3":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("价格", "取消", null, SalePriceList);
                            SalePrice = result == null || result == "取消" ? SalePrice : result;
                            SaleHousePara.Price = SalePrice == "全部价格" ? "价格" : SalePrice;
                            GetHouseList();
                        }
                        break;

                    //面积
                    case "4":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("面积", "取消", null, SquareList);
                            Square = result == null || result == "取消" ? Square : result;
                            SaleHousePara.Square = Square == "全部面积" ? "面积" : Square;
                            GetHouseList();
                        }
                        break;

                    default:
                        break;
                }
                */
            }, (t) => { return true; });

            SearchCommand = new Command(() =>
            {
                //GetHouseList();
            }, () => { return true; });

            TappedCommand = new Command<long>((h) =>
            {
                HouseInfo2 temp = new HouseInfo2();
                foreach (var item in HouseItemList)
                {
                    if (item.proId == h)
                    {
                        temp = item;
                    }
                }

                HouseDetailPage2 houseDetailPage2 = new HouseDetailPage2(temp);
                Application.Current.MainPage.Navigation.PushAsync(houseDetailPage2);
            }, (h) => { return true; });

            LoadMoreCommand = new Command(() =>
            {
                int page = int.Parse(PageNum) + 1;
                GetHouseList("chengdu", page.ToString(), PageSize);
            }, () => { return true; });

            GetHouseList("chengdu", PageNum, PageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        private async void GetHouseList(string cityPinYin, string pageNum, string pageSize)
        {
            try
            {
                IsLoading = true;
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                //SaleHousePara.SearchContent = SearchContent;
                //HouseRD houseRD = await RestSharpService.GetHouseAsync(SaleHousePara, SortType);
                string content = await RestSharpService.GetHouseAsync(cityPinYin, pageNum, pageSize);

                if (string.IsNullOrWhiteSpace(content))
                {
                    CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
                    return;
                }

                JObject jObject = JObject.Parse(content);

                if (jObject["code"].ToString() == "200")
                {
                    PageNum = jObject["data"]["pageNum"].ToString();
                    PageSize = jObject["data"]["pageSize"].ToString();
                    TotalPage = jObject["data"]["totalPage"].ToString();
                    Total = jObject["data"]["total"].ToString();

                    IsEnable = !(PageNum == TotalPage);

                    List<HouseInfo2> houseInfoList = JsonConvert.DeserializeObject<List<HouseInfo2>>(jObject["data"]["list"].ToString());

                    foreach (var h in houseInfoList)
                    {
                        HouseItemList.Add(h);
                    }
                }
                IsLoading = false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
