using Newtonsoft.Json;

namespace HouseSource.Models
{
    public class Area
    {
        [JsonProperty("areaName", NullValueHandling = NullValueHandling.Ignore)]
        public string areaName { get; set; }   //Comment

        [JsonProperty("dbName", NullValueHandling = NullValueHandling.Ignore)]
        public string dbName { get; set; }   //Comment

    }
}
