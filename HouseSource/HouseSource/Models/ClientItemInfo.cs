using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class ClientItemInfo
    {
        
        [JsonProperty("InquiryID", NullValueHandling = NullValueHandling.Ignore)]
        public string InquiryID { get; set; }

        [JsonProperty("InquiryNo", NullValueHandling = NullValueHandling.Ignore)]
        public string InquiryNo { get; set; }

        [JsonProperty("CustName", NullValueHandling = NullValueHandling.Ignore)]
        public string CustName { get; set; }   //客户名字

        [JsonProperty("CustMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string CustMobile { get; set; }

        [JsonProperty("EmpID", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpID { get; set; }

        [JsonProperty("RegPerson", NullValueHandling = NullValueHandling.Ignore)]
        public string RegPerson { get; set; }

        [JsonProperty("Position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty("Remark", NullValueHandling = NullValueHandling.Ignore)]
        public string Remark { get; set; }

        [JsonProperty("CountF", NullValueHandling = NullValueHandling.Ignore)]
        public string CountF { get; set; }

        [JsonProperty("CountT", NullValueHandling = NullValueHandling.Ignore)]
        public string CountT { get; set; }

        [JsonProperty("CountW", NullValueHandling = NullValueHandling.Ignore)]
        public string CountW { get; set; }

        [JsonProperty("CountY", NullValueHandling = NullValueHandling.Ignore)]
        public string CountY { get; set; }

        [JsonProperty("Trade", NullValueHandling = NullValueHandling.Ignore)]
        public string Trade { get; set; }

        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("PropertyUsage", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyUsage { get; set; }

        [JsonProperty("PropertyType", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyType { get; set; }

        [JsonProperty("PropertyCommission", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyCommission { get; set; }

        [JsonProperty("SquareMin", NullValueHandling = NullValueHandling.Ignore)]
        public string SquareMin { get; set; }

        [JsonProperty("SquareMax", NullValueHandling = NullValueHandling.Ignore)]
        public string SquareMax { get; set; }

        [JsonProperty("PriceMin", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceMin { get; set; }

        [JsonProperty("PriceMax", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceMax { get; set; }

        [JsonProperty("InquirySource", NullValueHandling = NullValueHandling.Ignore)]
        public string InquirySource { get; set; }

        [JsonProperty("UnitName", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitName { get; set; }

        [JsonProperty("CustMobile1", NullValueHandling = NullValueHandling.Ignore)]
        public string CustMobile1 { get; set; }
    }
}
