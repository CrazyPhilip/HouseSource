using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HouseSource.Models;
using HouseSource.Utils;

namespace HouseSource.ViewModels
{
    public class ShareViewModel : BaseViewModel
    {
        public Command<string> ShareCommand { get; set; }

        public ShareViewModel(string propertyID)
        {
            ShareCommand = new Command<string>((p) =>
            {
                switch (p)
                {
                    //分享给好友
                    case "1":
                        {
                            string para = "http://47.108.202.57:8081/WebService.asmx/PropertyDetail?DBName=" + GlobalVariables.LoggedUser.DBName +"&PropertyID=" + propertyID;
                            //MessagingCenter.Send(new object(), "Register");//首先进行注册，然后订阅注册的结果。
                            //MessagingCenter.Send(new object(), "ShareToFriend", para);
                        }
                        break;

                    //分享到朋友圈
                    case "2":
                        {
                            //string para = "http://ab3688.com/#/goodsDetails?productId=" + product.productId;
                            //MessagingCenter.Send(new object(), "Register");//首先进行注册，然后订阅注册的结果。
                            //MessagingCenter.Send(new object(), "ShareToTimeline", para);
                        }
                        break;

                    default:
                        break;
                }
            }, (p) => { return true; });

        }
    }
}
