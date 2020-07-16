using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class TaxResoldHouseViewModel : BaseViewModel
    {
        #region 计算结果
        private double resoldDeedTax;    //二手房 契税
        public double ResoldDeedTax
        {
            get { return resoldDeedTax; }
            set { SetProperty(ref resoldDeedTax, value); }
        }

        private double resoldAddedValueTax;    //二手房 增值税
        public double ResoldAddedValueTax
        {
            get { return resoldAddedValueTax; }
            set { SetProperty(ref resoldAddedValueTax, value); }
        }

        private double resoldIndividualIncomeTax;    //二手房 个人所得税
        public double ResoldIndividualIncomeTax
        {
            get { return resoldIndividualIncomeTax; }
            set { SetProperty(ref resoldIndividualIncomeTax, value); }
        }

        private double resoldComprehensiveLandPriceTax;    //二手房 综合地价税
        public double ResoldComprehensiveLandPriceTax
        {
            get { return resoldComprehensiveLandPriceTax; }
            set { SetProperty(ref resoldComprehensiveLandPriceTax, value); }
        }

        private double resoldTotalTax;    //二手房 税金总额
        public double ResoldTotalTax
        {
            get { return resoldTotalTax; }
            set { SetProperty(ref resoldTotalTax, value); }
        }
        #endregion

        #region
        private double resoldHouseArea;    //建筑面积
        public double ResoldHouseArea
        {
            get { return resoldHouseArea; }
            set { SetProperty(ref resoldHouseArea, value); }
        }

        private double resoldTotalPrice;    //总价
        public double ResoldTotalPrice
        {
            get { return resoldTotalPrice; }
            set { SetProperty(ref resoldTotalPrice, value); }
        }

        private double resoldOriginalTotalPrice;    //原价
        public double ResoldOriginalTotalPrice
        {
            get { return resoldOriginalTotalPrice; }
            set { SetProperty(ref resoldOriginalTotalPrice, value); }
        }
        #endregion

        #region 单选
        private bool commonResidenceRadio;   //普通住宅
        public bool CommonResidenceRadio
        {
            get { return commonResidenceRadio; }
            set { SetProperty(ref commonResidenceRadio, value); }
        }

        private bool nonOrdinaryResidenceRadio;   //非普通住宅
        public bool NonOrdinaryResidenceRadio
        {
            get { return nonOrdinaryResidenceRadio; }
            set { SetProperty(ref nonOrdinaryResidenceRadio, value); }
        }

        private bool affordableHouseRadio;   //经济适用房
        public bool AffordableHouseRadio
        {
            get { return affordableHouseRadio; }
            set { SetProperty(ref affordableHouseRadio, value); }
        }

        private bool totalPriceRadio;   //按总价计算
        public bool TotalPriceRadio
        {
            get { return totalPriceRadio; }
            set { SetProperty(ref totalPriceRadio, value); }
        }

        private bool priceSpreadRadio;   //按差价计算
        public bool PriceSpreadRadio
        {
            get { return priceSpreadRadio; }
            set { SetProperty(ref priceSpreadRadio, value); }
        }

        private bool lessThanTwoRadio;   //不满两年
        public bool LessThanTwoRadio
        {
            get { return lessThanTwoRadio; }
            set { SetProperty(ref lessThanTwoRadio, value); }
        }

        private bool twoToFiveRadio;   //二到五年
        public bool TwoToFiveRadio
        {
            get { return twoToFiveRadio; }
            set { SetProperty(ref twoToFiveRadio, value); }
        }

        private bool moreThanFiveRadio;   //五年以上
        public bool MoreThanFiveRadio
        {
            get { return moreThanFiveRadio; }
            set { SetProperty(ref moreThanFiveRadio, value); }
        }

        private bool resoldYesFirstRadio;   //是首次购房
        public bool ResoldYesFirstRadio
        {
            get { return resoldYesFirstRadio; }
            set { SetProperty(ref resoldYesFirstRadio, value); }
        }

        private bool resoldNoFirstRadio;   //不是首次购房
        public bool ResoldNoFirstRadio
        {
            get { return resoldNoFirstRadio; }
            set { SetProperty(ref resoldNoFirstRadio, value); }
        }

        private bool resoldOnlyYesRadio;   //是唯一住房
        public bool ResoldOnlyYesRadio
        {
            get { return resoldOnlyYesRadio; }
            set { SetProperty(ref resoldOnlyYesRadio, value); }
        }

        private bool resoldOnlyNoRadio;   //不是唯一住房
        public bool ResoldOnlyNoRadio
        {
            get { return resoldOnlyNoRadio; }
            set { SetProperty(ref resoldOnlyNoRadio, value); }
        }

        #endregion

        private double resoldDifferencePrice;    //二手房 差价

        public Command ClearCommand { get; set; }
        public Command CalculateCommand { get; set; }

        public TaxResoldHouseViewModel()
        {
            CommonResidenceRadio = true;
            TotalPriceRadio = true;
            LessThanTwoRadio = true;
            ResoldYesFirstRadio = true;
            ResoldOnlyYesRadio = true;

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
        /// 二手房 计算
        /// </summary>
        private void Calculate()
        {
            //resoldTotalPrice = double.Parse(ResoldTotalPrice);
            ResoldIndividualIncomeTax = ResoldTotalPrice * 0.01;
            ResoldComprehensiveLandPriceTax = 0;

            if (AffordableHouseRadio && LessThanTwoRadio)
            {
                ResoldAddedValueTax = ResoldTotalPrice * 0.05;
            }
            else
            {
                ResoldAddedValueTax = 0;
            }

            if (AffordableHouseRadio && ResoldOnlyYesRadio && !LessThanTwoRadio)
            {
                ResoldIndividualIncomeTax = 0;
            }

            if (ResoldOnlyYesRadio)
            {
                ResoldDeedTax = ResoldTotalPrice * 0.015;
            }
            else
            {
                ResoldDeedTax = ResoldTotalPrice * 0.02;
            }

            if (PriceSpreadRadio && ResoldOnlyNoRadio)
            {
                resoldDifferencePrice = Math.Abs(resoldTotalPrice - ResoldOriginalTotalPrice);    //差价
                ResoldIndividualIncomeTax = resoldDifferencePrice * 0.2;
            }

            if (AffordableHouseRadio)
            {
                resoldComprehensiveLandPriceTax = resoldTotalPrice * 0.1;
            }

            ResoldTotalTax = resoldAddedValueTax + resoldDeedTax + resoldIndividualIncomeTax + resoldComprehensiveLandPriceTax + 5;

            //ResoldComprehensiveLandPriceTax = DecimalFormatting(resoldComprehensiveLandPriceTax.ToString());
            //ResoldDeedTax = DecimalFormatting(resoldDeedTax.ToString());
            ResoldIndividualIncomeTax = resoldIndividualIncomeTax;
            //ResoldAddedValueTax = DecimalFormatting(resoldAddedValueTax.ToString());
            //ResoldTotalTax = DecimalFormatting(resoldTotalTax.ToString());

            //ResoldResultStack.IsVisible = true;
        }

        /// <summary>
        /// 二手房 输入检测
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            string regexString = @"[1-9]\d*.\d*|0.\d*[1-9]\d*";

            if (!CommonResidenceRadio && !NonOrdinaryResidenceRadio && !AffordableHouseRadio)
            {
                CrossToastPopUp.Current.ShowToastError("请选择物业类型！", ToastLength.Long);
                return false;
            }

            if (!TotalPriceRadio && !PriceSpreadRadio)
            {
                CrossToastPopUp.Current.ShowToastError("请选择计算方式！", ToastLength.Long);
                return false;
            }

            if (!AffordableHouseRadio)
            {
                if (!LessThanTwoRadio && !TwoToFiveRadio && !MoreThanFiveRadio)
                {
                    CrossToastPopUp.Current.ShowToastError("请选择房产购置年限！", ToastLength.Long);
                    return false;
                }
            }

            if (!ResoldYesFirstRadio && !ResoldNoFirstRadio)
            {
                CrossToastPopUp.Current.ShowToastError("请选择是否首次购房！", ToastLength.Long);
                return false;
            }

            if (!ResoldOnlyYesRadio && !ResoldOnlyNoRadio)
            {
                CrossToastPopUp.Current.ShowToastError("请选择是否唯一住房！", ToastLength.Long);
                return false;
            }

            //if (ResoldHouseArea == null || ResoldHouseArea == "")
            //{
            //    CrossToastPopUp.Current.ShowToastError("请输入建筑面积！", ToastLength.Long);
            //    return false;
            //}

            //if (!Regex.IsMatch(ResoldHouseArea, regexString))
            //{
            //    CrossToastPopUp.Current.ShowToastError("建筑面积格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
            //    return false;
            //}

            //if (ResoldTotalPrice == null || ResoldTotalPrice == "")
            //{
            //    CrossToastPopUp.Current.ShowToastError("请输入总价！", ToastLength.Long);
            //    return false;
            //}

            //if (!Regex.IsMatch(ResoldTotalPrice, regexString))
            //{
            //    CrossToastPopUp.Current.ShowToastError("总价格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
            //    return false;
            //}

            //if (!TotalPriceRadio && PriceSpreadRadio)
            //{
            //    if (ResoldOriginalTotalPrice == null || ResoldOriginalTotalPrice == "")
            //    {
            //        CrossToastPopUp.Current.ShowToastError("请输入原价！", ToastLength.Long);
            //        return false;
            //    }
            //
            //    if (!Regex.IsMatch(ResoldOriginalTotalPrice, regexString))
            //    {
            //        CrossToastPopUp.Current.ShowToastError("原价格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
            //        return false;
            //    }
            //}

            return true;
        }
    }
}
