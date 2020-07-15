using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;

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

        public HouseDetailViewModel()
        {

        }
    }
}
