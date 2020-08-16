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
    public partial class FluentCheckBox : ContentView
    {
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(FluentCheckBox), false, BindingMode.TwoWay);

        //public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FluentCheckBox), false);
        //private static readonly BindableProperty InnerBackgroundColorProperty = BindableProperty.Create(nameof(InnerBackgroundColor), typeof(Color), typeof(FluentCheckBox), Color.White);
        public static readonly BindableProperty CheckedBackgroundColorProperty = BindableProperty.Create(nameof(CheckedBackgroundColor), typeof(Color), typeof(FluentCheckBox), Color.LightPink);
        public static readonly BindableProperty UncheckedBackgroundColorProperty = BindableProperty.Create(nameof(UncheckedBackgroundColor), typeof(Color), typeof(FluentCheckBox), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FluentCheckBox), Color.LightGray);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FluentCheckBox), 5);
        //public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(int), typeof(FluentCheckBox), 5);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(FluentCheckBox), 12);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FluentCheckBox), string.Empty);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public Color CheckedBackgroundColor
        {
            get => (Color)GetValue(CheckedBackgroundColorProperty);
            set => SetValue(CheckedBackgroundColorProperty, value);
        }

        public Color UncheckedBackgroundColor
        {
            get => (Color)GetValue(UncheckedBackgroundColorProperty);
            set => SetValue(UncheckedBackgroundColorProperty, value);
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

        public int FontSize
        {
            get => (int)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public FluentCheckBox()
        {
            InitializeComponent();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

    }
}