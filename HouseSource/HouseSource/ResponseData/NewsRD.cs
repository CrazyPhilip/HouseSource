using Newtonsoft.Json;
using HouseSource.Models;

namespace HouseSource.ResponseData
{
    public class NewsRD
    {
        [JsonProperty("Total", NullValueHandling = NullValueHandling.Ignore)]
        public string Total { get; set; }    //总数

        [JsonProperty("News", NullValueHandling = NullValueHandling.Ignore)]
        public NewsInfo[] News { get; set; }    //总数
    }
}
