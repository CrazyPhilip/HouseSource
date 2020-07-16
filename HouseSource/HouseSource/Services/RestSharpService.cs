using HouseSource.Models;
using HouseSource.ResponseData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using HouseSource.Utils;

namespace HouseSource.Services
{
    public sealed class RestSharpService
    {
        private static readonly Lazy<RestSharpService> lazy = new Lazy<RestSharpService>(() => new RestSharpService());
        
        public static RestSharpService Instance { get { return lazy.Value; } }
        
        private RestSharpService() 
        { 
        
        }

        #region 会员注册登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="telOrEmpNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<string> Login(string dbName, string telOrEmpNo, string password)
        {
            try
            {
                string url = string.Format("Login?DBName={0}&TelOrEmpNo={1}&Password={2}", dbName, telOrEmpNo, password);

                string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
                return GetSubString(content, "{", "}");
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static async Task<string> SendCode(string tel)
        {
            string url = "GetTelCode?DBName=cd&Tel=" + tel;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return GetSubString(content, "{", "}");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="tel">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="name">真实姓名</param>
        /// <returns></returns>
        public static async Task<string> Register(string tel, string password, string name)
        {
            string url = "ApplyForRegister?DBName=cd&EmpNo=&Tel=" + tel
                + "&Password=" + password + "&EmpName=" + name + "&AccountStyle=独立经纪人&CompanyOrEstateName=";

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return GetSubString(content, "{", "}");
        }
        #endregion


        #region 房产数据

        /// <summary>
        /// 获取出售/出租房源信息
        /// </summary>
        /// <param name="DBName"></param>
        /// <param name="EmpNo"></param>
        /// <param name="DistrictName"></param>
        /// <param name="CountF"></param>
        /// <param name="Price"></param>
        /// <param name="Square"></param>
        /// <param name="PropertyUsage"></param>
        /// <param name="EstateName"></param>
        /// <param name="BuildNo"></param>
        /// <param name="RoomNo"></param>
        /// <param name="PanType"></param>
        /// <param name="Floor"></param>
        /// <returns></returns>
        public static async Task<HouseRD> GetHouseAsync(HousePara housePara, string type)
        {
            string url = "";
            if (type.Equals("出售"))
            {
                url = "/querySaleHouseSource";
            }
            else if (type.Equals("出租"))
            {
                url += "/queryRentHouseSource";
            }

            url = url + "?DBName=cd"
                + "&EmpNo=" + GlobalVariables.LoggedUser.EmpNo
                + "&DistrictName=" + housePara.DistrictName
                + "&CountF=" + housePara.CountF
                + "&Price=" + housePara.Price
                + "&Square=" + housePara.Square
                + "&PropertyUsage=" + housePara.PropertyUsage
                + "&EstateName=" + housePara.EstateName
                + "&BuildNo=" + housePara.BuildNo
                + "&RoomNo=" + housePara.RoomNo
                + "&PanType=" + housePara.PanType
                + "&SearchContent=&Page=&EmpID=";

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            content = GetSubString(content, "{", "}");
            HouseRD houseRD = JsonConvert.DeserializeObject<HouseRD>(content);

            return houseRD;
        }
        
        /// <summary>
        /// 获取收藏房源
        /// </summary>
        /// <returns></returns>
        public static async Task<HouseRD> GetCollection()
        {
            string url = "/MyCollect?DBName=cd&EmpID=" + GlobalVariables.LoggedUser.EmpID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            content = GetSubString(content, "{", "}");
            HouseRD houseRD = JsonConvert.DeserializeObject<HouseRD>(content);

            return houseRD;
        }

        /// <summary>
        /// 获取某个房源的详情信息
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> GetOneHouseAsync(string propertyID)
        {
            string url = "queryModProperty?DBName=cd&EmpNoOrTel=" + GlobalVariables.LoggedUser.EmpNo + "&PropertyID=" + propertyID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            content = GetSubString(content, "{", "}");
            return content;
        }
        #endregion

        #region 消息
        /// <summary>
        /// 获取新闻公告
        /// </summary>
        /// <returns></returns>
        public static async Task<NewsRD> GetNewsAsync()
        {
            string url = $"queryNews?DBName=cd&EmpNoOrTel={GlobalVariables.LoggedUser.EmpID}";

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);

            content = GetSubString(content, "{", "}");
            NewsRD newsRD = JsonConvert.DeserializeObject<NewsRD>(content);
            return newsRD;
        }

        #endregion

        /// <summary>
        /// 获取城区列表
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetDistrictList()
        {
            string url = "GetDistrictList?DBName=cd";

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            content = GetSubString(content, "{", "}");
            return content;
        }

        /// <summary>
        /// 根据选择的城区获取片区列表
        /// </summary>
        /// <param name="districtName"></param>
        /// <returns></returns>
        public static async Task<AreaRD> GetAreaMsgByDistrictName(string districtName)
        {
            string url = "GetAreaMsgByDistrictName?DBName=cd&DistrictName=" + districtName;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);

            content = GetSubString(content, "{", "}");
            AreaRD responseData = JsonConvert.DeserializeObject<AreaRD>(content);
            return responseData;
        }

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="addClientPara"></param>
        /// <returns></returns>
        public static async Task<CommonRD> AddNewClient(AddClientPara addClientPara)
        {
            string uri = "NewInquiry";
            //string httpContent = JsonConvert.SerializeObject(addClientPara);

            string httpContent = "";
            Type type = addClientPara.GetType();
            foreach (var item in type.GetProperties())
            {
                httpContent += (item.Name + "=" + item.GetValue(addClientPara, null) + "&");
            }
            httpContent = httpContent.TrimEnd('&');

            string content = await RestSharpHelper<string>.PostFormAsyncWithoutDeserialization(uri, httpContent);
            CommonRD responseData = JsonConvert.DeserializeObject<CommonRD>(content);

            return responseData;
        }

        /// <summary>
        /// 截取字符串，处理网站返回值
        /// </summary>
        /// <param name="content"></param>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        public static string GetSubString(string content, string startStr, string endStr)
        {
            int startIndex = content.IndexOf(startStr);
            //int endIndex = content.LastIndexOf(endStr);
            string str = "";

            str = content.Substring(startIndex, content.Length - startIndex);
            str = str.Substring(0, str.LastIndexOf(endStr) + 1);

            return str;
        }
    }
}
