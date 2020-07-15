using Newtonsoft.Json;

namespace HouseSource.Models
{
    public class NewsInfo
    {
        [JsonProperty("NewsID", NullValueHandling = NullValueHandling.Ignore)]
        public string NewsID { get; set; }   //消息编号

        [JsonProperty("NewsType", NullValueHandling = NullValueHandling.Ignore)]
        public string NewsType { get; set; }   //类型

        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }   //标题

        [JsonProperty("Content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }   //内容

        [JsonProperty("ModDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ModDate { get; set; }   //发布时间

        [JsonProperty("EmpName", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpName { get; set; }   //发布人

        [JsonProperty("TitleColor2", NullValueHandling = NullValueHandling.Ignore)]
        public string TitleColor2 { get; set; }   //标题颜色

        [JsonProperty("FlagTop", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagTop { get; set; }   //是否置顶

    }
}
