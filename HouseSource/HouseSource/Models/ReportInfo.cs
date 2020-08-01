using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Models
{
    public class ReportInfo
    {
        public string receivableNum { get; set; }//应收业绩

        public string paidNum { get; set; }//实收业绩

        public string nrNum { get; set; }//未收业绩

        public string companyRank { get; set; }//公司业绩排名

        public string ffzs { get; set; }//房增

        public string kkzs { get; set; }//客增

        public string fgj { get; set; }//房跟

        public string kgj { get; set; }//客跟

        public string ffcs3 { get; set; }//照片

        public string ffcs4 { get; set; }//钥匙

        public string ffcs { get; set; }//普通看房（空看）

        public string ffcs2 { get; set; }//带看

        public string companyRank2 { get; set; }//公司工作量排名

    }
}
