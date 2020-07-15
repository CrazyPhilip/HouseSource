using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class HousePara
    {
        //用于网络传参
        
        [JsonProperty("districtName", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictName { get; set; }    //区名

        [JsonProperty("countF", NullValueHandling = NullValueHandling.Ignore)]
        public string CountF { get; set; }    //房型

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }    //总价

        [JsonProperty("square", NullValueHandling = NullValueHandling.Ignore)]
        public string Square { get; set; }    //区域

        [JsonProperty("propertyUsage", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyUsage { get; set; }    //用途

        [JsonProperty("estateName", NullValueHandling = NullValueHandling.Ignore)]
        public string EstateName { get; set; }    //楼盘名

        [JsonProperty("buildNo", NullValueHandling = NullValueHandling.Ignore)]
        public string BuildNo { get; set; }    //栋座

        [JsonProperty("roomNo", NullValueHandling = NullValueHandling.Ignore)]
        public string RoomNo { get; set; }    //房号

        [JsonProperty("panType", NullValueHandling = NullValueHandling.Ignore)]
        public string PanType { get; set; }    //盘类型

        [JsonProperty("floor", NullValueHandling = NullValueHandling.Ignore)]
        public string Floor { get; set; }    //楼层

        public string MaxPrice { get; set; }    //楼层

        public string MinPrice { get; set; }    //楼层

    }
}
