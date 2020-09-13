using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class District
    {
        [JsonProperty("town", NullValueHandling = NullValueHandling.Ignore)]
        public string town { get; set; }   //Comment
    }
}
