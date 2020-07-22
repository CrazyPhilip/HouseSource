using HouseSource.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllHouseListPage : ContentPage
    {
        public AllHouseListPage()
        {
            InitializeComponent();

            BindingContext = new AllHouseListViewModel("");
        }

        public AllHouseListPage(string searchContent = "")
        {
            InitializeComponent();

            BindingContext = new AllHouseListViewModel(searchContent);
        }
    }
}