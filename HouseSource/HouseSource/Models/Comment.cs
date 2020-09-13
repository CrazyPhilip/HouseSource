using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class Comment
    {
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string content { get; set; }   //Comment

        [JsonProperty("commentPerson", NullValueHandling = NullValueHandling.Ignore)]
        public string commentPerson { get; set; }   //Comment

        [JsonProperty("commentPersonTel", NullValueHandling = NullValueHandling.Ignore)]
        public string commentPersonTel { get; set; }   //Comment

        [JsonProperty("photoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string photoUrl { get; set; }   //Comment

    }
}
