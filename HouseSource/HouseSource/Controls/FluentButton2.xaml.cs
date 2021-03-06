﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FluentButton2 : ContentView
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(FluentButton2), string.Empty);
        public static readonly BindableProperty OptionNameProperty = BindableProperty.Create(nameof(OptionName), typeof(string), typeof(FluentButton2), string.Empty);
        public static readonly BindableProperty OptionColorProperty = BindableProperty.Create(nameof(OptionColor), typeof(Color), typeof(FluentButton2), Color.Black);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(FluentButton2), 12);

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(FluentButton2), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty DescriptionColorProperty = BindableProperty.Create(nameof(DescriptionColor), typeof(Color), typeof(FluentButton2), Color.LightGray);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(FluentButton2), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FluentButton2), null);

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public Color DescriptionColor
        {
            get => (Color)GetValue(DescriptionColorProperty);
            set => SetValue(DescriptionColorProperty, value);
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

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public FluentButton2()
        {
            InitializeComponent();
        }
    }
}