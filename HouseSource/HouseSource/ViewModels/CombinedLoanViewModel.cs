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
    public class CombinedLoanViewModel : BaseViewModel
    {
        //PF == Provident Fund 公积金贷款

        private string pFLoanAmount;   //公积金 贷款金额
        public string PFLoanAmount
        {
            get { return pFLoanAmount; }
            set { SetProperty(ref pFLoanAmount, value); }
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

        // CL == Commercial Loan  商业贷款

        private string cLLoanAmount;   //商业贷款 贷款金额
        public string CLLoanAmount
        {
            get { return cLLoanAmount; }
            set { SetProperty(ref cLLoanAmount, value); }
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


        private string totalPrice;    //组合贷款 房价总额
        public string TotalPrice
        {
            get { return totalPrice; }
            set { SetProperty(ref totalPrice, value); }
        }

        private int loanProportionIndex;    //组合贷款 贷款比例
        public int LoanProportionIndex
        {
            get { return loanProportionIndex; }
            set { SetProperty(ref loanProportionIndex, value); }
        }

        private double totalLoan;    //组合贷款 贷款总额
        public double TotalLoan
        {
            get { return totalLoan; }
            set { SetProperty(ref totalLoan, value); }
        }

        private double totalInterest;    //组合贷款 总利息
        public double TotalInterest
        {
            get { return totalInterest; }
            set { SetProperty(ref totalInterest, value); }
        }

        private double pFTotalInterest;    //公积金 利息
        public double PFTotalInterest
        {
            get { return pFTotalInterest; }
            set { SetProperty(ref pFTotalInterest, value); }
        }

        private double cLTotalInterest;    //商业贷款 利息
        public double CLTotalInterest
        {
            get { return cLTotalInterest; }
            set { SetProperty(ref cLTotalInterest, value); }
        }

        private double monthlyRepayment;    //组合贷款 总月均还款
        public double MonthlyRepayment
        {
            get { return monthlyRepayment; }
            set { SetProperty(ref monthlyRepayment, value); }
        }

        private double pFMonthlyRepayment;    //公积金 月均还款
        public double PFMonthlyRepayment
        {
            get { return pFMonthlyRepayment; }
            set { SetProperty(ref pFMonthlyRepayment, value); }
        }

        private double cLMonthlyRepayment;    //商业贷款 月均还款
        public double CLMonthlyRepayment
        {
            get { return cLMonthlyRepayment; }
            set { SetProperty(ref cLMonthlyRepayment, value); }
        }

        #region 选择器
        private List<string> cLoanProportionList;   //组合 贷款比例列表
        public List<string> CLoanProportionList
        {
            get { return cLoanProportionList; }
            set { SetProperty(ref cLoanProportionList, value); }
        }

        private List<string> cPFYearList;   //组合 公积金按揭年数列表
        public List<string> CPFYearList
        {
            get { return cPFYearList; }
            set { SetProperty(ref cPFYearList, value); }
        }

        private List<string> cCLYearList;   //组合 商贷按揭年数列表
        public List<string> CCLYearList
        {
            get { return cCLYearList; }
            set { SetProperty(ref cCLYearList, value); }
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

        private double pFTotalLoan;           //公积金 贷款总额
        private double cLTotalLoan;           //商贷 贷款总额

        public Command ClearCommand { get; set; }
        public Command CalculateCommand { get; set; }

        public CombinedLoanViewModel()
        {
            InitPickers();

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
            CLoanProportionList = new List<string>();
            CPFYearList = new List<string>();
            CCLYearList = new List<string>();

            for (int i = 1; i <= 30; i++)
            {
                CPFYearList.Add(i + "年（" + i * 12 + "期）");
                CCLYearList.Add(i + "年（" + i * 12 + "期）");
            }

            for (int j = 4; j <= 16; j++)
            {
                CLoanProportionList.Add(j * 5 + "%");
            }
        }


        /// <summary>
        /// 计算
        /// </summary>
        private void Calculate()
        {
            double PFMonthlyIntRate = (double.Parse(PFRate) / 100.0) / 12;   //公积金 月利率
            int PFmonths = (PFYearIndex + 1) * 12;    //公积金 还款月数
            pFTotalLoan = double.Parse(PFLoanAmount);   //公积金 贷款总额

            double CLMonthlyIntRate = (double.Parse(CLRate) / 100.0) / 12;   //商贷 月利率
            int CLmonths = (CLYearIndex + 1) * 12;    //商贷 还款月数
            cLTotalLoan = double.Parse(CLLoanAmount);   //商贷 贷款总额

            if (TotalPriceRadio && !TotalLoanRadio)
            {
                double proportion = (LoanProportionIndex + 4) / 20.0;
                TotalLoan = double.Parse(TotalPrice) * proportion;
                //TotalLoan = DecimalFormatting(TotalLoan.ToString());    //贷款总额

                if ((pFTotalLoan + cLTotalLoan - TotalLoan) != 0.0)
                {
                    CrossToastPopUp.Current.ShowToastError("公积金贷款金额与商贷金额之和，应等于贷款总额，请重新填写！", ToastLength.Long);
                    return;
                }
            }
            else if (!TotalPriceRadio && TotalLoanRadio)
            {
                TotalLoan = double.Parse(PFLoanAmount) + double.Parse(CLLoanAmount);
                //TotalLoan = DecimalFormatting(TotalLoan.ToString());    //贷款总额
            }

            if (AveCapAndIntRadio)
            {
                //公积金 等额本息 总利息
                PFTotalInterest = ((pFTotalLoan * PFMonthlyIntRate * Math.Pow((1 + PFMonthlyIntRate), PFmonths)) / (Math.Pow((1 + PFMonthlyIntRate), PFmonths) - 1)) * PFmonths - pFTotalLoan;
                //PFTotalInterest = DecimalFormatting(pFTotalInterest.ToString());
                //商贷 等额本息 总利息
                CLTotalInterest = cLTotalLoan * ((CLmonths * CLMonthlyIntRate - 1) * Math.Pow((1 + CLMonthlyIntRate), CLmonths) + 1) / (Math.Pow((1 + CLMonthlyIntRate), CLmonths) - 1);
                //CLTotalInterest = DecimalFormatting(cLTotalInterest.ToString());
                //组合贷款 等额本息 总利息
                TotalInterest = pFTotalInterest + cLTotalInterest;
                //TotalInterest = DecimalFormatting(TotalInterest.ToString());

                //公积金 等额本息 月均还款
                PFMonthlyRepayment = (pFTotalLoan + pFTotalInterest) / PFmonths;
                //PFMonthlyRepayment = DecimalFormatting(pFMonthlyRepayment.ToString());
                //商贷 等额本息 月均还款
                CLMonthlyRepayment = (cLTotalLoan + cLTotalInterest) / PFmonths;
                //CLMonthlyRepayment = DecimalFormatting(cLMonthlyRepayment.ToString());
                //组合贷款 等额本息 月均还款
                MonthlyRepayment = pFMonthlyRepayment + cLMonthlyRepayment;
                //MonthlyRepayment = DecimalFormatting(MonthlyRepayment.ToString());

            }
            else
            {
                //公积金 等额本金 总利息
                PFTotalInterest = PFmonths * ((pFTotalLoan * PFMonthlyIntRate) - (PFMonthlyIntRate * (pFTotalLoan / PFmonths) * (PFmonths - 1)) / 2 + (pFTotalLoan / PFmonths)) - pFTotalLoan;
                //PFTotalInterest = DecimalFormatting(pFTotalInterest.ToString());
                //商贷 等额本金 总利息
                CLTotalInterest = (cLTotalLoan * CLMonthlyIntRate * (CLmonths + 1) / 2);
                //CLTotalInterest = DecimalFormatting(cLTotalInterest.ToString());
                //组合贷款 等额本金 总利息
                TotalInterest = pFTotalInterest + cLTotalInterest;
                //TotalInterest = DecimalFormatting(TotalInterest.ToString());

                //公积金 等额本金 月均还款
                PFMonthlyRepayment = (pFTotalLoan + pFTotalInterest) / PFmonths;
                //PFMonthlyRepayment = DecimalFormatting(pFMonthlyRepayment.ToString());
                //商贷 等额本金 月均还款
                CLMonthlyRepayment = (cLTotalLoan + cLTotalInterest) / CLmonths;
                //CLMonthlyRepayment = DecimalFormatting(cLMonthlyRepayment.ToString());
                //组合贷款 等额本金 月均还款
                MonthlyRepayment = pFMonthlyRepayment + cLMonthlyRepayment;
                //MonthlyRepayment = DecimalFormatting(MonthlyRepayment.ToString());
            }

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
                if (TotalPrice == null || TotalPrice == "")
                {
                    CrossToastPopUp.Current.ShowToastError("请输入房价总额！", ToastLength.Long);
                    return false;
                }
                else if (!Regex.IsMatch(TotalPrice, regexString))
                {
                    CrossToastPopUp.Current.ShowToastError("房价总额格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                    return false;
                }
                //else if (LoanProportion == null || LoanProportion == "")
                //{
                //    CrossToastPopUp.Current.ShowToastError("请选择贷款比例！", ToastLength.Long);
                //    return false;
                //}
            }

            if (PFLoanAmount == null || PFLoanAmount == "")
            {
                CrossToastPopUp.Current.ShowToastError("请输入公积金贷款金额！", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(PFLoanAmount, regexString))
            {
                CrossToastPopUp.Current.ShowToastError("公积金贷款金额格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                return false;
            }

            //if (PFYear == null || PFYear == "")
            //{
            //    CrossToastPopUp.Current.ShowToastError("请选择公积金按揭年数！", ToastLength.Long);
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

            if (CLLoanAmount == null || CLLoanAmount == "")
            {
                CrossToastPopUp.Current.ShowToastError("请输入商贷贷款金额！", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(CLLoanAmount, regexString))
            {
                CrossToastPopUp.Current.ShowToastError("商贷贷款金额格式不对，只能是正浮点数，请重新填写！", ToastLength.Long);
                return false;
            }

            //if (CLYear == null || CLYear == "")
            //{
            //    CrossToastPopUp.Current.ShowToastError("请选择商贷按揭年数！", ToastLength.Long);
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
