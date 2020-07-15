using System;
using System.Collections.Generic;
using UltimateXF.Widget.Charts.Models;
using UltimateXF.Widget.Charts.Models.Formatters;
using UltimateXF.Widget.Charts.Models.LineChart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            //var FontFamily = "";
            //switch (Device.RuntimePlatform)
            //{
            //    case Device.iOS:
            //        FontFamily = "Pacifico-Regular";
            //        break;
            //    case Device.Android:
            //        FontFamily = "Fonts/Pacifico-Regular.ttf";
            //        break;
            //    default:
            //        break;
            //}

            //chart.DescriptionChart.Text = "Test label chart description";
            //chart.AxisLeft.DrawGridLines = false;
            //chart.AxisLeft.DrawAxisLine = true;
            //chart.AxisLeft.Enabled = true;
            //
            //chart.AxisRight.DrawAxisLine = false;
            //chart.AxisRight.DrawGridLines = false;
            //chart.AxisRight.Enabled = false;
            
            //chart.AxisRight.FontFamily = FontFamily;
            //chart.AxisLeft.FontFamily = FontFamily;
            //chart.XAxis.FontFamily = FontFamily;
            
            //chart.XAxis.XAXISPosition = XAXISPosition.BOTTOM;
            //chart.XAxis.AxisValueFormatter = new TextByIndexXAxisFormatter(labels);
        }
    }
}