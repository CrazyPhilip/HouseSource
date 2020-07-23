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

namespace HouseSource.ViewModels
{
    public class CollectionViewModel : BaseViewModel
    {
        private ObservableCollection<HouseItemInfo> houseItemList;    //收藏列表
        public ObservableCollection<HouseItemInfo> HouseItemList
        {
            get { return houseItemList; }
            set { SetProperty(ref houseItemList, value); }
        }

        public List<HouseInfo> houseList { get; set; }    //收藏列表   原始

        public Command RefreshCommand { get; set; }
        public Command<string> TappedCommand { get; set; }

        public CollectionViewModel()
        {
            RefreshCommand = new Command(() =>
            {
            }, () => { return true; });

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

            InitCollectionPage();
        }

        private async void InitCollectionPage()
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
