using Newtonsoft.Json;

namespace HouseSource.Models
{
    public class AreaInfo
    {
        [JsonProperty("AreaID", NullValueHandling = NullValueHandling.Ignore)]
        public string AreaID { get; set; }

        [JsonProperty("AreaName", NullValueHandling = NullValueHandling.Ignore)]
        public string AreaName { get; set; }
    }
}
