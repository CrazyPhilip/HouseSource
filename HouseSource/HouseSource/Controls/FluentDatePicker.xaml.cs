using System;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FluentDatePicker : ContentView
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(FluentDatePicker), string.Empty);
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FluentDatePicker), false);
        public static readonly BindableProperty InnerBackgroundColorProperty = BindableProperty.Create(nameof(InnerBackgroundColor), typeof(Color), typeof(FluentDatePicker), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FluentDatePicker), Color.White);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FluentDatePicker), 5);
        public static readonly BindableProperty OptionNameProperty = BindableProperty.Create(nameof(OptionName), typeof(string), typeof(FluentDatePicker), string.Empty);
        public static readonly BindableProperty OptionColorProperty = BindableProperty.Create(nameof(OptionColor), typeof(Color), typeof(FluentDatePicker), Color.Black);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FluentDatePicker), Color.Black);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(FluentDatePicker), 12);
        
        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePicker), new DateTime(2020, 1, 1), BindingMode.TwoWay,
            coerceValue: CoerceDate, propertyChanged: DatePropertyChanged);

        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DatePicker), new DateTime(1970, 1, 1),
            validateValue: ValidateMinimumDate, coerceValue: CoerceMinimumDate);

        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DatePicker), new DateTime(2099, 12, 31),
            validateValue: ValidateMaximumDate, coerceValue: CoerceMaximumDate);

        public event EventHandler<DateChangedEventArgs> DateSelected;

        static object CoerceDate(BindableObject bindable, object value)
        {
            var picker = (FluentDatePicker)bindable;
            DateTime dateValue = ((DateTime)value).Date;

            if (dateValue > picker.MaximumDate)
                dateValue = picker.MaximumDate;

            if (dateValue < picker.MinimumDate)
                dateValue = picker.MinimumDate;

            return dateValue;
        }

        static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var datePicker = (FluentDatePicker)bindable;
            datePicker.DateSelected?.Invoke(datePicker, new DateChangedEventArgs((DateTime)oldValue, (DateTime)newValue));
        }

        static bool ValidateMaximumDate(BindableObject bindable, object value)
        {
            return ((DateTime)value).Date >= ((FluentDatePicker)bindable).MinimumDate.Date;
        }

        static bool ValidateMinimumDate(BindableObject bindable, object value)
        {
            return ((DateTime)value).Date <= ((FluentDatePicker)bindable).MaximumDate.Date;
        }

        static object CoerceMaximumDate(BindableObject bindable, object value)
        {
            DateTime dateValue = ((DateTime)value).Date;
            var picker = (FluentDatePicker)bindable;
            if (picker.Date > dateValue)
                picker.Date = dateValue;

            return dateValue;
        }

        static object CoerceMinimumDate(BindableObject bindable, object value)
        {
            DateTime dateValue = ((DateTime)value).Date;
            var picker = (FluentDatePicker)bindable;
            if (picker.Date < dateValue)
                picker.Date = dateValue;

            return dateValue;
        }

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
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

        public FluentDatePicker()
        {
            InitializeComponent();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            myDatePicker.Focus();
        }

    }
}