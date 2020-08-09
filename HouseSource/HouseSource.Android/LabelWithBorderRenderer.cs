using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HouseSource.Controls;
using HouseSource.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(LabelWithBorder), typeof(LabelWithBorderRenderer))]
namespace HouseSource.Droid
{
    public class LabelWithBorderRenderer : LabelRenderer
    {
        public LabelWithBorderRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            LabelWithBorder model = (LabelWithBorder)Element;

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
            }

            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetShape(ShapeType.Rectangle);
                    gd.SetGradientType(GradientType.LinearGradient);
                    gd.SetCornerRadius(model.CornerRadius * 4);
                    gd.SetStroke(model.BorderThickness, model.BorderColor.ToAndroid());
                    gd.SetColor(model.DrawableColor.ToAndroid());

                    Control.SetPadding(2, 2, 2, 2);
                    Control.SetBackground(gd);
                }
            }

        }

    }
}