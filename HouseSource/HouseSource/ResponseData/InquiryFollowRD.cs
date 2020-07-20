using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;

namespace HouseSource.ResponseData
{
    public class InquiryFollowRD
    {
        [JsonProperty("Msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }   //Comment

        [JsonProperty("InquiryFollowInfo", NullValueHandling = NullValueHandling.Ignore)]
        public InquiryFollowItemInfo[] InquiryFollowInfo { get; set; }   //Comment

    }
}
