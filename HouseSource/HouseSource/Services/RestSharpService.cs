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
using RestSharp;

namespace HouseSource.Services
{
    public sealed class RestSharpService
    {
        private static readonly Lazy<RestSharpService> lazy = new Lazy<RestSharpService>(() => new RestSharpService());
        
        public static RestSharpService Instance { get { return lazy.Value; } }

        //public static JsonSerializerSettings settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };

        private RestSharpService() 
        { 
        
        }

        #region 会员注册登录修改密码
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="telOrEmpNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<string> Login(string telOrEmpNo, string password)
        {
            try
            {
                string url = string.Format("Login?TelOrEmpNo={0}&Password={1}", telOrEmpNo, password);

                string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
                return content;
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
            string url = "GetTelCode?Tel=" + tel;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 检查是否已注册
        /// </summary>
        /// <param name="tel">手机号</param>
        /// <returns></returns>
        public static async Task<string> CheckIfRegister(string tel)
        {
            string url = "IfRegisterTel?Tel=" + tel;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="tel">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="name">真实姓名</param>
        /// <returns></returns>
        public static async Task<string> Register(string DBName, string tel, string password, string name, string AccountStyle, string CompanyOrEstateName)
        {
            string url = "ApplyForRegister?DBName=" + DBName + "&EmpNo=&Tel=" + tel
                + "&Password=" + password + "&EmpName=" + name + "&AccountStyle=" + AccountStyle + "&CompanyOrEstateName=" + CompanyOrEstateName;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="tel">手机号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static async Task<string> ResetPassword(string tel, string password)
        {
            string url = "ModTelPassword?Tel=" + tel + "&NewPassWord=" + password;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
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
        public static async Task<string> GetHouseAsync(HousePara housePara, string type)
        {
            string url = "";
            if (type.Equals("出售"))
            {
                url = "QuerySaleHouseSource";
            }
            else if (type.Equals("出租"))
            {
                url = "QueryRentHouseSource";
            }

            url = url + "?DBName=" + GlobalVariables.LoggedUser.DBName
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
                + "&SearchContent=" + housePara.SearchContent
                + "&Page=" + housePara.Page
                + "&EmpID=" + housePara.EmpID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            //content = GetSubString(content, "{", "}");
            //HouseRD houseRD = JsonConvert.DeserializeObject<HouseRD>(content);

            return content;
        }
        
        /// <summary>
        /// 获取收藏房源
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetCollection()
        {
            string url = "GetMyCollection?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpID=" + GlobalVariables.LoggedUser.EmpID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            //content = GetSubString(content, "{", "}");
            //HouseRD houseRD = JsonConvert.DeserializeObject<HouseRD>(content);

            return content;
        }

        //暂时未用
        /// <summary>
        /// 获取某个房源的详情信息
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> GetOneHouseAsync(string propertyID)
        {
            string url = "QueryModProperty?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpNoOrTel=" + GlobalVariables.LoggedUser.EmpNo + "&PropertyID=" + propertyID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获取某个房源是否被收藏
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> CheckCollection(string propertyID)
        {
            string url = "IfPropertyCollectted?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpID=" + GlobalVariables.LoggedUser.EmpID + "&PropertyID=" + propertyID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 收藏或取消收藏
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> CollectOrCancel(string propertyID)
        {
            string url = "CollectProperty?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpID=" + GlobalVariables.LoggedUser.EmpID + "&PropertyID=" + propertyID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获取房源图片列表
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> GetPhotosByPropertyID(string propertyID)
        {
            string url = "GetPhotoUrlByPropertyID?DBName=" + GlobalVariables.LoggedUser.DBName + "&PropertyID=" + propertyID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            if (!content.Contains("EmptyList"))
            {
                content = GetSubString(content, "{", "}");
                return content;
            }
            else
            {
                return "EmptyList";
            }
        }

        /// <summary>
        /// 获得小区基本信息
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> GetEstateInfoByEstateName(string estateName)
        {
            string url = "GetEstateInfoByEstateName?DBName=" + GlobalVariables.LoggedUser.DBName + "&SelectType=1&EstateName=" + estateName;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获得某小区的栋座列表
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> GetDongzuoByEstateID(string estateID)
        {
            string url = "GetDongzuoByEstateID?DBName=" + GlobalVariables.LoggedUser.DBName + "&EstateID=" + estateID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获得某小区某栋楼的单元列表
        /// </summary>
        /// <param name="propertyID">房源编号</param>
        /// <returns></returns>
        public static async Task<string> GetCellByBuildingID(string buildingID)
        {
            string url = "GetCellByBuildingID?DBName=" + GlobalVariables.LoggedUser.DBName + "&BuildingID=" + buildingID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }
        
        /// <summary>
        /// 新增房源
        /// </summary>
        /// <param name="addHousePara"></param>
        /// <param name="photos"></param>
        /// <returns></returns>
        public static async Task<string> AddNewHouse(AddHousePara addHousePara, IList<string> photos)
        {
            string url = "NewHouseData";
            //string httpContent = JsonConvert.SerializeObject(addClientPara);

            //string httpContent = "";
            //Type type = addHousePara.GetType();
            //foreach (var item in type.GetProperties())
            //{
            //    httpContent += (item.Name + "=" + item.GetValue(addHousePara, null) + "&");
            //}
            //httpContent = httpContent.TrimEnd('&');

            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddHeader("content-type", "multipart/form-data");
            //requestPost.AddParameter("multipart/form-data", form, ParameterType.RequestBody);
            Type type = addHousePara.GetType();
            foreach (var item in type.GetProperties())
            {
                //httpContent += (item.Name + "=" + item.GetValue(addHousePara, null) + "&");
                requestPost.AddParameter(item.Name, item.GetValue(addHousePara, null));
            }

            foreach (var item in photos)
            {
                requestPost.AddFile("photos", item);
            }

            string content = await RestSharpHelper<string>.PostFormAsyncWithoutDeserialization(requestPost);
            return content;
        }

        /// <summary>
        /// 修改房源
        /// </summary>
        /// <param name="addHousePara"></param>
        /// <returns></returns>
        public static async Task<string> ModifyHouse(AddHousePara addHousePara)
        {
            string url = "ModHouseData";
            //string httpContent = JsonConvert.SerializeObject(addClientPara);

            //string httpContent = "";
            //Type type = addHousePara.GetType();
            //foreach (var item in type.GetProperties())
            //{
            //    httpContent += (item.Name + "=" + item.GetValue(addHousePara, null) + "&");
            //}
            //httpContent = httpContent.TrimEnd('&');

            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddHeader("content-type", "multipart/form-data");
            //requestPost.AddParameter("multipart/form-data", form, ParameterType.RequestBody);
            Type type = addHousePara.GetType();
            foreach (var item in type.GetProperties())
            {
                //httpContent += (item.Name + "=" + item.GetValue(addHousePara, null) + "&");
                requestPost.AddParameter(item.Name, item.GetValue(addHousePara, null));
            }

            string content = await RestSharpHelper<string>.PostFormAsyncWithoutDeserialization(requestPost);
            return content;
        }

        /// <summary>
        /// 获取房源跟进信息
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public static async Task<string> GetHouseFollowInfo(string propertyID)
        {
            string url = "GetHouseFollowInfo?DBName=" + GlobalVariables.LoggedUser.DBName + "&PropertyID=" + propertyID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 新增房源跟进信息
        /// </summary>
        /// <param name="propertyID"></param>
        /// <param name="_content"></param>
        /// <param name="alertType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<string> NewHouseFollow(string propertyID, string _content,string alertType, string type)
        {
            string url = "NewHouseFollow?DBName=" + GlobalVariables.LoggedUser.DBName + "&PropertyID=" + propertyID
                + "&EmpNo=" + GlobalVariables.LoggedUser.EmpNo
                + "&Content=" + _content
                + "&AlertType=" + alertType
                + "&FollowType=" + type;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }
        #endregion

        #region 消息
        /// <summary>
        /// 获取新闻公告
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetNewsAsync()
        {
            string url = "QueryNews?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpNoOrTel={GlobalVariables.LoggedUser.EmpID}";

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        #endregion

        #region 地理信息
        /// <summary>
        /// 获取城区列表
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetDistrictList()
        {
            string url = "GetDistrictList?DBName=" + GlobalVariables.LoggedUser.DBName + "";

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 根据选择的城区获取片区列表
        /// </summary>
        /// <param name="districtName"></param>
        /// <returns></returns>
        public static async Task<AreaRD> GetAreaMsgByDistrictName(string districtName)
        {
            string url = "GetAreaMsgByDistrictName?DBName=" + GlobalVariables.LoggedUser.DBName + "&DistrictName=" + districtName;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);

            content = GetSubString(content, "{", "}");
            AreaRD responseData = JsonConvert.DeserializeObject<AreaRD>(content);
            return responseData;
        }

        #endregion

        #region 客户
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
        /// 获取客源列表
        /// </summary>
        /// <param name="clientPara"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<string> GetClientAsync(ClientPara clientPara, string type)
        {
            string url = (type == "Sale" ? "QuerySaleCustomerSource" : "QueryRentCustomerSource")
                + "?DBName=" + GlobalVariables.LoggedUser.DBName + ""
                + "&Tel=" + GlobalVariables.LoggedUser.EmpNo
                + "&RoomStyle=" + clientPara.RoomStyle
                + "&IsPublic=" + clientPara.IsPublic
                + "&Floor=" + clientPara.Floor
                + "&Square=" + clientPara.Square
                + "&Price=" + clientPara.Price
                + "&CusName=" + clientPara.CusName
                + "&Phone=" + clientPara.Phone
                + "&Contact=" + clientPara.Contact
                + "&SearchContent=" + clientPara.SearchContent
                + "&Page=" + clientPara.Page
                + "&EmpID=" + clientPara.EmpID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获取客源跟进信息
        /// </summary>
        /// <param name="inquiryID"></param>
        /// <returns></returns>
        public static async Task<string> GetInquiryFollowInfo(string inquiryID)
        {
            string url = "GetInquiryFollowInfo?DBName=" + GlobalVariables.LoggedUser.DBName + "&InquiryID=" + inquiryID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 新增跟进信息
        /// </summary>
        /// <param name="inquiryID"></param>
        /// <returns></returns>
        public static async Task<string> NewInquiryFollow(string inquiryID, string _content, string type)
        {
            string url = "NewInquiryFollow?DBName=" + GlobalVariables.LoggedUser.DBName + "&InquiryID=" + inquiryID
                + "&EmpNo=" + GlobalVariables.LoggedUser.EmpNo
                + "&Content=" + _content
                + "&FollowType=" + type;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }
        #endregion

        #region 报表
        /// <summary>
        /// 获取业绩
        /// </summary>
        /// <param name="timeOption"></param>
        /// <returns></returns>
        public static async Task<string> GetAchievement(string timeOption)
        {
            string url = "QueryAchievement?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpID=" + GlobalVariables.LoggedUser.EmpID + "&TimeOptions=" + timeOption;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获取工作量
        /// </summary>
        /// <param name="timeOption"></param>
        /// <returns></returns>
        public static async Task<string> GetWorkLoad(string timeOption)
        {
            string url = "QueryWorkload?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpID=" + GlobalVariables.LoggedUser.EmpID + "&TimeOptions=" + timeOption;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获取工作量排名
        /// </summary>
        /// <param name="timeOption"></param>
        /// <returns></returns>
        public static async Task<string> GetWorkLoadRank(string timeOption)
        {
            string url = "QueryWorkloadRank?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpID=" + GlobalVariables.LoggedUser.EmpID + "&TimeOptions=" + timeOption;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }
        #endregion

        #region 检查更新
        public static async Task<string> GetNewestVersion()
        {
            try
            {
                string url = "GetNewestAPPVersionMsg";

                string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
                return content;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 获取办理业务的链接
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetBusinessHand()
        {
            string url = "BusinessHand?DBName=" + GlobalVariables.LoggedUser.DBName + "&EmpID=" + GlobalVariables.LoggedUser.EmpID;

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
        }

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetDBName()
        {
            string url = "GetDBList";

            string content = await RestSharpHelper<string>.GetAsyncWithoutDeserialization(url);
            return content;
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

            string str = content.Substring(startIndex, content.Length - startIndex);
            str = str.Substring(0, str.LastIndexOf(endStr) + 1);

            return str;
        }

    }
}
