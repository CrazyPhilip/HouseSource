using HouseSource.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.ResponseData
{
    public class AreaRD
    {
        [JsonProperty("Msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        [JsonProperty("AreaMsg", NullValueHandling = NullValueHandling.Ignore)]
        public AreaInfo[] AreaMsg { get; set; }

    }
}
