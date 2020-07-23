using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class SaleHouseViewModel : BaseViewModel
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

        public Command SearchCommand { get; set; }
        public Command<string> SortCommand { get; set; }
        public Command<string> TappedCommand { get; set; }

        public SaleHouseViewModel()
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

            SortCommand = new Command<string>(async (t) =>
            {
                switch (t)
                {
                    //区域
                    case "0":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("区域", "取消", null, DistrictList);
                            District = result == null || result == "取消" ? District : result;

                        }
                        break;

                    //房型
                    case "1":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("房型", "取消", null, RoomStyleList);
                            RoomStyle = result == null || result == "取消" ? RoomStyle : result;
                            //SaleHousePara.DistrictName = District == "全部区域" ? "区域" : District.TrimEnd('区');
                            //GetHouseList();
                        }
                        break;

                    //价格
                    case "2":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("价格", "取消", null, SalePriceList);
                            SalePrice = result == null || result == "取消" ? SalePrice : result;
                            //SaleHousePara.CountF = RoomStyle == "全部房型" ? "房型" : RoomStyle;
                            //GetHouseList();
                        }
                        break;

                    //面积
                    case "3":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("面积", "取消", null, SquareList);
                            Square = result == null || result == "取消" ? Square : result;
                            //SaleHousePara.Price = SalePrice == "全部价格" ? "价格" : SalePrice;
                            //GetHouseList();
                        }
                        break;

                    //状态
                    case "4":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("状态", "取消", null, StatusList);
                            Status = result == null || result == "取消" ? Status : result;
                            //SaleHousePara.Square = Square == "全部面积" ? "面积" : Square;
                            //GetHouseList();
                        }
                        break;

                    default:
                        break;
                }
            }, (t) => { return true; });

            SearchCommand = new Command(() =>
            {
                //GetHouseList();
            }, () => { return true; });

            TappedCommand = new Command<string>((h) =>
            {

            }, (h) => { return true; });
        }
    }
}
