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
    public partial class ClientManagePage : TabbedPage
    {
        public ClientManagePage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            AddClientPage2 addClientPage = new AddClientPage2();
            Application.Current.MainPage.Navigation.PushAsync(addClientPage);
        }
    }
}