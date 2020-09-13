using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class HouseInfo2
    {
        //用于存放服务器传回的房屋信息

        [JsonProperty("proId", NullValueHandling = NullValueHandling.Ignore)]
        public long proId { get; set; }    //

        [JsonProperty("proEstId", NullValueHandling = NullValueHandling.Ignore)]
        public long proEstId { get; set; }    //

        [JsonProperty("proAreId", NullValueHandling = NullValueHandling.Ignore)]
        public long proAreId { get; set; }    //

        [JsonProperty("proInnernetId", NullValueHandling = NullValueHandling.Ignore)]
        public string proInnernetId { get; set; }    //

        [JsonProperty("proNo", NullValueHandling = NullValueHandling.Ignore)]
        public string proNo { get; set; }    //

        [JsonProperty("proCity", NullValueHandling = NullValueHandling.Ignore)]
        public string proCity { get; set; }    //

        [JsonProperty("proCityId", NullValueHandling = NullValueHandling.Ignore)]
        public long proCityId { get; set; }    //

        [JsonProperty("proDistrict", NullValueHandling = NullValueHandling.Ignore)]
        public string proDistrict { get; set; }    //

        [JsonProperty("proDistrictId", NullValueHandling = NullValueHandling.Ignore)]
        public long proDistrictId { get; set; }    //

        [JsonProperty("proArea", NullValueHandling = NullValueHandling.Ignore)]
        public string proArea { get; set; }    //

        [JsonProperty("proAreaId", NullValueHandling = NullValueHandling.Ignore)]
        public long proAreaId { get; set; }    //

        [JsonProperty("proAddr", NullValueHandling = NullValueHandling.Ignore)]
        public string proAddr { get; set; }    //

        [JsonProperty("proLat", NullValueHandling = NullValueHandling.Ignore)]
        public double proLat { get; set; }    //

        [JsonProperty("proLng", NullValueHandling = NullValueHandling.Ignore)]
        public double proLng { get; set; }    //

        [JsonProperty("proEstateName", NullValueHandling = NullValueHandling.Ignore)]
        public string proEstateName { get; set; }    //

        [JsonProperty("proTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string proTitle { get; set; }    //

        [JsonProperty("proKeywords", NullValueHandling = NullValueHandling.Ignore)]
        public string proKeywords { get; set; }    //

        [JsonProperty("proSquare", NullValueHandling = NullValueHandling.Ignore)]
        public long proSquare { get; set; }    //

        [JsonProperty("proPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long proPrice { get; set; }    //

        [JsonProperty("proPriceType", NullValueHandling = NullValueHandling.Ignore)]
        public string proPriceType { get; set; }    //

        [JsonProperty("proUnitPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long proUnitPrice { get; set; }    //

        [JsonProperty("proUnitPriceType", NullValueHandling = NullValueHandling.Ignore)]
        public string proUnitPriceType { get; set; }    //

        [JsonProperty("proRentPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long proRentPrice { get; set; }    //

        [JsonProperty("proRentPriceType", NullValueHandling = NullValueHandling.Ignore)]
        public string SqproRentPriceTypeuare { get; set; }    //

        [JsonProperty("proFirstPay", NullValueHandling = NullValueHandling.Ignore)]
        public string proFirstPay { get; set; }    //

        [JsonProperty("proMonthPay", NullValueHandling = NullValueHandling.Ignore)]
        public string proMonthPay { get; set; }    //

        [JsonProperty("proCountF", NullValueHandling = NullValueHandling.Ignore)]
        public int proCountF { get; set; }    //

        [JsonProperty("proCountT", NullValueHandling = NullValueHandling.Ignore)]
        public int proCountT { get; set; }    //

        [JsonProperty("proCountW", NullValueHandling = NullValueHandling.Ignore)]
        public int proCountW { get; set; }    //

        [JsonProperty("proCountY", NullValueHandling = NullValueHandling.Ignore)]
        public int proCountY { get; set; }    //

        [JsonProperty("proDirection", NullValueHandling = NullValueHandling.Ignore)]
        public string proDirection { get; set; }    //

        [JsonProperty("proFloor", NullValueHandling = NullValueHandling.Ignore)]
        public string proFloor { get; set; }    //

        [JsonProperty("proFloorAll", NullValueHandling = NullValueHandling.Ignore)]
        public int proFloorAll { get; set; }    //

        [JsonProperty("proType", NullValueHandling = NullValueHandling.Ignore)]
        public string proType { get; set; }    //

        [JsonProperty("proTrade", NullValueHandling = NullValueHandling.Ignore)]
        public string proTrade { get; set; }    //

        [JsonProperty("proKey", NullValueHandling = NullValueHandling.Ignore)]
        public bool proKey { get; set; }    //

        [JsonProperty("proUsage", NullValueHandling = NullValueHandling.Ignore)]
        public string proUsage { get; set; }    //

        [JsonProperty("proDecoration", NullValueHandling = NullValueHandling.Ignore)]
        public string proDecoration { get; set; }    //

        [JsonProperty("proCoverUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string proCoverUrl { get; set; }    //

        [JsonProperty("proDbName", NullValueHandling = NullValueHandling.Ignore)]
        public string proDbName { get; set; }    //

        [JsonProperty("proEmployee1Id", NullValueHandling = NullValueHandling.Ignore)]
        public string proEmployee1Id { get; set; }    //

        [JsonProperty("proEmployee1Name", NullValueHandling = NullValueHandling.Ignore)]
        public string proEmployee1Name { get; set; }    //

        [JsonProperty("proEmployee1Img", NullValueHandling = NullValueHandling.Ignore)]
        public string proEmployee1Img { get; set; }    //

        [JsonProperty("proEmployee1Phone", NullValueHandling = NullValueHandling.Ignore)]
        public string proEmployee1Phone { get; set; }    //

        [JsonProperty("proLooknum", NullValueHandling = NullValueHandling.Ignore)]
        public long proLooknum { get; set; }    //

        [JsonProperty("proCompleteYear", NullValueHandling = NullValueHandling.Ignore)]
        public long proCompleteYear { get; set; }    //

        [JsonProperty("proModDate", NullValueHandling = NullValueHandling.Ignore)]
        public string proModDate { get; set; }    //

        [JsonProperty("proDate", NullValueHandling = NullValueHandling.Ignore)]
        public string proDate { get; set; }    //

        [JsonProperty("proEavgPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double proEavgPrice { get; set; }   //Comment

        [JsonProperty("proCompanyName", NullValueHandling = NullValueHandling.Ignore)]
        public string proCompanyName { get; set; }   //Comment

        [JsonProperty("proCollectCount", NullValueHandling = NullValueHandling.Ignore)]
        public long proCollectCount { get; set; }   //Comment

        [JsonProperty("proLadderRatio", NullValueHandling = NullValueHandling.Ignore)]
        public string proLadderRatio { get; set; }   //Comment

        [JsonProperty("proOwn", NullValueHandling = NullValueHandling.Ignore)]
        public string proOwn { get; set; }   //Comment

        [JsonProperty("proOwnership", NullValueHandling = NullValueHandling.Ignore)]
        public string proOwnership { get; set; }   //Comment

        [JsonProperty("proMortgate", NullValueHandling = NullValueHandling.Ignore)]
        public string proMortgate { get; set; }   //Comment

        [JsonProperty("proElevator", NullValueHandling = NullValueHandling.Ignore)]
        public string proElevator { get; set; }   //Comment

        [JsonProperty("proHouseCheck", NullValueHandling = NullValueHandling.Ignore)]
        public string proHouseCheck { get; set; }   //Comment

        [JsonProperty("proRightYears", NullValueHandling = NullValueHandling.Ignore)]
        public int proRightYears { get; set; }   //Comment

        [JsonProperty("proScore", NullValueHandling = NullValueHandling.Ignore)]
        public int proScore { get; set; }   //Comment

        [JsonProperty("proDataId", NullValueHandling = NullValueHandling.Ignore)]
        public string proDataId { get; set; }   //Comment

        [JsonProperty("proCreateDate", NullValueHandling = NullValueHandling.Ignore)]
        public string proCreateDate { get; set; }   //Comment

        [JsonProperty("proStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string proStatus { get; set; }   //Comment

        [JsonProperty("proPhotoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string proPhotoUrl { get; set; }   //Comment

        [JsonProperty("proDetail", NullValueHandling = NullValueHandling.Ignore)]
        public string proDetail { get; set; }   //Comment


    }
}
