using HouseSource.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.ViewModels
{
    public class MessageDetailViewModel : BaseViewModel
    {
        private NewsInfo news;   //消息
        public NewsInfo News
        {
            get { return news; }
            set { SetProperty(ref news, value); }
        }

        public MessageDetailViewModel()
        {

        }
    }
}
