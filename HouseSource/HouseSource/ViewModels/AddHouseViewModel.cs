using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class AddHouseViewModel : BaseViewModel
    {
        private List<string> tradeList;   //租售类型
        public List<string> TradeLsit
        {
            get { return tradeList; }
            set { SetProperty(ref tradeList, value); }
        }

        private List<string> propertyTypeList;   //房型列表
        public List<string> PropertyTypeList
        {
            get { return propertyTypeList; }
            set { SetProperty(ref propertyTypeList, value); }
        }

        private List<string> decoration;   //装修列表
        public List<string> Decoration
        {
            get { return decoration; }
            set { SetProperty(ref decoration, value); }
        }

        private List<string> directionList;   //房屋朝向列表
        public List<string> DirectionList
        {
            get { return directionList; }
            set { SetProperty(ref directionList, value); }
        }

        private List<string> propertyUseTypeList;   //使用类型列表
        public List<string> PropertyUseTypeList
        {
            get { return propertyUseTypeList; }
            set { SetProperty(ref propertyUseTypeList, value); }
        }

        private List<string> propertySourceList;   //来源类型列表
        public List<string> PropertySourceList
        {
            get { return propertySourceList; }
            set { SetProperty(ref propertySourceList, value); }
        }

        private List<string> signList;   //特点
        public List<string> SignList
        {
            get { return signList; }
            set { SetProperty(ref signList, value); }
        }

        private List<string> propertyTradeList;   //交易方式
        public List<string> PropertyTradeList
        {
            get { return propertyTradeList; }
            set { SetProperty(ref propertyTradeList, value); }
        }

        public Command AddCommand { get; set; }
        public Command ClearCommand { get; set; }

        public AddHouseViewModel()
        {
            AddCommand = new Command(() =>
            {

            }, () => { return true; });

            ClearCommand = new Command(() =>
            {

            }, () => { return true; });


        }
    }
}
