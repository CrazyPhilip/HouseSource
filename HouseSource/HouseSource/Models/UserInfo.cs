using Newtonsoft.Json;

namespace HouseSource.Models
{
    public class UserInfo
    {
        [JsonProperty("EmpID", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpID { get; set; }   //Comment

        [JsonProperty("EmpName", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpName { get; set; }   //Comment

        [JsonProperty("PhotoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PhotoUrl { get; set; }   //Comment

        [JsonProperty("AccountStyle", NullValueHandling = NullValueHandling.Ignore)]
        public string AccountStyle { get; set; }   //Comment

        [JsonProperty("CompanyOrEstateName", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyOrEstateName { get; set; }   //Comment

        [JsonIgnore]
        public string EmpNo { get; set; }   //Comment

    }
}
