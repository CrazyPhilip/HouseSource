using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using HouseSource.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Orientation = Android.Widget.Orientation;
using AColor = Android.Graphics.Color;
using Android.Graphics.Drawables;
using HouseSource.Controls;
using Android.Text;
using Android.Text.Style;

[assembly: ExportRenderer(typeof(MyPicker), typeof(MyPickerRenderer))]
namespace HouseSource.Droid
{
    class MyPickerRenderer : PickerRenderer, IPickerRenderer
    {
        //AlertDialog _dialog;
        Dialog dialog;

        public MyPickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            Picker model = Element;
            if (Control != null)
            {
                Control.SetBackgroundColor(AColor.Transparent);
                Control.SetPadding(0, 0, 0, 0);
                Control.SetLines(1);
                Control.TextAlignment = Android.Views.TextAlignment.Center;

            }
        }

        IElementController ElementController => Element as IElementController;

        /*
        void IPickerRenderer.OnClick()
        {
            Picker model = Element;

            if (dialog != null)
                return;

            var picker = new NumberPicker(Context);
            if (model.Items != null && model.Items.Any())
            {
                picker.MaxValue = model.Items.Count - 1;
                picker.MinValue = 0;
                picker.SetDisplayedValues(model.Items.ToArray());
                picker.WrapSelectorWheel = false;
                picker.DescendantFocusability = DescendantFocusability.BlockDescendants;
                picker.Value = model.SelectedIndex;
                //picker.HorizontalFadingEdgeEnabled = true;
            }

            Android.Widget.Button button = new Android.Widget.Button(Context);
            button.Text = "确定";
            button.SetWidth(LayoutParams.MatchParent);
            button.SetBackgroundColor(AColor.Transparent);

            button.Click += (sender, e) =>
            {
                ElementController.SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
                // It is possible for the Content of the Page to be changed on SelectedIndexChanged. 
                // In this case, the Element & Control will no longer exist.
                if (Element != null)
                {
                    if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
                        Control.Text = model.Items[Element.SelectedIndex];
                    ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                }
                dialog.Dismiss();
                dialog = null;
            };

            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(AColor.White);
            gd.SetCornerRadius(20);

            var layout = new LinearLayout(Context);
            //LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            //layoutParams.SetMargins(0, 0, 0, 0);

            layout.Orientation = Orientation.Vertical;
            layout.SetPadding(20, 20, 20, 20);
            layout.SetMinimumWidth(10000);
            layout.AddView(picker);
            layout.AddView(button);
            layout.SetBackground(gd);

            ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);

            dialog = new Dialog(Context, Resource.Style.edit_AlertDialog_style);
            dialog.SetCanceledOnTouchOutside(true);

            WindowManagerLayoutParams layoutParams = new WindowManagerLayoutParams
            {
                Height = LayoutParams.WrapContent,
                Width = LayoutParams.MatchParent,
                Gravity = GravityFlags.Bottom,

            };
            //dialog.Window.SetGravity(GravityFlags.Bottom);
            //dialog.Window.Attributes.Height = ViewGroup.LayoutParams.WrapContent;
            //dialog.Window.Attributes.Width = ViewGroup.LayoutParams.MatchParent;
            dialog.Window.Attributes = layoutParams;

            dialog.SetContentView(layout);

            dialog.Create();
            dialog.DismissEvent += (sender, args) =>
            {
                ElementController?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                dialog?.Dispose();
                dialog = null;
            };
            dialog.Show();

        }
        */

        void IPickerRenderer.OnClick()
        {
            Picker model = Element;
            if (dialog == null)
            {
                using (var builder = new AlertDialog.Builder(Context))
                {
                    if (!Element.IsSet(Picker.TitleColorProperty))
                    {
                        builder.SetTitle(model.Title ?? "");
                    }
                    else
                    {
                        var title = new SpannableString(model.Title ?? "");
                        //title.SetSpan(new ForegroundColorSpan(model.TitleColor.ToAndroid()), 0, title.Length(), SpanTypes.ExclusiveExclusive);
                        title.SetSpan(new ForegroundColorSpan(AColor.Black), 0, title.Length(), SpanTypes.ExclusiveExclusive);

                        builder.SetTitle(title);
                    }

                    string[] items = model.Items.ToArray();
                    builder.SetItems(items, (s, e) => ((IElementController)model).SetValueFromRenderer(Picker.SelectedIndexProperty, e.Which));

                    builder.SetNegativeButton(global::Android.Resource.String.Cancel, (o, args) => { });

                    ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);

                    dialog = builder.Create();
                }
                dialog.SetCanceledOnTouchOutside(true);
                dialog.DismissEvent += (sender, args) =>
                {
                    (Element as IElementController)?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                    dialog?.Dispose();
                    dialog = null;
                };

                dialog.Show();
            }
        }

    }
}
