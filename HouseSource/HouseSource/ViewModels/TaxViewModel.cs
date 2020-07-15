using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class TaxViewModel : BaseViewModel
    {
        #region 新房
        private string newTotalPrice;    //新房 总价
        public string NewTotalPrice
        {
            get { return newTotalPrice; }
            set { SetProperty(ref newTotalPrice, value); }
        }

        private string newDeedTax;    //新房 契税
        public string NewDeedTax
        {
            get { return newDeedTax; }
            set { SetProperty(ref newDeedTax, value); }
        }

        private string newMaintenanceFund;    //新房 维修基金
        public string NewMaintenanceFund
        {
            get { return newMaintenanceFund; }
            set { SetProperty(ref newMaintenanceFund, value); }
        }

        private string newTotalTax;    //新房 税金总额
        public string NewTotalTax
        {
            get { return newTotalTax; }
            set { SetProperty(ref newTotalTax, value); }
        }

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

        #region 二手房
        private string resoldDeedTax;    //二手房 契税
        public string ResoldDeedTax
        {
            get { return resoldDeedTax; }
            set { SetProperty(ref resoldDeedTax, value); }
        }

        private string resoldAddedValueTax;    //二手房 增值税
        public string ResoldAddedValueTax
        {
            get { return resoldAddedValueTax; }
            set { SetProperty(ref resoldAddedValueTax, value); }
        }

        private string resoldIndividualIncomeTax;    //二手房 个人所得税
        public string ResoldIndividualIncomeTax
        {
            get { return resoldIndividualIncomeTax; }
            set { SetProperty(ref resoldIndividualIncomeTax, value); }
        }

        private string resoldComprehensiveLandPriceTax;    //二手房 综合地价税
        public string ResoldComprehensiveLandPriceTax
        {
            get { return resoldComprehensiveLandPriceTax; }
            set { SetProperty(ref resoldComprehensiveLandPriceTax, value); }
        }

        private string resoldTotalTax;    //二手房 税金总额
        public string ResoldTotalTax
        {
            get { return resoldTotalTax; }
            set { SetProperty(ref resoldTotalTax, value); }
        }

        private string resoldHouseArea;    //建筑面积
        public string ResoldHouseArea
        {
            get { return resoldHouseArea; }
            set { SetProperty(ref resoldHouseArea, value); }
        }

        private string resoldTotalPrice;    //总价
        public string ResoldTotalPrice
        {
            get { return resoldTotalPrice; }
            set { SetProperty(ref resoldTotalPrice, value); }
        }

        private string resoldOriginalTotalPrice;    //原价
        public string ResoldOriginalTotalPrice
        {
            get { return resoldOriginalTotalPrice; }
            set { SetProperty(ref resoldOriginalTotalPrice, value); }
        }
        #endregion

        public Command<string> ClearCommand { get; set; }
        public Command<string> CalculateCommand { get; set; }

        public TaxViewModel()
        {
            ClearCommand = new Command<string>((p) =>
            {
                CrossToastPopUp.Current.ShowToastSuccess(p, ToastLength.Short);
            }, (p) => { return true; });

            CalculateCommand = new Command<string>((p) =>
            {
                CrossToastPopUp.Current.ShowToastSuccess(p, ToastLength.Short);
            }, (p) => { return true; });
        }
    }
}
