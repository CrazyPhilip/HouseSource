using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using HouseSource.Models;

namespace HouseSource.ResponseData
{
    public class EstateRD
    {
        [JsonProperty("Result", NullValueHandling = NullValueHandling.Ignore)]
        public string Result { get; set; }   //Comment

        [JsonProperty("Info", NullValueHandling = NullValueHandling.Ignore)]
        public EstateItemInfo[] Info { get; set; }   //Comment

    }
}
