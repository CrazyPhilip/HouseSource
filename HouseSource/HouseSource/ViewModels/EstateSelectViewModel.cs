using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseSource.Models;
using Xamarin.Forms;
using HouseSource.Views;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using HouseSource.ResponseData;
using HouseSource.Services;
using Newtonsoft.Json;

namespace HouseSource.ViewModels
{
    public class EstateSelectViewModel : BaseViewModel
    {
		private ObservableCollection<EstateItemInfo> estateList;   //Comment
		public ObservableCollection<EstateItemInfo> EstateList
		{
			get { return estateList; }
			set { SetProperty(ref estateList, value); }
		}

		private string searchContent;   //Comment
		public string SearchContent
		{
			get { return searchContent; }
			set { SetProperty(ref searchContent, value); }
		}

		private bool isRefreshing;   //Comment
		public bool IsRefreshing
		{
			get { return isRefreshing; }
			set { SetProperty(ref isRefreshing, value); }
		}

		public Command<EstateItemInfo> SelectCommand { get; set; }
		public Command SearchCommand { get; set; }

		public EstateSelectViewModel()
		{
			EstateList = new ObservableCollection<EstateItemInfo>();

			SelectCommand = new Command<EstateItemInfo>(async (e) =>
			{
				await BackPage(e);
			}, (e) => { return true; });

			SearchCommand = new Command(() =>
			{
				Search();
			}, () => { return true; });

		}

		/// <summary>
		/// 搜索房源
		/// </summary>
		private async void Search()
		{
			try
			{
				if (!Tools.IsNetConnective())
				{
					CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
					return;
				}

				if (string.IsNullOrWhiteSpace(searchContent))
				{
					CrossToastPopUp.Current.ShowToastError("请输入楼盘名", ToastLength.Short);
					return;
				}

				string content = await RestSharpService.GetEstateInfoByEstateName(searchContent);

                if (string.IsNullOrWhiteSpace(content))
                {
					CrossToastPopUp.Current.ShowToastError("服务器错误", ToastLength.Short);
					return;
                }

				BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

                if (baseResponse.Flag == "success")
                {
					List<EstateItemInfo> list = JsonConvert.DeserializeObject<List<EstateItemInfo>>(baseResponse.Result.ToString());
					EstateList = new ObservableCollection<EstateItemInfo>(list);
                }
				else
				{
					CrossToastPopUp.Current.ShowToastError("没有查找到匹配的小区", ToastLength.Short);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// 返回参数到上一级页面
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public async Task BackPage(EstateItemInfo e)
		{
			var page = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(p => p is AddHousePage2);

            if (page.BindingContext is AddHouseViewModel vm)
            {
                vm.Estate = e;
                vm.HouseTitle = e.DistrictName + " " + e.EstateName + " ";
                vm.GetBuildings();
            }

            await Application.Current.MainPage.Navigation.PopAsync();
		}
	}
}
