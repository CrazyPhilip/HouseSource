using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.ResponseData
{
    public class BaseResponse
    {
        [JsonProperty("Flag", NullValueHandling = NullValueHandling.Ignore)]
        public string Flag { get; set; }   //Comment

        [JsonProperty("Msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }   //Comment

        [JsonProperty("Result", NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; set; }   //Comment

    }
}
