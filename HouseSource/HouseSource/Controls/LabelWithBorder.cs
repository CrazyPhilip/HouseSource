using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseSource.Controls
{
    public class LabelWithBorder : Label
    {
        public static readonly BindableProperty DrawableColorProperty = BindableProperty.Create(nameof(DrawableColor), typeof(Color), typeof(LabelWithBorder));
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(LabelWithBorder));
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(LabelWithBorder));
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(LabelWithBorder));

        public Color DrawableColor
        {
            get { return (Color)GetValue(DrawableColorProperty); }
            set { SetValue(DrawableColorProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public LabelWithBorder()
        {
        }
    }
}
