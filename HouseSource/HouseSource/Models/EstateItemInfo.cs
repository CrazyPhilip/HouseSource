using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class EstateItemInfo
    {
        [JsonProperty("EstateID", NullValueHandling = NullValueHandling.Ignore)]
        public string EstateID { get; set; }   //Comment

        [JsonProperty("EstateName", NullValueHandling = NullValueHandling.Ignore)]
        public string EstateName { get; set; }   //Comment

        [JsonProperty("CityName", NullValueHandling = NullValueHandling.Ignore)]
        public string CityName { get; set; }   //Comment

        [JsonProperty("DistrictName", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictName { get; set; }   //Comment

    }
}
