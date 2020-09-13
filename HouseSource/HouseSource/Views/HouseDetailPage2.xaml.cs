using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HouseSource.Models;
using HouseSource.ViewModels;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseDetailPage2 : ContentPage
    {
        public HouseDetailPage2(HouseInfo2 house)
        {
            InitializeComponent();

            BindingContext = new HouseDetailViewModel2(house);
        }
    }
}