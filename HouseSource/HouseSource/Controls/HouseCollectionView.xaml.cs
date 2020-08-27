using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseCollectionView : ContentView
    {
        public static readonly BindableProperty HouseItemListProperty = BindableProperty.Create(nameof(HouseItemList), typeof(IList), typeof(HouseCollectionView), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(HouseCollectionView), null);

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public IList HouseItemList
        {
            get => (IList)GetValue(HouseItemListProperty);
            set => SetValue(HouseItemListProperty, value);
        }

        public HouseCollectionView()
        {
            InitializeComponent();

        }
    }
}