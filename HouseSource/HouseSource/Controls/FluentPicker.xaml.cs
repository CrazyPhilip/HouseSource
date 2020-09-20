using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace HouseSource.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FluentPicker : ContentView
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(FluentPicker), string.Empty);
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FluentPicker), false);
        public static readonly BindableProperty InnerBackgroundColorProperty = BindableProperty.Create(nameof(InnerBackgroundColor), typeof(Color), typeof(FluentPicker), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FluentPicker), Color.White);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FluentPicker), 5);
        public static readonly BindableProperty OptionNameProperty = BindableProperty.Create(nameof(OptionName), typeof(string), typeof(FluentPicker), string.Empty);
        public static readonly BindableProperty OptionColorProperty = BindableProperty.Create(nameof(OptionColor), typeof(Color), typeof(FluentPicker), Color.Black);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FluentPicker), Color.Black);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(FluentPicker), 12);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(FluentPicker), "请选择");
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(FluentPicker), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(FluentPicker), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(FluentPicker), -1, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged);
        //public static readonly BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(nameof(ItemDisplayBinding), typeof(Binding), typeof(FluentPicker), null, defaultBindingMode: BindingMode.TwoWay);
        
        public event EventHandler SelectedIndexChanged;

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (FluentPicker)bindable;
            picker.UpdateSelectedItem(picker.SelectedIndex);
            picker.SelectedIndexChanged?.Invoke(bindable, EventArgs.Empty);
        }

        private void UpdateSelectedItem(int index)
        {
            if (index == -1)
            {
                SelectedItem = null;
                return;
            }

            if (ItemsSource != null)
            {
                SelectedItem = ItemsSource[index].ToString();
                return;
            }

            SelectedItem = myPicker.Items[index];
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (FluentPicker)bindable;
            picker.UpdateSelectedIndex(newValue);
        }

        void UpdateSelectedIndex(object selectedItem)
        {
            if (ItemsSource != null)
            {
                SelectedIndex = ItemsSource.IndexOf(selectedItem);
                return;
            }
            SelectedIndex = myPicker.Items.IndexOf(selectedItem.ToString());
        }

        //public Binding ItemDisplayBinding
        //{
        //    get => (Binding)GetValue(ItemDisplayBindingProperty);
        //    set => SetValue(ItemDisplayBindingProperty, value);
        //}

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
        
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public int FontSize
        {
            get => (int)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public string SelectedItem
        {
            get => (string)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public FluentPicker()
        {
            InitializeComponent();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            myPicker.Focus();
        }

        //public event EventHandler<SelectedItemChangedEventArgs> SelectedChanged;
        //public void NotifySelectedChanged(object item)
        //{
        //    SelectedChanged?.Invoke(this, new SelectedItemChangedEventArgs(SelectedItem, SelectedIndex));
        //}
    }
}