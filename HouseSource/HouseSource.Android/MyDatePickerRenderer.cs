using Android.Content;
using HouseSource.Controls;
using HouseSource.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(MyDatePicker), typeof(MyDatePickerRenderer))]
namespace HouseSource.Droid
{
    class MyDatePickerRenderer : DatePickerRenderer
    {
        public MyDatePickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(AColor.Transparent);
                Control.SetPadding(0, 0, 0, 0);
                Control.SetLines(1);
                Control.TextAlignment = Android.Views.TextAlignment.Center;
            }
        }

    }
}