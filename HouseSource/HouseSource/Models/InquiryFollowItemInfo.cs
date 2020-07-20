using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class InquiryFollowItemInfo
    {
        [JsonProperty("FollowDate", NullValueHandling = NullValueHandling.Ignore)]
        public string FollowDate { get; set; }   //Comment

        [JsonProperty("Content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }   //Comment

        [JsonProperty("FollowType", NullValueHandling = NullValueHandling.Ignore)]
        public string FollowType { get; set; }   //Comment

        [JsonProperty("EmpName", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpName { get; set; }   //Comment

        [JsonProperty("DeptName", NullValueHandling = NullValueHandling.Ignore)]
        public string DeptName { get; set; }   //Comment

    }
}
