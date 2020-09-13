using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Services;
using HouseSource.Models;
using HouseSource.ResponseData;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using UltimateXF.Widget.Charts.Models;
using UltimateXF.Widget.Charts.Models.LineChart;
using UltimateXF.Widget.Charts.Models.Formatters;
using UltimateXF.Widget.Charts.Models.Component;
using HouseSource.Views;
using Newtonsoft.Json;
using System.Linq;

namespace HouseSource.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<string> carouselList;   //comment
        public List<string> CarouselList
        {
            get { return carouselList; }
            set { SetProperty(ref carouselList, value); }
        }

        private List<Option> optionList;   //Comment
        public List<Option> OptionList
        {
            get { return optionList; }
            set { SetProperty(ref optionList, value); }
        }

        private string searchContent;   //Comment
        public string SearchContent
        {
            get { return searchContent; }
            set { SetProperty(ref searchContent, value); }
        }

        #region MyRegion
        private ObservableCollection<HouseItemInfo> houseItemList;    //收藏列表
        public ObservableCollection<HouseItemInfo> HouseItemList
        {
            get { return houseItemList; }
            set { SetProperty(ref houseItemList, value); }
        }

        public List<HouseInfo> houseList { get; set; }    //收藏列表   原始

        public Command<string> TappedCommand { get; set; }
        #endregion

        public Command SearchCommand { get; set; }
        public Command TempCommand { get; set; }
        public Command<string> NavigateCommand { get; set; }    //导航命令事件

        public HomeViewModel()
        {
            CarouselList = new List<string>
            {
                "pic1.jpg", "pic2.jpg", "pic3.jpg"
            };

            OptionList = new List<Option>
            {
                //new Option { icon = "estate.png", option = "全网房源", page = "HouseSource.Views.AllHouseListPage"},
                new Option { icon = "estate.png", option = "全网房源", page = "HouseSource.Views.AllHouseListPage2"},
                new Option { icon = "house_manage.png", option = "房源管理", page = "HouseSource.Views.HouseManagePage"},
                new Option { icon = "customer_manage.png", option = "客源管理", page = "HouseSource.Views.ClientManagePage"},
                new Option { icon = "news2.png", option = "新闻公告", page = "HouseSource.Views.MessagePage"},
                new Option { icon = "calculator1.png", option = "房贷计算器", page = "HouseSource.Views.LoanPage" },
                new Option { icon = "calculator2.png", option = "税费计算器", page = "HouseSource.Views.TaxPage" },
                new Option { icon = "add_house.png", option = "新增房源", page = "HouseSource.Views.AddHousePage2"},
                new Option { icon = "add_customer.png", option = "新增客源", page = "HouseSource.Views.AddClientPage2"}

            };

            SearchCommand = new Command(() =>
            {
                AllHouseListPage allHouseListPage = new AllHouseListPage(SearchContent);
                Application.Current.MainPage.Navigation.PushAsync(allHouseListPage);
            }, () => { return true; });

            //导航命令事件
            NavigateCommand = new Command<string>((pageName) =>
            {
                switch (pageName)
                {
                    case "HouseSource.Views.HouseManagePage":
                        {
                            TabbedPage page = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(p => p is MainPage) as TabbedPage;
                            page.CurrentPage = page.Children.FirstOrDefault(p => p is HouseManagePage);
                        }
                        break;

                    case "HouseSource.Views.ClientManagePage":
                        {
                            TabbedPage page = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(p => p is MainPage) as TabbedPage;
                            page.CurrentPage = page.Children.FirstOrDefault(p => p is ClientManagePage);
                        }
                        break;

                    default:
                        {
                            Type type = Type.GetType(pageName);
                            Page page = (Page)Activator.CreateInstance(type);
                            Application.Current.MainPage.Navigation.PushAsync(page);
                        }
                        break;
                }

            }, (pageName) => { return true; });

            TappedCommand = new Command<string>((h) =>
            {
                //houseList.ForEach((item) =>
                //{
                //    if (item.PropertyID == h)
                //    {
                //        HouseDetailPage houseDetailPage = new HouseDetailPage(item);
                //        Application.Current.MainPage.Navigation.PushAsync(houseDetailPage);
                //    }
                //});
                //HouseInfo temp = new HouseInfo();
                //foreach (var item in houseList)
                //{
                //    if (item.PropertyID == h)
                //    {
                //        temp = item;
                //    }
                //}

                //HouseDetailPage houseDetailPage = new HouseDetailPage(temp);
                //Application.Current.MainPage.Navigation.PushAsync(houseDetailPage);

            }, (h) => { return true; });

            TempCommand = new Command(() =>
            {
                CrossToastPopUp.Current.ShowToastWarning("功能开发中", ToastLength.Short);
            }, () => { return true; });

            //InitHomePage();
        }

        /// <summary>
        /// 初始化首页
        /// </summary>
        private async void InitHomePage()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.GetCollection();

                BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

                if (baseResponse.Flag == "fail")
                {
                    CrossToastPopUp.Current.ShowToastError(baseResponse.Msg, ToastLength.Short);
                    return;
                }

                houseList = JsonConvert.DeserializeObject<List<HouseInfo>>(baseResponse.Result.ToString());

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

                HouseItemList = new ObservableCollection<HouseItemInfo>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
