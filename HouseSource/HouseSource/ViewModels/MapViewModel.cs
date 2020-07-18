using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HouseSource.Controls;

namespace HouseSource.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private string[] SortTypeList { get; set; }   //分类列表

        private string[] DistrictList { get; set; }   //区域列表

        private string[] RoomStyleList { get; set; }   //房型列表

        private string[] SalePriceList { get; set; }   //价格列表

        private string[] SquareList { get; set; }   //面积列表

        private string sortType;   //分类
        public string SortType
        {
            get { return sortType; }
            set { SetProperty(ref sortType, value); }
        }

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

        public Command<string> SortCommand { get; set; }

        public MapViewModel()
        {
            SortTypeList = new[] { "出售", "出租" };

            DistrictList = new[] { "全部区域", "青白江区", "郫都区", "金牛区", "成华区", "高新西区", "武侯区", "锦江区", "高新区", "天府新区", "温江区", "新都区", "青羊区", "双流区", "龙泉驿区" };

            RoomStyleList = new[] { "全部房型", "1房", "2房", "3房", "4房及以上" };

            SalePriceList = new[] { "全部价格", "0-30万", "30-50万", "50-100万", "100-150万", "150-200万", "200-300万", "300-500万", "500万以上" };

            SquareList = new[] { "全部面积", "0-20平", "20-50平", "50-100平", "100-150平", "150-200平", "200-250平", "250-500平", "500-800平", "800平以上" };

            //UsageList = new List<string> { "全部用途", "住宅", "商住", "商铺", "网店", "写字楼", "厂房", "写厂", "铺厂", "仓库", "地皮", "车位", "其他" };
            //PanTypeList = new List<string> { "全部类型", "公盘", "私盘", "特盘", "封盘" };

            SortType = SortTypeList[0];
            District = DistrictList[0];
            RoomStyle = RoomStyleList[0];
            SalePrice = SalePriceList[0];
            Square = SquareList[0];

            SortCommand = new Command<string>(async (t) =>
            {
                switch (t)
                {
                    //分类
                    case "0":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("分类", "取消", null, SortTypeList);
                            SortType = result == null || result == "取消" ? SortType : result;


                        }
                        break;

                    //区域
                    case "1":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("区域", "取消", null, DistrictList);
                            District = result == null || result == "取消" ? District : result;
                        }
                        break;

                    //房型
                    case "2":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("区域", "取消", null, RoomStyleList);
                            RoomStyle = result == null || result == "取消" ? RoomStyle : result;
                        }
                        break;

                    //价格
                    case "3":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("价格", "取消", null, SalePriceList);
                            SalePrice = result == null || result == "取消" ? SalePrice : result;
                        }
                        break;

                    //面积
                    case "4":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("面积", "取消", null, SquareList);
                            Square = result == null || result == "取消" ? Square : result;
                        }
                        break;

                    default:
                        break;
                }
            }, (t) => { return true; });
        }
    }
}
