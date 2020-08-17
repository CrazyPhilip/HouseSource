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
    public partial class FluentEditor : ContentView
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(FluentEntry), string.Empty);
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FluentEntry), false);
        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(FluentEntry), false);
        public static readonly BindableProperty InnerBackgroundColorProperty = BindableProperty.Create(nameof(InnerBackgroundColor), typeof(Color), typeof(FluentEntry), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FluentEntry), Color.White);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FluentEntry), 5);
        public static readonly BindableProperty OptionNameProperty = BindableProperty.Create(nameof(OptionName), typeof(string), typeof(FluentEntry), string.Empty);
        public static readonly BindableProperty OptionColorProperty = BindableProperty.Create(nameof(OptionColor), typeof(Color), typeof(FluentEntry), Color.Black);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(FluentEntry), 12);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(FluentEntry), "请输入");
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FluentEntry), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FluentEntry), Color.Black);

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }
        
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
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
        
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public string OptionName
        {
            get => (string)GetValue(OptionNameProperty);
            set => SetValue(OptionNameProperty, value);
        }

        public Color OptionColor
        {
            get => (Color)GetValue(OptionColorProperty);
            set => SetValue(OptionColorProperty, value);
        }

        public int FontSize
        {
            get => (int)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public FluentEditor()
        {
            InitializeComponent();
        }
    }
}