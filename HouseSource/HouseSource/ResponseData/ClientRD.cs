using HouseSource.Models;
using Newtonsoft.Json;

namespace HouseSource.ResponseData
{
    public class ClientRD
    {
        [JsonProperty("Customers",NullValueHandling = NullValueHandling.Ignore)]
        public ClientItemInfo[] Customers { get; set; }
    }
}
