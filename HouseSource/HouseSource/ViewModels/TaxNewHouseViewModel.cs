using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class TaxNewHouseViewModel : BaseViewModel
    {
        #region 计算结果
        private double newTotalPrice;    //新房 总价
        public double NewTotalPrice
        {
            get { return newTotalPrice; }
            set { SetProperty(ref newTotalPrice, value); }
        }

        private double newDeedTax;    //新房 契税
        public double NewDeedTax
        {
            get { return newDeedTax; }
            set { SetProperty(ref newDeedTax, value); }
        }

        private double newMaintenanceFund;    //新房 维修基金
        public double NewMaintenanceFund
        {
            get { return newMaintenanceFund; }
            set { SetProperty(ref newMaintenanceFund, value); }
        }

        private double newTotalTax;    //新房 税金总额
        public double NewTotalTax
        {
            get { return newTotalTax; }
            set { SetProperty(ref newTotalTax, value); }
        }
        #endregion

        #region
        private string newHouseArea;    //建筑面积
        public string NewHouseArea
        {
            get { return newHouseArea; }
            set { SetProperty(ref newHouseArea, value); }
        }

        private string newUnitPrice;    //单价
        public string NewUnitPrice
        {
            get { return newUnitPrice; }
            set { SetProperty(ref newUnitPrice, value); }
        }
        #endregion

        private bool yesRadio;   //是首次购房
        public bool YesRadio
        {
            get { return yesRadio; }
            set { SetProperty(ref yesRadio, value); }
        }

        private bool noRadio;   //不是首次购房
        public bool NoRadio
        {
            get { return noRadio; }
            set { SetProperty(ref noRadio, value); }
        }

        public Command ClearCommand { get; set; }
        public Command CalculateCommand { get; set; }

        public TaxNewHouseViewModel()
        {
            YesRadio = true;

            ClearCommand = new Command(() =>
            {

            }, () => { return true; });

            CalculateCommand = new Command(() =>
            {
                if (CheckInput())
                {
                    Calculate();
                }
            }, () => { return true; });
        }

        /// <summary>
        /// 新房 计算
        /// </summary>
        private void Calculate()
        {
            NewTotalPrice = double.Parse(NewHouseArea) * double.Parse(NewUnitPrice);
            //NewTotalPrice = DecimalFormatting(newTotalPrice.ToString());    //新房 总价

            NewMaintenanceFund = NewTotalPrice * 0.03;
            //NewMaintenanceFund = DecimalFormatting(newMaintenanceFund.ToString());    //新房 维修基金

            if (YesRadio)
            {
                NewDeedTax = NewTotalPrice * 0.015;
                //NewDeedTax = DecimalFormatting(newDeedTax.ToString());    //新房 契税

                NewTotalTax = newDeedTax + newMaintenanceFund + 85;
                //NewTotalTax = DecimalFormatting(newTotalTax.ToString());    //新房 税金总额
            }

            //NewResultStack.IsVisible = true;

        }

        /// <summary>
        /// 新房 输入检测
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            string regexString = @"[1-9]\d*.\d*|0.\d*[1-9]\d*";

            if (NewHouseArea == null || NewHouseArea == "")
            {
                CrossToastPopUp.Current.ShowToastError("请输入建筑面积！", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(NewHouseArea, regexString))
            {
                CrossToastPopUp.Current.ShowToastError("建筑面积格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                return false;
            }

            if (NewUnitPrice == null || NewUnitPrice == "")
            {
                CrossToastPopUp.Current.ShowToastError("请输入单价！", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(NewUnitPrice, regexString))
            {
                CrossToastPopUp.Current.ShowToastError("单价格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                return false;
            }

            if (!YesRadio && !NoRadio)
            {
                CrossToastPopUp.Current.ShowToastError("请选择是否首次购房！", ToastLength.Long);
                return false;
            }

            return true;
        }
    }
}
