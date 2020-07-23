using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;
using Newtonsoft.Json;

namespace HouseSource.ResponseData
{
    public class BuildingRD
    {
        [JsonProperty("Msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }   //Comment

        [JsonProperty("Dongzuo", NullValueHandling = NullValueHandling.Ignore)]
        public BuildingInfo[] Dongzuo { get; set; }   //Comment

    }
}
