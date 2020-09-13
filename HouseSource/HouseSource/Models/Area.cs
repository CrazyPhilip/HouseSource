using Newtonsoft.Json;

namespace HouseSource.Models
{
    public class Area
    {
        [JsonProperty("village", NullValueHandling = NullValueHandling.Ignore)]
        public string village { get; set; }   //Comment

        [JsonProperty("dbName", NullValueHandling = NullValueHandling.Ignore)]
        public string dbName { get; set; }   //Comment

    }
}
