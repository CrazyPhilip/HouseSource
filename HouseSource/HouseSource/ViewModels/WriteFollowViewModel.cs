using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class WriteFollowViewModel : BaseViewModel
    {
        private List<string> followTypeList;   //跟进类型列表
        public List<string> FollowTypeList
        {
            get { return followTypeList; }
            set { SetProperty(ref followTypeList, value); }
        }

        private List<string> alertList;   //提醒范围列表
        public List<string> AlertList
        {
            get { return alertList; }
            set { SetProperty(ref alertList, value); }
        }

        private string followType;   //跟进类型
        public string FollowType
        {
            get { return followType; }
            set { SetProperty(ref followType, value); }
        }

        private string alert;   //提醒范围
        public string Alert
        {
            get { return alert; }
            set { SetProperty(ref alert, value); }
        }

        private string content;   //跟进内容
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        private bool visible;   //Comment
        public bool Visible
        {
            get { return visible; }
            set { SetProperty(ref visible, value); }
        }

        public string InquiryID { get; set; }
        public string PropertyID { get; set; }

        public Command CommitCommand { get; set; }

        public WriteFollowViewModel(string id, bool isProperty)
        {
            if (isProperty)
            {
                PropertyID = id;
                Visible = isProperty;
                AlertList = new List<string> { "本部", "本人" };
            }
            else
            {
                InquiryID = id;
                Visible = isProperty;
            }

            FollowTypeList = new List<string>()
            {
                "去电","来电","带看","实勘","空看","拜访","派单","短信","拿钥匙","责任盘维护","申请","修改",
                "保留","其他","回访","看房","发网络","议价","客还价","短信","诚意金","预定","有效跟进","激活"
            };

            FollowType = FollowTypeList[0];

            CommitCommand = new Command(() =>
            {
                Commit();
            }, () => { return true; });

        }

        /// <summary>
        /// 新增跟进内容
        /// </summary>
        private async void Commit()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Content))
                {
                    CrossToastPopUp.Current.ShowToastError("跟进内容为空", ToastLength.Short);
                    return;
                }
                string _Content = "";

                _Content = Visible ? await RestSharpService.NewHouseFollow(PropertyID, Content, Alert, FollowType) : await RestSharpService.NewInquiryFollow(InquiryID, Content, FollowType);

                if (string.IsNullOrWhiteSpace(_Content))
                {
                    CrossToastPopUp.Current.ShowToastError("未知错误，提交跟进失败", ToastLength.Short);
                    return;
                }
                else if (_Content.Contains("success"))
                {
                    CrossToastPopUp.Current.ShowToastSuccess("新增跟进成功！", ToastLength.Short);
                    return;
                }
                else if (_Content.Contains("failed"))
                {
                    CrossToastPopUp.Current.ShowToastWarning("提交跟进失败，可能是网络或数据库波动", ToastLength.Short);
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
