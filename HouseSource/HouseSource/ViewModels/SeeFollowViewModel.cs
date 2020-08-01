using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using HouseSource.Models;
using HouseSource.ResponseData;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace HouseSource.ViewModels
{
    public class SeeFollowViewModel : BaseViewModel
    {
		private ObservableCollection<InquiryFollowItemInfo> followInfoList;   //Comment
		public ObservableCollection<InquiryFollowItemInfo> FollowInfoList
		{
			get { return followInfoList; }
			set { SetProperty(ref followInfoList, value); }
		}

		public SeeFollowViewModel(string id, bool isProperty)
		{
			InitSeeFollowPage(id, isProperty);

		}

		/// <summary>
		/// 初始化看跟进页面
		/// </summary>
		private async void InitSeeFollowPage(string id, bool isProperty)
		{
			try
			{
				if (!Tools.IsNetConnective())
				{
					CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
					return;
				}

				InquiryFollowRD inquiryFollowRD = new InquiryFollowRD();
				inquiryFollowRD = isProperty ? await RestSharpService.GetHouseFollowInfo(id) : await RestSharpService.GetInquiryFollowInfo(id);

                if (isProperty)
				{
					FollowInfoList = inquiryFollowRD.Msg == "success" ? new ObservableCollection<InquiryFollowItemInfo>(inquiryFollowRD.FollowInfo) : new ObservableCollection<InquiryFollowItemInfo>();
				}
				else
				{
					FollowInfoList = inquiryFollowRD.Msg == "success" ? new ObservableCollection<InquiryFollowItemInfo>(inquiryFollowRD.InquiryFollowInfo) : new ObservableCollection<InquiryFollowItemInfo>();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
