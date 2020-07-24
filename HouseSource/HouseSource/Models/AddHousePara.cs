using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HouseSource.Models
{
    public class AddHousePara
    {
        [JsonProperty("DBName", NullValueHandling = NullValueHandling.Ignore)]
        public string DBName { get; set; }   //Comment

        [JsonProperty("CityName", NullValueHandling = NullValueHandling.Ignore)]
        public string CityName { get; set; }   //Comment

        [JsonProperty("DistrictName", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictName { get; set; }   //Comment

        [JsonProperty("EstateID", NullValueHandling = NullValueHandling.Ignore)]
        public string EstateID { get; set; }   //Comment

        [JsonProperty("RoomNo", NullValueHandling = NullValueHandling.Ignore)]
        public string RoomNo { get; set; }   //Comment

        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }   //Comment

        [JsonProperty("Floor", NullValueHandling = NullValueHandling.Ignore)]
        public string Floor { get; set; }   //Comment

        [JsonProperty("Trade", NullValueHandling = NullValueHandling.Ignore)]
        public string Trade { get; set; }   //Comment

        [JsonProperty("CountF", NullValueHandling = NullValueHandling.Ignore)]
        public string CountF { get; set; }   //Comment

        [JsonProperty("CountW", NullValueHandling = NullValueHandling.Ignore)]
        public string CountW { get; set; }   //Comment

        [JsonProperty("CountT", NullValueHandling = NullValueHandling.Ignore)]
        public string CountT { get; set; }   //Comment

        [JsonProperty("CountY", NullValueHandling = NullValueHandling.Ignore)]
        public string CountY { get; set; }   //Comment

        [JsonProperty("PropertyUsage", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyUsage { get; set; }   //Comment

        [JsonProperty("PropertyType", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyType { get; set; }   //Comment

        [JsonProperty("PropertyDirection", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyDirection { get; set; }   //Comment

        [JsonProperty("PropertySource", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertySource { get; set; }   //Comment

        [JsonProperty("PropertyRight", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyRight { get; set; }   //Comment

        [JsonProperty("Square", NullValueHandling = NullValueHandling.Ignore)]
        public string Square { get; set; }   //Comment

        [JsonProperty("PriceUnit", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceUnit { get; set; }   //Comment

        [JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }   //Comment

        [JsonProperty("PropertyDecoration", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyDecoration { get; set; }   //Comment

        [JsonProperty("OwnerName", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerName { get; set; }   //Comment

        [JsonProperty("BuildNo", NullValueHandling = NullValueHandling.Ignore)]
        public string BuildNo { get; set; }   //Comment

        [JsonProperty("FloorAll", NullValueHandling = NullValueHandling.Ignore)]
        public string FloorAll { get; set; }   //Comment

        [JsonProperty("OwnerMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerMobile { get; set; }   //Comment

        [JsonProperty("EmpNoOrTel", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpNoOrTel { get; set; }   //Comment

        [JsonProperty("PropertyOwn", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyOwn { get; set; }   //Comment

        [JsonProperty("PropertyCertificate", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyCertificate { get; set; }   //Comment

        [JsonProperty("PropertyOccupy", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyOccupy { get; set; }   //Comment

        [JsonProperty("PropertyCommission", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyCommission { get; set; }   //Comment

        [JsonProperty("PropertyBuy", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyBuy { get; set; }   //Comment

        [JsonProperty("ZhuZhaiPropertyFurniture", NullValueHandling = NullValueHandling.Ignore)]
        public string ZhuZhaiPropertyFurniture { get; set; }   //Comment

        [JsonProperty("HandOverDate", NullValueHandling = NullValueHandling.Ignore)]
        public string HandOverDate { get; set; }   //Comment

        [JsonProperty("HangDate", NullValueHandling = NullValueHandling.Ignore)]
        public string HangDate { get; set; }   //Comment

        [JsonProperty("CompleteYear", NullValueHandling = NullValueHandling.Ignore)]
        public string CompleteYear { get; set; }   //Comment

        [JsonProperty("PropertyLook", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyLook { get; set; }   //Comment

        [JsonProperty("KeyNo", NullValueHandling = NullValueHandling.Ignore)]
        public string KeyNo { get; set; }   //Comment

        [JsonProperty("Remark", NullValueHandling = NullValueHandling.Ignore)]
        public string Remark { get; set; }   //Comment

        [JsonProperty("PropertyIntroduce", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyIntroduce { get; set; }   //Comment

        [JsonProperty("OwnerIntroduce", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerIntroduce { get; set; }   //Comment

        [JsonProperty("ServiceIntroduce", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceIntroduce { get; set; }   //Comment

        [JsonProperty("FlagWeb", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagWeb { get; set; }   //Comment

        [JsonProperty("OwnerMobile2", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerMobile2 { get; set; }   //Comment

        [JsonProperty("OwnerMobile3", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerMobile3 { get; set; }   //Comment

        [JsonProperty("FlagMWWY", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagMWWY { get; set; }   //Comment

        [JsonProperty("FlagWDY", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagWDY { get; set; }   //Comment

        [JsonProperty("FlagKDK", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagKDK { get; set; }   //Comment

        [JsonProperty("FlagXSFY", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagXSFY { get; set; }   //Comment

        [JsonProperty("PropertyID", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyID { get; set; }   //Comment

        [JsonProperty("ChangeLog", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeLog { get; set; }   //Comment

        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }   //Comment

        [JsonProperty("photos", NullValueHandling = NullValueHandling.Ignore)]
        public string photos { get; set; }   //Comment

    }
}
