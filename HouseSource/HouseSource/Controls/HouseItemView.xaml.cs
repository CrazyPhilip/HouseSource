using HouseSource.Models;
using HouseSource.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseItemView : ContentView
    {

        public static readonly BindableProperty HouseProperty = BindableProperty.Create(nameof(House), typeof(HouseInfo), typeof(HouseItemView), null);
        public static readonly BindableProperty HouseItemProperty = BindableProperty.Create(nameof(HouseItem), typeof(HouseItemInfo), typeof(HouseItemView), null);

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(HouseItemView), false);
        public static readonly BindableProperty InnerBackgroundColorProperty = BindableProperty.Create(nameof(InnerBackgroundColor), typeof(Color), typeof(HouseItemView), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(HouseItemView), Color.White);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(HouseItemView), 5);

        public HouseInfo House
        {
            get => (HouseInfo)GetValue(HouseProperty);
            set => SetValue(HouseProperty, value);
        }

        public HouseItemInfo HouseItem
        {
            get => (HouseItemInfo)GetValue(HouseItemProperty);
            set => SetValue(HouseItemProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public Color InnerBackgroundColor
        {
            get => (Color)GetValue(InnerBackgroundColorProperty);
            set => SetValue(InnerBackgroundColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Command TappedCommand { get; set; }

        public HouseItemView()
        {
            InitializeComponent();

            TappedCommand = new Command(() =>
            {
                HouseDetailPage houseDetailPage = new HouseDetailPage(House);
                Application.Current.MainPage.Navigation.PushAsync(houseDetailPage);
            }, () => { return true; });
        }
    }
}