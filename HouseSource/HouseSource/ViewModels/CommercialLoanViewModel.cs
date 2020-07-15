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
    public class CommercialLoanViewModel : BaseViewModel
    {
        // CL == Commercial Loan  商业贷款

        private string cLTotalPrice;    //商业贷款 房价总额
        public string CLTotalPrice
        {
            get { return cLTotalPrice; }
            set { SetProperty(ref cLTotalPrice, value); }
        }

        private string cLLoanAmount;   //商业贷款 贷款金额
        public string CLLoanAmount
        {
            get { return cLLoanAmount; }
            set { SetProperty(ref cLLoanAmount, value); }
        }

        private int cLLoanProportionIndex;    //商业贷款 贷款比例
        public int CLLoanProportionIndex
        {
            get { return cLLoanProportionIndex; }
            set { SetProperty(ref cLLoanProportionIndex, value); }
        }

        private int cLYearIndex;    //商业贷款 按揭年数
        public int CLYearIndex
        {
            get { return cLYearIndex; }
            set { SetProperty(ref cLYearIndex, value); }
        }

        private string cLRate;    //商业贷款 利率
        public string CLRate
        {
            get { return cLRate; }
            set { SetProperty(ref cLRate, value); }
        }


        private string cLTotalLoan;    //商业贷款 贷款总额
        public string CLTotalLoan
        {
            get { return cLTotalLoan; }
            set { SetProperty(ref cLTotalLoan, value); }
        }

        private string cLTotalInterest;    //商业贷款 总利息
        public string CLTotalInterest
        {
            get { return cLTotalInterest; }
            set { SetProperty(ref cLTotalInterest, value); }
        }

        private string cLMonthlyRepayment;    //商业贷款 月均还款
        public string CLMonthlyRepayment
        {
            get { return cLMonthlyRepayment; }
            set { SetProperty(ref cLMonthlyRepayment, value); }
        }

        #region 选择器
        private List<string> cLYearList;   //商业 按揭年数列表
        public List<string> CLYearList
        {
            get { return cLYearList; }
            set { SetProperty(ref cLYearList, value); }
        }

        private List<string> cLLoanProportionList;   //商业 比例列表
        public List<string> CLLoanProportionList
        {
            get { return cLLoanProportionList; }
            set { SetProperty(ref cLLoanProportionList, value); }
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

        private double TotalLoan;           //贷款总额
        private double TotalInterest;       //总利息
        private double MonthlyRepayment;     //月均还款

        public Command ClearCommand { get; set; }
        public Command CalculateCommand { get; set; }

        public CommercialLoanViewModel()
        {
            InitPickers();

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
            CLYearList = new List<string>();
            CLLoanProportionList = new List<string>();

            for (int i = 1; i <= 30; i++)
            {
                CLYearList.Add(i + "年（" + i * 12 + "期）");
            }

            for (int j = 4; j <= 16; j++)
            {
                CLLoanProportionList.Add(j * 5 + "%");
            }
        }


        /// <summary>
        /// 计算
        /// </summary>
        private void Calculate()
        {
                double MonthlyIntRate = (double.Parse(CLRate) / 100.0) / 12;   //月利率
                int months = (CLYearIndex + 1) * 12;    //还款月数

                //按房价总额计算
                if (TotalPriceRadio)
                {
                    double proportion = (CLLoanProportionIndex + 4) / 20.0;
                    TotalLoan = double.Parse(CLTotalPrice) * proportion;
                    CLTotalLoan = TotalLoan.ToString();    //贷款总额

                    if (AveCapAndIntRadio)
                    {
                        //等额本息
                        TotalInterest = TotalLoan * ((months * MonthlyIntRate - 1) * Math.Pow((1 + MonthlyIntRate), months) + 1) / (Math.Pow((1 + MonthlyIntRate), months) - 1);
                        CLTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                    }
                    else
                    {
                        //等额本金
                        TotalInterest = (TotalLoan * MonthlyIntRate * (months + 1) / 2);
                        CLTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                    }
                }

                //按贷款总额计算
                if (TotalLoanRadio)
                {
                    TotalLoan = double.Parse(CLLoanAmount);
                    CLTotalLoan = TotalLoan.ToString();   //输入的贷款金额CLLoanAmount就是贷款总额CLTotalLoan

                    if (AveCapAndIntRadio)
                    {
                        //等额本息
                        TotalInterest = TotalLoan * ((months * MonthlyIntRate - 1) * Math.Pow((1 + MonthlyIntRate), months) + 1) / (Math.Pow((1 + MonthlyIntRate), months) - 1);
                        CLTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                    }
                    else
                    {
                        //等额本金
                        TotalInterest = (TotalLoan * MonthlyIntRate * (months + 1) / 2);
                        CLTotalInterest = DecimalFormatting(TotalInterest.ToString());   //总利息
                    }
                }

                MonthlyRepayment = (TotalLoan + TotalInterest) / months;
                CLMonthlyRepayment = DecimalFormatting(MonthlyRepayment.ToString());    //月均还款

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
                if (CLTotalPrice == null || CLTotalPrice == "")
                {
                    CrossToastPopUp.Current.ShowToastError("请输入房价总额！", ToastLength.Long);
                    return false;
                }
                else if (!Regex.IsMatch(CLTotalPrice, regexString))
                {
                    CrossToastPopUp.Current.ShowToastError("房价总额格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                    return false;
                }
                //else if (CLLoanProportion == null || CLLoanProportion == "")
                //{
                //    CrossToastPopUp.Current.ShowToastError("请选择贷款比例！", ToastLength.Long);
                //    return false;
                //}
            }

            if (!TotalPriceRadio && TotalLoanRadio)
            {
                if (CLLoanAmount == null || CLLoanAmount == "")
                {
                    CrossToastPopUp.Current.ShowToastError("贷款金额不能为空！", ToastLength.Long);
                    return false;
                }
                else if (!Regex.IsMatch(CLLoanAmount, regexString))
                {
                    CrossToastPopUp.Current.ShowToastError("贷款金额格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                    return false;
                }
            }

            //if (CLYear == null || CLYear == "")
            //{
            //    CrossToastPopUp.Current.ShowToastError("请选择按揭年数！", ToastLength.Long);
            //    return false;
            //}

            if (CLRate == null || CLRate == "")
            {
                CrossToastPopUp.Current.ShowToastError("请输入商贷利率！", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(CLRate, regexString))
            {
                CrossToastPopUp.Current.ShowToastError("商贷利率格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
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
