using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class ProvidentFundViewModel : BaseViewModel
    {
        //PF == Provident Fund 公积金贷款

        private string pFTotalPrice;    //公积金 房价总额
        public string PFTotalPrice
        {
            get { return pFTotalPrice; }
            set { SetProperty(ref pFTotalPrice, value); }
        }

        private string pFLoanAmount;   //公积金 贷款金额
        public string PFLoanAmount
        {
            get { return pFLoanAmount; }
            set { SetProperty(ref pFLoanAmount, value); }
        }

        private int pFLoanProportionIndex;    //公积金 贷款比例
        public int PFLoanProportionIndex
        {
            get { return pFLoanProportionIndex; }
            set { SetProperty(ref pFLoanProportionIndex, value); }
        }

        private int pFYearIndex;    //公积金 按揭年数
        public int PFYearIndex
        {
            get { return pFYearIndex; }
            set { SetProperty(ref pFYearIndex, value); }
        }

        private string pFRate;    //公积金 利率
        public string PFRate
        {
            get { return pFRate; }
            set { SetProperty(ref pFRate, value); }
        }



        private string pFTotalLoan;    //公积金 贷款总额
        public string PFTotalLoan
        {
            get { return pFTotalLoan; }
            set { SetProperty(ref pFTotalLoan, value); }
        }

        private string pFTotalInterest;    //公积金 总利息
        public string PFTotalInterest
        {
            get { return pFTotalInterest; }
            set { SetProperty(ref pFTotalInterest, value); }
        }

        private string pFMonthlyRepayment;    //公积金 月均还款
        public string PFMonthlyRepayment
        {
            get { return pFMonthlyRepayment; }
            set { SetProperty(ref pFMonthlyRepayment, value); }
        }

        #region 选择器
        private List<string> pFYearList;   //公积金 按揭年数列表
        public List<string> PFYearList
        {
            get { return pFYearList; }
            set { SetProperty(ref pFYearList, value); }
        }

        private List<string> pFLoanProportionList;   //公积金 比例列表
        public List<string> PFLoanProportionList
        {
            get { return pFLoanProportionList; }
            set { SetProperty(ref pFLoanProportionList, value); }
        }

        private List<string> calculationList;   //计算方式
        public List<string> CalculationList
        {
            get { return calculationList; }
            set { SetProperty(ref calculationList, value); }
        }

        private List<string> paymentList;   //还款方式
        public List<string> PaymentList
        {
            get { return paymentList; }
            set { SetProperty(ref paymentList, value); }
        }
        #endregion

        #region 单选
        private bool totalPriceRadio;   //按房价总额计算
        public bool TotalPriceRadio
        {
            get { return totalPriceRadio; }
            set { SetProperty(ref totalPriceRadio, value); }
        }

        private bool totalLoanRadio;   //按贷款总额计算
        public bool TotalLoanRadio
        {
            get { return totalLoanRadio; }
            set { SetProperty(ref totalLoanRadio, value); }
        }

        private bool aveCapAndIntRadio;   //等额本息
        public bool AveCapAndIntRadio
        {
            get { return aveCapAndIntRadio; }
            set { SetProperty(ref aveCapAndIntRadio, value); }
        }

        private bool aveCapRadio;   //等额本金
        public bool AveCapRadio
        {
            get { return aveCapRadio; }
            set { SetProperty(ref aveCapRadio, value); }
        }
        #endregion

        private string calculateMethod;   //Comment
        public string CalculateMethod
        {
            get { return calculateMethod; }
            set { SetProperty(ref calculateMethod, value); }
        }

        private string payment;   //Comment
        public string Payment
        {
            get { return payment; }
            set { SetProperty(ref payment, value); }
        }

        private double TotalLoan;           //贷款总额
        private double TotalInterest;       //总利息
        private double MonthlyRepayment;     //月均还款

        public Command ClearCommand { get; set; }
        public Command CalculateCommand { get; set; }

        public ProvidentFundViewModel()
        {
            //InitPickers();

            PFYearList = new List<string>();
            PFLoanProportionList = new List<string>();

            for (int i = 1; i <= 30; i++)
            {
                PFYearList.Add(i + "年（" + i * 12 + "期）");
            }

            for (int j = 4; j <= 16; j++)
            {
                PFLoanProportionList.Add(j * 5 + "%");
            }

            CalculationList = new List<string>() { "按房价总额", "按贷款总额" };
            PaymentList = new List<string>() { "等额本息", "等额本金" };
            CalculateMethod = CalculationList[0];
            Payment = PaymentList[0];

            TotalPriceRadio = true;
            AveCapAndIntRadio = true;

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
        /// 初始化选择器
        /// </summary>
        private void InitPickers()
        {
            PFYearList = new List<string>();
            PFLoanProportionList = new List<string>();

            for (int i = 1; i <= 30; i++)
            {
                PFYearList.Add(i + "年（" + i * 12 + "期）");
            }

            for (int j = 4; j <= 16; j++)
            {
                PFLoanProportionList.Add(j * 5 + "%");
            }

            CalculationList = new List<string>() { "按房价总额", "按贷款总额" };
            PaymentList = new List<string>() { "等额本息", "等额本金" };
            //CalculateMethod = CalculationList[0];
            //Payment = PaymentList[0];
        }

        /// <summary>
        /// 计算
        /// </summary>
        private void Calculate()
        {
            double MonthlyIntRate = (double.Parse(PFRate) / 100.0) / 12;   //月利率
            int months = (PFYearIndex + 1) * 12;    //还款月数

            //按房价总额计算
            if (TotalPriceRadio)
            {
                double proportion = (PFLoanProportionIndex + 4) / 20.0;
                TotalLoan = double.Parse(PFTotalPrice) * proportion;
                PFTotalLoan = TotalLoan.ToString();    //贷款总额

                if (AveCapAndIntRadio)
                {
                    //等额本息
                    TotalInterest = ((TotalLoan * MonthlyIntRate * Math.Pow((1 + MonthlyIntRate), months)) / (Math.Pow((1 + MonthlyIntRate), months) - 1)) * months - TotalLoan;
                    PFTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                }
                else
                {
                    //等额本金
                    TotalInterest = months * ((TotalLoan * MonthlyIntRate) - (MonthlyIntRate * (TotalLoan / months) * (months - 1)) / 2 + (TotalLoan / months)) - TotalLoan;
                    PFTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                }
            }

            //按贷款总额计算
            if (TotalLoanRadio)
            {
                TotalLoan = double.Parse(PFLoanAmount);
                PFTotalLoan = TotalLoan.ToString();   //输入的贷款金额PFLoanAmount就是贷款总额PFTotalLoan

                if (AveCapAndIntRadio)
                {
                    //等额本息
                    TotalInterest = ((TotalLoan * MonthlyIntRate * Math.Pow((1 + MonthlyIntRate), months)) / (Math.Pow((1 + MonthlyIntRate), months) - 1)) * months - TotalLoan;
                    PFTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                }
                else
                {
                    //等额本金
                    TotalInterest = months * ((TotalLoan * MonthlyIntRate) - (MonthlyIntRate * (TotalLoan / months) * (months - 1)) / 2 + (TotalLoan / months)) - TotalLoan;
                    PFTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                }
            }

            MonthlyRepayment = (TotalLoan + TotalInterest) / months;
            PFMonthlyRepayment = DecimalFormatting(MonthlyRepayment.ToString());    //月均还款

            //ResultStack.IsVisible = true;
        }

        /// <summary>
        /// 检查输入是否有误
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            string regexString = @"[1-9]\d*.\d*|0.\d*[1-9]\d*";

            if (!TotalPriceRadio && !TotalLoanRadio)
            {
                CrossToastPopUp.Current.ShowToastError("请选择计算方式！", ToastLength.Long);
                return false;
            }

            if (!AveCapAndIntRadio && !AveCapRadio)
            {
                CrossToastPopUp.Current.ShowToastError("请选择还款方式！", ToastLength.Long);
                return false;
            }

            if (TotalPriceRadio && !TotalLoanRadio)
            {
                if (PFTotalPrice == null || PFTotalPrice == "")
                {
                    CrossToastPopUp.Current.ShowToastError("请输入房价总额！", ToastLength.Long);
                    return false;
                }
                else if (!Regex.IsMatch(PFTotalPrice, regexString))
                {
                    CrossToastPopUp.Current.ShowToastError("房价总额格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                    return false;
                }
                //else if (PFLoanProportionIndex == null)
                //{
                //    CrossToastPopUp.Current.ShowToastError("请选择贷款比例！", ToastLength.Long);
                //    return false;
                //}
            }

            if (!TotalPriceRadio && TotalLoanRadio)
            {
                if (PFLoanAmount == null || PFLoanAmount == "")
                {
                    CrossToastPopUp.Current.ShowToastError("贷款金额不能为空！", ToastLength.Long);
                    return false;
                }
                else if (!Regex.IsMatch(PFLoanAmount, regexString))
                {
                    CrossToastPopUp.Current.ShowToastError("贷款金额格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                    return false;
                }
            }

            //if (PFYear == null || PFYear == "")
            //{
            //    CrossToastPopUp.Current.ShowToastError("请选择按揭年数！", ToastLength.Long);
            //    return false;
            //}

            if (PFRate == null || PFRate == "")
            {
                CrossToastPopUp.Current.ShowToastError("请输入公积金利率！", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(PFRate, regexString))
            {
                CrossToastPopUp.Current.ShowToastError("公积金利率格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                return false;
            }

            return true;
        }

        private string DecimalFormatting(string str)
        {
            if (str.Contains('.'))
            {
                return str.Split('.')[0] + "." + (str.Split('.')[1].Length >= 2 ? str.Split('.')[1].Substring(0, 2) : str.Split('.')[1]);
            }
            else
            {
                return str;
            }

        }
    }
}
