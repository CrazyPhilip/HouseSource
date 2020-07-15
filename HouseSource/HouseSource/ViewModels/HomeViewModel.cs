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
    public class HomeViewModel : BaseViewModel
    {
        private List<string> carouselList;   //comment
        public List<string> CarouselList
        {
            get { return carouselList; }
            set { SetProperty(ref carouselList, value); }
        }

        private List<Option> optionList;   //Comment
        public List<Option> OptionList
        {
            get { return optionList; }
            set { SetProperty(ref optionList, value); }
        }

        public Command<string> NavigateCommand { get; set; }    //导航命令事件

        public HomeViewModel()
        {
            CarouselList = new List<string>
            {
                "pic1.jpg", "pic2.jpg", "pic3.jpg"
            };

            OptionList = new List<Option>
            {
                new Option { icon = "building1.png", option = "全网房源", page = "HouseSource.Views.AllHouseListPage"},
                new Option { icon = "building2.png", option = "房源管理", page = "HouseSource.Views.HouseManagePage"},
                new Option { icon = "people.png", option = "客源管理", page = "HouseSource.Views.ClientManagePage"},
                new Option { icon = "news.png", option = "新闻公告", page = "HouseSource.Views.MessagePage"},
                new Option { icon = "calculator.png", option = "房贷计算器", page = "HouseSource.Views.LoanPage" },
                new Option { icon = "calculator.png", option = "税费计算器", page = "HouseSource.Views.TaxPage" },
                new Option { icon = "add.png", option = "新增房源", page = "HouseSource.Views.AddHousePage"},
                new Option { icon = "add_client.png", option = "新增客源", page = "HouseSource.Views.AddClientPage"}

            };

            //导航命令事件
            NavigateCommand = new Command<string>(async (pageName) =>
            {
                Type type = Type.GetType(pageName);
                Page page = (Page)Activator.CreateInstance(type);
                await Application.Current.MainPage.Navigation.PushAsync(page);

            }, (pageName) => { return true; });


            //InitHomePage();
        }

        /// <summary>
        /// 初始化首页
        /// </summary>
        private void InitHomePage()
        {

        }
    }
}
