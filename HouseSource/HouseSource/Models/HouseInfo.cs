using Newtonsoft.Json;

namespace HouseSource.Models
{
    public class HouseInfo
    {
        //用于存放服务器传回的房屋信息

        [JsonProperty("EmpID", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpID { get; set; }    //员工ID

        [JsonProperty("PropertyID", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyID { get; set; }    //

        [JsonProperty("BuildNo", NullValueHandling = NullValueHandling.Ignore)]
        public string BuildNo { get; set; }    //栋座

        [JsonProperty("RoomNo", NullValueHandling = NullValueHandling.Ignore)]
        public string RoomNo { get; set; }    //房号

        [JsonProperty("Remark", NullValueHandling = NullValueHandling.Ignore)]
        public string Remark { get; set; }    //

        [JsonProperty("UnitName", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitName { get; set; }    //售价价格单位

        [JsonProperty("RentUnitName", NullValueHandling = NullValueHandling.Ignore)]
        public string RentUnitName { get; set; }    //租金价格单位

        [JsonProperty("Floor", NullValueHandling = NullValueHandling.Ignore)]
        public string Floor { get; set; }    //楼层

        [JsonProperty("EstateName", NullValueHandling = NullValueHandling.Ignore)]
        public string EstateName { get; set; }    //楼盘

        [JsonProperty("PropertyDirection", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyDirection { get; set; }    //朝向

        [JsonProperty("PropertyDecoration", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyDecoration { get; set; }    //装修

        [JsonProperty("CityName", NullValueHandling = NullValueHandling.Ignore)]
        public string CityName { get; set; }    //城市

        [JsonProperty("DistrictName", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictName { get; set; }    //区县

        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }    //状态

        [JsonProperty("PropertyLook", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyLook { get; set; }    //

        [JsonProperty("Grade", NullValueHandling = NullValueHandling.Ignore)]
        public string Grade { get; set; }    //

        [JsonProperty("PropertyType", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyType { get; set; }    //类型

        [JsonProperty("PropertySource", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertySource { get; set; }    //

        [JsonProperty("RegPerson", NullValueHandling = NullValueHandling.Ignore)]
        public string RegPerson { get; set; }    //员工职位？

        [JsonProperty("DeptName", NullValueHandling = NullValueHandling.Ignore)]
        public string DeptName { get; set; }    //部门

        [JsonProperty("EmpName", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpName { get; set; }    //员工名字

        [JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }    //总价

        [JsonProperty("RentPrice", NullValueHandling = NullValueHandling.Ignore)]
        public string RentPrice { get; set; }    //租金

        [JsonProperty("Square", NullValueHandling = NullValueHandling.Ignore)]
        public string Square { get; set; }    //面积

        [JsonProperty("PropertyUsage", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyUsage { get; set; }    //用途

        [JsonProperty("Trade", NullValueHandling = NullValueHandling.Ignore)]
        public string Trade { get; set; }    //交易方式

        [JsonProperty("ownername", NullValueHandling = NullValueHandling.Ignore)]
        public string ownername { get; set; }    //所有人、户主名字

        [JsonProperty("ownermobile", NullValueHandling = NullValueHandling.Ignore)]
        public string ownermobile { get; set; }    //户主电话

        [JsonProperty("PhotoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PhotoUrl { get; set; }    //图片

        [JsonProperty("CountF", NullValueHandling = NullValueHandling.Ignore)]
        public string CountF { get; set; }    //室、房型

        [JsonProperty("CountT", NullValueHandling = NullValueHandling.Ignore)]
        public string CountT { get; set; }    //厅

        [JsonProperty("CountW", NullValueHandling = NullValueHandling.Ignore)]
        public string CountW { get; set; }    //卫

        [JsonProperty("TrustDate", NullValueHandling = NullValueHandling.Ignore)]
        public string TrustDate { get; set; }    //可信时间

        [JsonProperty("FlagWeb", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagWeb { get; set; }    //

        [JsonProperty("Privy", NullValueHandling = NullValueHandling.Ignore)]
        public string Privy { get; set; }    //

        [JsonProperty("AreaName", NullValueHandling = NullValueHandling.Ignore)]
        public string AreaName { get; set; }    //地区名

        [JsonProperty("ContactName", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactName { get; set; }    //

        [JsonProperty("OwnerTel", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerTel { get; set; }    //

        [JsonProperty("PropertyOwn", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyOwn { get; set; }    //产权

        [JsonProperty("PropertyCertificate", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyCertificate { get; set; }    //

        [JsonProperty("PropertyBuy", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyBuy { get; set; }    //

        [JsonProperty("PropertyCommission", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyCommission { get; set; }    //

        [JsonProperty("KeyNo", NullValueHandling = NullValueHandling.Ignore)]
        public string KeyNo { get; set; }    //

        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }    //

        [JsonProperty("tel", NullValueHandling = NullValueHandling.Ignore)]
        public string tel { get; set; }    //

        [JsonProperty("PropertyIntroduce", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyIntroduce { get; set; }   //Comment

        [JsonProperty("OwnerIntroduce", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerIntroduce { get; set; }   //Comment

        [JsonProperty("ServiceIntroduce", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceIntroduce { get; set; }   //Comment

    }
}
