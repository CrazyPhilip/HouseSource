namespace HouseSource.Models
{
    public class AddClientPara
    {
        public string DBName { get; set; }     //数据库名

        public string CustName { get; set; }     //姓名

        public string CustMobile { get; set; }     //电话

        public string Trade { get; set; }   //交易

        public string Country { get; set; }   //户籍

        public string PropertyFloor { get; set; }   //婚姻  原JS代码中是这样写的

        public string PropertyUsage { get; set; }   //用途

        public string CustGrade { get; set; }   //等级

        public string PropertyDecoration { get; set; }   //装修

        public string PropertyType { get; set; }   //房屋类型

        public string PriceMin { get; set; }     //最低预算

        public string PriceMax { get; set; }     //最高预算

        public string SquareMin { get; set; }     //最小面积

        public string SquareMax { get; set; }     //最大面积

        public string FoorStart { get; set; }     //最低楼层

        public string FoorEnd { get; set; }     //最高楼层

        public string CountFStart { get; set; }     //最小房数

        public string CountF { get; set; }     //最大房数

        public string DistrictName { get; set; }   //城区

        public string AreaID { get; set; }   //片区、街区

        public string EmpNoOrTel { get; set; }    //管理员
    }
}
