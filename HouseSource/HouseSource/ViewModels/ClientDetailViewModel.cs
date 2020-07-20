using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class ClientDetailViewModel : BaseViewModel
    {
        private ClientItemInfo client;   //Comment
        public ClientItemInfo Client
        {
            get { return client; }
            set { SetProperty(ref client, value); }
        }

        public Command<Type> NavigateCommand { get; set; }

        public ClientDetailViewModel(ClientItemInfo clientItemInfo)
        {
            Client = clientItemInfo;

            NavigateCommand = new Command<Type>(async (pageName) =>
            {
                //Type type = Type.GetType(pageName);
                Page page = (Page)Activator.CreateInstance(pageName, Client.InquiryID);
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }, (pageName) => { return true; });
        }
    }
}
