using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{ 
    public class WebViewModel : BaseViewModel
    {
        private string uRL;   //网址
        public string URL
        {
            get { return uRL; }
            set { SetProperty(ref uRL, value); }
        }

        private bool isLoading;   //Comment
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        public Command BackCommand { get; set; }
        public Command ForwardCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public Command NavigatingCommand { get; set; }
        public Command NavigatedCommand { get; set; }

        public WebViewModel(string url, string title)
        {
            URL = url;

            BackCommand = new Command(() =>
            {

            }, () => { return true; });

            ForwardCommand = new Command(() =>
            {

            }, () => { return true; });

            RefreshCommand = new Command(() =>
            {

            }, () => { return true; });

            NavigatingCommand = new Command(() =>
            {
                IsLoading = true;
            }, () => { return true; });

            NavigatedCommand = new Command(() =>
            {
                IsLoading = false;
            }, () => { return true; });


        }
    }
}
