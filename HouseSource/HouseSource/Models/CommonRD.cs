using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class CommonRD
    {
        [JsonProperty("Msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }    //总数
    }
}
