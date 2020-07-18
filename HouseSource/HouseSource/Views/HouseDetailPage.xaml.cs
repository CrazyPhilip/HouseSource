using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HouseSource.Models;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseDetailPage : ContentPage
    {
        public HouseDetailPage(HouseInfo house)
        {
            InitializeComponent();

            houseDetailViewModel.House = house;
            houseDetailViewModel.HouseTitle = house.DistrictName + " " + house.AreaName + " " + house.EstateName;
        }
    }
}