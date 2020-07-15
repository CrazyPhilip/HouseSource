using Newtonsoft.Json;
using HouseSource.Models;

namespace HouseSource.ResponseData
{
    public class HouseRD
    {
        [JsonProperty("PromptMsg", NullValueHandling = NullValueHandling.Ignore)]
        public string PromptMsg { get; set; }    //

        [JsonProperty("Buildings", NullValueHandling = NullValueHandling.Ignore)]
        public HouseInfo[] Buildings { get; set; }    //
    }
}
