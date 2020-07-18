using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class HouseDetailViewModel : BaseViewModel
    {
        private HouseInfo house;   //Comment
        public HouseInfo House
        {
            get { return house; }
            set { SetProperty(ref house, value); }
        }

        private string houseTitle;   //Comment
        public string HouseTitle
        {
            get { return houseTitle; }
            set { SetProperty(ref houseTitle, value); }
        }

        public Command ShareCommand { get; set; }
        public Command CollectCommand { get; set; }
        public Command CallCommand { get; set; }

        public HouseDetailViewModel()
        {
            
        }
    }
}
