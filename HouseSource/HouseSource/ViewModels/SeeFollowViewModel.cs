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
		private ObservableCollection<InquiryFollowItemInfo> inquiryFollowInfoList;   //Comment
		public ObservableCollection<InquiryFollowItemInfo> InquiryFollowInfoList
		{
			get { return inquiryFollowInfoList; }
			set { SetProperty(ref inquiryFollowInfoList, value); }
		}

		public SeeFollowViewModel(string inquiryID)
		{
			InitSeeFollowPage(inquiryID);

		}

		/// <summary>
		/// 初始化看跟进页面
		/// </summary>
		private async void InitSeeFollowPage(string inquiryID)
		{
			try
			{
				if (!Tools.IsNetConnective())
				{
					CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
					return;
				}

				InquiryFollowRD inquiryFollowRD = await RestSharpService.GetInquiryFollowInfo(inquiryID);

				InquiryFollowInfoList = inquiryFollowRD != null ? new ObservableCollection<InquiryFollowItemInfo>(inquiryFollowRD.InquiryFollowInfo) : new ObservableCollection<InquiryFollowItemInfo>();

			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
