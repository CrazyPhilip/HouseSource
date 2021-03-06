﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HouseSource.Models;
using HouseSource.ViewModels;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseDetailPage : ContentPage
    {
        public HouseDetailPage(HouseInfo house)
        {
            InitializeComponent();

            BindingContext = new HouseDetailViewModel(house);
        }
    }
}