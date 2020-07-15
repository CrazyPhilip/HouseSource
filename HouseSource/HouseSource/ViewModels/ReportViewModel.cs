using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Services;
using HouseSource.Models;
using HouseSource.ResponseData;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Microcharts;
using SkiaSharp;
using UltimateXF.Widget.Charts.Models;
using UltimateXF.Widget.Charts.Models.LineChart;
using UltimateXF.Widget.Charts.Models.Formatters;
using UltimateXF.Widget.Charts.Models.Component;

namespace HouseSource.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {

        private LineChart barChartView;   //Comment
        public LineChart BarChartView
        {
            get { return barChartView; }
            set { SetProperty(ref barChartView, value); }
        }

        private LineChartData myLineChartData;   //Comment
        public LineChartData MyLineChartData
        {
            get { return myLineChartData; }
            set { SetProperty(ref myLineChartData, value); }
        }

        private XAxisConfig myXAxisConfig;   //Comment
        public XAxisConfig MyXAxisConfig
        {
            get { return myXAxisConfig; }
            set { SetProperty(ref myXAxisConfig, value); }
        }

        private ChartDescription description;   //Comment
        public ChartDescription Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }


        public ReportViewModel()
        {

            var entries2 = new List<EntryChart>();
            var labels = new List<string>();

            Description = new ChartDescription { Text = "这是周报" };
            MyXAxisConfig = new XAxisConfig
            {
                DrawGridLines = false,
                DrawAxisLine = true,
                XAXISPosition = XAXISPosition.BOTTOM,
                AxisValueFormatter = new TextByIndexXAxisFormatter(labels)
            };

            var random = new Random();
            for (int i = 0; i < 7; i++)
            {
                entries2.Add(new EntryChart(i, random.Next(1000, 50000)));
                labels.Add("Entry" + i);
            }

            var dataSet5 = new LineDataSetXF(entries2, "Line DataSet 2")
            {
                Colors = new List<Color> { Color.Green },
                CircleHoleColor = Color.Blue,
                CircleColors = new List<Color> { Color.Blue },
                CircleRadius = 3,
                DrawValues = false
            };

            MyLineChartData = new LineChartData(new List<ILineDataSetXF>() { dataSet5 });


            BarChartView = new LineChart
            {
                Entries = new[]
                {
                    new ChartEntry(200) { Label = "January", ValueLabel = "200", Color = SKColor.Parse("#266489") },
                    new ChartEntry(400) { Label = "February", ValueLabel = "400", Color = SKColor.Parse("#68B9C0") },
                    new ChartEntry(-100) { Label = "March", ValueLabel = "-100", Color = SKColor.Parse("#90D585")}
                }
            };
        }
    }
}
