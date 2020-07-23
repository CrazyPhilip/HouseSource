using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class BuildingInfo
    {
        [JsonProperty("BuildingID", NullValueHandling = NullValueHandling.Ignore)]
        public string BuildingID { get; set; }   //Comment

        [JsonProperty("BuildingName", NullValueHandling = NullValueHandling.Ignore)]
        public string BuildingName { get; set; }   //Comment


    }
}
