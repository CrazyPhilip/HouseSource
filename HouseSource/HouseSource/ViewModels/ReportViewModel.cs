using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Services;
using HouseSource.Models;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace HouseSource.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private ReportInfo todayReport;   //本日
        public ReportInfo TodayReport
        {
            get { return todayReport; }
            set { SetProperty(ref todayReport, value); }
        }

        private ReportInfo weekReport;   //本周
        public ReportInfo WeekReport
        {
            get { return weekReport; }
            set { SetProperty(ref weekReport, value); }
        }

        private ReportInfo monthReport;   //本月
        public ReportInfo MonthReport
        {
            get { return monthReport; }
            set { SetProperty(ref monthReport, value); }
        }

        public ReportViewModel()
        {
            InitReportPage();
        }

        private async void InitReportPage()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string todayAchievement = await RestSharpService.GetAchievement("today");
                string todayWorkLoad = await RestSharpService.GetWorkLoad("today");
                string todayWorkLoadRank = await RestSharpService.GetWorkLoadRank("today");

                string weekAchievement = await RestSharpService.GetAchievement("week");
                string weekWorkLoad = await RestSharpService.GetWorkLoad("week");
                string weekWorkLoadRank = await RestSharpService.GetWorkLoadRank("week");

                string monthAchievement = await RestSharpService.GetAchievement("month");
                string monthWorkLoad = await RestSharpService.GetWorkLoad("month");
                string monthWorkLoadRank = await RestSharpService.GetWorkLoadRank("month");

                ReportInfo reportInfo1 = new ReportInfo();
                ReportInfo reportInfo2 = new ReportInfo();
                ReportInfo reportInfo3 = new ReportInfo();
                if (!string.IsNullOrWhiteSpace(todayAchievement))
                {
                    JObject jObject = JObject.Parse(todayAchievement);
                    reportInfo1.receivableNum = jObject["Result"][0]["ys"].ToString();
                    reportInfo1.paidNum = jObject["Result"][0]["ss"].ToString();
                    reportInfo1.nrNum = (double.Parse(reportInfo1.receivableNum) - double.Parse(reportInfo1.paidNum)).ToString();
                    reportInfo1.companyRank = jObject["Result"][0]["rk"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(todayWorkLoad))
                {
                    JObject jObject = JObject.Parse(todayWorkLoad);
                    reportInfo1.ffzs = jObject["Result"][0]["ffzs"].ToString();
                    reportInfo1.kkzs = jObject["Result"][0]["kkzs"].ToString();
                    reportInfo1.fgj = jObject["Result"][0]["fgj"].ToString();
                    reportInfo1.kgj = jObject["Result"][0]["kgj"].ToString();
                    reportInfo1.ffcs3 = jObject["Result"][0]["ffcs3"].ToString();
                    reportInfo1.ffcs4 = jObject["Result"][0]["ffcs4"].ToString();
                    reportInfo1.ffcs = jObject["Result"][0]["ffcs"].ToString();
                    reportInfo1.ffcs2 = jObject["Result"][0]["ffcs2"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(todayWorkLoadRank))
                {
                    JObject jObject = JObject.Parse(todayWorkLoadRank);
                    reportInfo1.companyRank2 = jObject["Result"][0]["RowNum"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(weekAchievement))
                {
                    JObject jObject = JObject.Parse(todayAchievement);
                    reportInfo2.receivableNum = jObject["Result"][0]["ys"].ToString();
                    reportInfo2.paidNum = jObject["Result"][0]["ss"].ToString();
                    reportInfo2.nrNum = (double.Parse(reportInfo2.receivableNum) - double.Parse(reportInfo2.paidNum)).ToString();
                    reportInfo2.companyRank = jObject["Result"][0]["rk"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(weekWorkLoad))
                {
                    JObject jObject = JObject.Parse(todayWorkLoad);
                    reportInfo2.ffzs = jObject["Result"][0]["ffzs"].ToString();
                    reportInfo2.kkzs = jObject["Result"][0]["kkzs"].ToString();
                    reportInfo2.fgj = jObject["Result"][0]["fgj"].ToString();
                    reportInfo2.kgj = jObject["Result"][0]["kgj"].ToString();
                    reportInfo2.ffcs3 = jObject["Result"][0]["ffcs3"].ToString();
                    reportInfo2.ffcs4 = jObject["Result"][0]["ffcs4"].ToString();
                    reportInfo2.ffcs = jObject["Result"][0]["ffcs"].ToString();
                    reportInfo2.ffcs2 = jObject["Result"][0]["ffcs2"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(weekWorkLoadRank))
                {
                    JObject jObject = JObject.Parse(todayWorkLoadRank);
                    reportInfo2.companyRank2 = jObject["Result"][0]["RowNum"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(monthAchievement))
                {
                    JObject jObject = JObject.Parse(todayAchievement);
                    reportInfo3.receivableNum = jObject["Result"][0]["ys"].ToString();
                    reportInfo3.paidNum = jObject["Result"][0]["ss"].ToString();
                    reportInfo3.nrNum = (double.Parse(reportInfo3.receivableNum) - double.Parse(reportInfo3.paidNum)).ToString();
                    reportInfo3.companyRank = jObject["Result"][0]["rk"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(monthWorkLoad))
                {
                    JObject jObject = JObject.Parse(todayWorkLoad);
                    reportInfo3.ffzs = jObject["Result"][0]["ffzs"].ToString();
                    reportInfo3.kkzs = jObject["Result"][0]["kkzs"].ToString();
                    reportInfo3.fgj = jObject["Result"][0]["fgj"].ToString();
                    reportInfo3.kgj = jObject["Result"][0]["kgj"].ToString();
                    reportInfo3.ffcs3 = jObject["Result"][0]["ffcs3"].ToString();
                    reportInfo3.ffcs4 = jObject["Result"][0]["ffcs4"].ToString();
                    reportInfo3.ffcs = jObject["Result"][0]["ffcs"].ToString();
                    reportInfo3.ffcs2 = jObject["Result"][0]["ffcs2"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(monthWorkLoadRank))
                {
                    JObject jObject = JObject.Parse(todayWorkLoadRank);
                    reportInfo3.companyRank2 = jObject["Result"][0]["RowNum"].ToString();
                }

                TodayReport = reportInfo1;
                WeekReport = reportInfo2;
                MonthReport = reportInfo3;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
