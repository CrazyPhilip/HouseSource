using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class HouseItemInfo
    {
        //将HouseInfo中的信息处理后存放于此，用于房源列表项的显示

        public string HouseTitle { get; set; }   //标题

        public string RoomStyle { get; set; }   //房型

        public string Square { get; set; }   //面积

        public string EstateName { get; set; }   //楼盘

        public string Price { get; set; }   //总价

        public string SinglePrice { get; set; }    //单价

        public string PhotoUrl { get; set; }  //图片

        public string PanType { get; set; }   //楼盘类型

        public string PropertyDecoration { get; set; }   //装修

        public string PropertyLook { get; set; }    //预约

        public string PropertyID { get; set; }    //房源编号
    }
}
