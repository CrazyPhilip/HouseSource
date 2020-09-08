using Newtonsoft.Json;

namespace HouseSource.ResponseData
{
    public class LoginRD
    {
        [JsonProperty("DBName", NullValueHandling = NullValueHandling.Ignore)]
        public string DBName { get; set; }   //Comment

        [JsonProperty("EmpID", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpID { get; set; }   //Comment

        [JsonProperty("EmpName", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpName { get; set; }   //Comment

        [JsonProperty("PhotoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PhotoUrl { get; set; }   //Comment

        [JsonProperty("AccountStyle", NullValueHandling = NullValueHandling.Ignore)]
        public string AccountStyle { get; set; }   //Comment

        //[JsonProperty("CompanyOrEstateName", NullValueHandling = NullValueHandling.Ignore)]
        //public string CompanyOrEstateName { get; set; }   //Comment

        //[JsonProperty("CityName", NullValueHandling = NullValueHandling.Ignore)]
        //public string CityName { get; set; }   //Comment

        //[JsonProperty("DistrictName", NullValueHandling = NullValueHandling.Ignore)]
        //public string DistrictName { get; set; }   //Comment

        //[JsonProperty("AreaName", NullValueHandling = NullValueHandling.Ignore)]
        //public string AreaName { get; set; }   //Comment

        //[JsonProperty("EstateName", NullValueHandling = NullValueHandling.Ignore)]
        //public string EstateName { get; set; }   //Comment

        //[JsonProperty("RealName", NullValueHandling = NullValueHandling.Ignore)]
        //public string RealName { get; set; }   //Comment

        //[JsonProperty("Position", NullValueHandling = NullValueHandling.Ignore)]
        //public string Position { get; set; }   //Comment

        [JsonIgnore]
        public string EmpNo { get; set; }   //Comment

    }
}
