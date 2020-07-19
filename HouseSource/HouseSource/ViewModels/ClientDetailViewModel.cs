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

        public Command<string> NavigateCommand { get; set; }

        public ClientDetailViewModel(ClientItemInfo clientItemInfo)
        {
            Client = clientItemInfo;

            NavigateCommand = new Command<string>((p) =>
            {
                Type type = Type.GetType(p);
                Page page = (Page)Activator.CreateInstance(type);
                Application.Current.MainPage.Navigation.PushAsync(page);
            }, (p) => { return true; });
        }
    }
}
