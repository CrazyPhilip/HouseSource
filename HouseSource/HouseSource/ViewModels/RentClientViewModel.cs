﻿using HouseSource.Models;
using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Services;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using HouseSource.ResponseData;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Xamarin.Forms;
using HouseSource.Views;

namespace HouseSource.ViewModels
{
    public class RentClientViewModel : BaseViewModel
    {
        private string[] FloorList { get; set; }   //楼层列表

        private string[] RoomStyleList { get; set; }   //房型列表

        private string[] SalePriceList { get; set; }   //价格列表

        private string[] SquareList { get; set; }   //面积列表

        private ObservableCollection<ClientItemInfo> rentClientList;   //求租客源列表
        public ObservableCollection<ClientItemInfo> RentClientList
        {
            get { return rentClientList; }
            set { SetProperty(ref rentClientList, value); }
        }

		private string floor;   //楼层
		public string Floor
		{
			get { return floor; }
			set { SetProperty(ref floor, value); }
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

		ClientPara clientPara = new ClientPara
		{
			RoomStyle = "房型",
			IsPublic = "类型",
			Floor = "楼层",
			Square = "面积",
			Price = "价格",
			CusName = "",
			Phone = "",
			Contact = "",
			SearchContent = "",
			Page = "",
			EmpID = ""
		};

		public Command SortCommand { get; set; }
		public Command<string> TappedCommand { get; set; }

		public RentClientViewModel()
		{
			FloorList = new[] { "楼层（全部）", "0-10楼", "10-20楼", "20-30楼", "30楼以上" };
			RoomStyleList = new[] { "全部房型", "1房", "2房", "3房", "4房及以上" };
			SalePriceList = new[] { "全部价格", "0-30万", "30-50万", "50-100万", "100-150万", "150-200万", "200-300万", "300-500万", "500万以上" };
			SquareList = new[] { "全部面积", "0-20平", "20-50平", "50-100平", "100-150平", "150-200平", "200-250平", "250-500平", "500-800平", "800平以上" };

			Floor = FloorList[0];
			RoomStyle = RoomStyleList[0];
			SalePrice = SalePriceList[0];
			Square = SquareList[0];

            SortCommand = new Command<string>(async (t) =>
            {
                switch (t)
                {
					//房型
					case "0":
                        {
							string result = await Application.Current.MainPage.DisplayActionSheet("房型", "取消", null, RoomStyleList);
							RoomStyle = result == null || result == "取消" ? RoomStyle : result;
						}
                        break;

                    //楼层
                    case "1":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("楼层", "取消", null, FloorList);
                            Floor = result == null || result == "取消" ? Floor : result;
                        }
                        break;

                    //面积
                    case "2":
                        {
                            string result = await Application.Current.MainPage.DisplayActionSheet("面积", "取消", null, RoomStyleList);
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

                    default:
                        break;
                }
            }, (t) => { return true; });

			TappedCommand = new Command<string>((n) =>
			{
				foreach (var item in RentClientList)
				{
					if (item.InquiryNo == n)
					{
						ClientDetailPage clientDetailPage = new ClientDetailPage(item);
						Application.Current.MainPage.Navigation.PushAsync(clientDetailPage);
					}
				}
			}, (n) => { return true; });

			GetClientList();
		}

		/// <summary>
		/// 获取客源
		/// </summary>
		private async void GetClientList()
		{
			try
			{
				if (!Tools.IsNetConnective())
				{
					CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
					return;
				}

				ClientRD rentClientRD = await RestSharpService.GetClientAsync(clientPara, "Rent");

				RentClientList = rentClientRD != null ? new ObservableCollection<ClientItemInfo>(rentClientRD.Customers) : new ObservableCollection<ClientItemInfo>();

			}
			catch (Exception)
			{

				throw;
			}


		}
	}
}
