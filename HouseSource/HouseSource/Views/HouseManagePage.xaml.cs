﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseManagePage : TabbedPage
    {
        public HouseManagePage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            AddHousePage addHousePage = new AddHousePage();
            Application.Current.MainPage.Navigation.PushAsync(addHousePage);
        }
    }
}