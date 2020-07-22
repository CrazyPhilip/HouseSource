using HouseSource.Models;
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
    public class SaleClientViewModel : BaseViewModel
    {
        private string[] FloorList { get; set; }   //楼层列表
        private string[] RoomStyleList { get; set; }   //房型列表
        private string[] SalePriceList { get; set; }   //价格列表
        private string[] SquareList { get; set; }   //面积列表

        private ObservableCollection<ClientItemInfo> saleClientList;   //求购客源列表
        public ObservableCollection<ClientItemInfo> SaleClientList
        {
            get { return saleClientList; }
            set { SetProperty(ref saleClientList, value); }
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

		private string searchContent;   //Comment
		public string SearchContent
		{
			get { return searchContent; }
			set { SetProperty(ref searchContent, value); }
		}

		public ClientPara clientPara { get; set; }

		public Command SearchCommand { get; set; }
		public Command SortCommand { get; set; }
		public Command<string> TappedCommand { get; set; }

		public SaleClientViewModel()
		{
			FloorList = new[] { "全部楼层", "0-10楼", "10-20楼", "20-30楼", "30楼以上" };
			RoomStyleList = new[] { "全部房型", "1房", "2房", "3房", "4房及以上" };
			SalePriceList = new[] { "全部价格", "0-30万", "30-50万", "50-100万", "100-150万", "150-200万", "200-300万", "300-500万", "500万以上" };
			SquareList = new[] { "全部面积", "0-20平", "20-50平", "50-100平", "100-150平", "150-200平", "200-250平", "250-500平", "500-800平", "800平以上" };

			Floor = FloorList[0];
			RoomStyle = RoomStyleList[0];
			SalePrice = SalePriceList[0];
			Square = SquareList[0];

			SaleClientList = new ObservableCollection<ClientItemInfo>();

			clientPara = new ClientPara
			{
				RoomStyle = "房型",
				IsPublic = "类型",
				Floor = "楼层",
				Square = "面积",
				Price = "价格",
				CusName = "",
				Phone = "",
				Contact = "",
				SearchContent = SearchContent,
				Page = "",
				EmpID = ""
			};

			SortCommand = new Command<string>(async (t) =>
			{
				switch (t)
				{
					//房型
					case "0":
						{
							string result = await Application.Current.MainPage.DisplayActionSheet("房型", "取消", null, RoomStyleList);
							RoomStyle = result == null || result == "取消" ? RoomStyle : result;
							clientPara.RoomStyle = RoomStyle == "全部房型" ? "房型" : RoomStyle;
							GetClientList();
						}
						break;

					//楼层
					case "1":
						{
							string result = await Application.Current.MainPage.DisplayActionSheet("楼层", "取消", null, FloorList);
							Floor = result == null || result == "取消" ? Floor : result;
							clientPara.Floor = Floor == "全部楼层" ? "楼层" : Floor;
							GetClientList();
						}
						break;

					//面积
					case "2":
						{
							string result = await Application.Current.MainPage.DisplayActionSheet("面积", "取消", null, SquareList);
							Square = result == null || result == "取消" ? Square : result;
							clientPara.Square = Square == "全部面积" ? "面积" : Square;
							GetClientList();
						}
						break;

					//价格
					case "3":
						{
							string result = await Application.Current.MainPage.DisplayActionSheet("价格", "取消", null, SalePriceList);
							SalePrice = result == null || result == "取消" ? SalePrice : result;
							clientPara.Price = SalePrice == "全部价格" ? "价格" : SalePrice;
							GetClientList();
						}
						break;

					default:
						GetClientList();
						break;
				}
			}, (t) => { return true; });

			TappedCommand = new Command<string>((n) =>
			{
				foreach (var item in SaleClientList)
				{
					if (item.InquiryNo == n)
					{
						ClientDetailPage clientDetailPage = new ClientDetailPage(item);
						Application.Current.MainPage.Navigation.PushAsync(clientDetailPage);
					}
				}
			}, (n) => { return true; });

			SearchCommand = new Command(() =>
			{
				GetClientList();
			}, () => { return true; });

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

				clientPara.SearchContent = SearchContent;
				ClientRD saleClientRD = await RestSharpService.GetClientAsync(clientPara, "Sale");

				SaleClientList.Clear();
				if (saleClientRD != null)
				{
					foreach (var item in saleClientRD.Customers)
					{
						SaleClientList.Add(item);
					}
				}
			}
			catch (Exception)
			{
				throw;
			}


		}
	}
}
