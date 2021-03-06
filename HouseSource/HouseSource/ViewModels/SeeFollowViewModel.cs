﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using HouseSource.Models;
using HouseSource.ResponseData;
using HouseSource.Services;
using HouseSource.Utils;
using Newtonsoft.Json;
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
				string content = isProperty ? await RestSharpService.GetHouseFollowInfo(id) : await RestSharpService.GetInquiryFollowInfo(id);

                if (!string.IsNullOrWhiteSpace(content))
                {
					BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

                    if (baseResponse.Flag == "success")
                    {
						List<InquiryFollowItemInfo> list = JsonConvert.DeserializeObject<List<InquiryFollowItemInfo>>(baseResponse.Result.ToString());

						FollowInfoList = new ObservableCollection<InquiryFollowItemInfo>(list);
					}
                    else
                    {
						FollowInfoList = new ObservableCollection<InquiryFollowItemInfo>();
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
