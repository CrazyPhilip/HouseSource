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
using Microcharts;
using SkiaSharp;
using UltimateXF.Widget.Charts.Models;
using UltimateXF.Widget.Charts.Models.LineChart;
using UltimateXF.Widget.Charts.Models.Formatters;
using UltimateXF.Widget.Charts.Models.Component;
using HouseSource.Views;

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

        public Command<string> NavigateCommand { get; set; }    //导航命令事件

        public HomeViewModel()
        {
            CarouselList = new List<string>
            {
                "pic1.jpg", "pic2.jpg", "pic3.jpg"
            };

            OptionList = new List<Option>
            {
                new Option { icon = "building1.png", option = "全网房源", page = "HouseSource.Views.AllHouseListPage"},
                new Option { icon = "building2.png", option = "房源管理", page = "HouseSource.Views.HouseManagePage"},
                new Option { icon = "people.png", option = "客源管理", page = "HouseSource.Views.ClientManagePage"},
                new Option { icon = "news.png", option = "新闻公告", page = "HouseSource.Views.MessagePage"},
                new Option { icon = "calculator.png", option = "房贷计算器", page = "HouseSource.Views.LoanPage" },
                new Option { icon = "calculator.png", option = "税费计算器", page = "HouseSource.Views.TaxPage" },
                new Option { icon = "add.png", option = "新增房源", page = "HouseSource.Views.AddHousePage"},
                new Option { icon = "add_client.png", option = "新增客源", page = "HouseSource.Views.AddClientPage"}

            };

            //导航命令事件
            NavigateCommand = new Command<string>(async (pageName) =>
            {
                Type type = Type.GetType(pageName);
                Page page = (Page)Activator.CreateInstance(type);
                await Application.Current.MainPage.Navigation.PushAsync(page);

            }, (pageName) => { return true; });

            TappedCommand = new Command<string>((h) =>
            {
                houseList.ForEach((item) =>
                {
                    if (item.PropertyID == h)
                    {
                        HouseDetailPage houseDetailPage = new HouseDetailPage(item);
                        Application.Current.MainPage.Navigation.PushAsync(houseDetailPage);
                    }
                });
            }, (h) => { return true; });

            InitHomePage();
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

                HouseRD houseRD = await RestSharpService.GetCollection();

                List<HouseItemInfo> list = new List<HouseItemInfo>();
                if (houseRD.Buildings != null)
                {
                    foreach (var h in houseRD.Buildings)
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
                            PhotoUrl = (h.PhotoUrl == "" ? "NullPic.jpg" : h.PhotoUrl)
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
                    houseList = new List<HouseInfo>(houseRD.Buildings);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
