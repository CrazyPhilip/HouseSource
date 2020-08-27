using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace HouseSource.Utils
{
    public static class Tools
    {
        /// <summary>
        /// 检测是否手机号码
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string tel)
        {
            if (string.IsNullOrWhiteSpace(tel))
            {
                return false;
            }

            string CellPhoneReg = @"^[1]+[3,4,5,7,8,9]+\d{9}$";   //手机
            return Regex.IsMatch(tel, CellPhoneReg);
        }

        /// <summary>
        /// 检测是否数字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsNumber(string num)
        {
            if (string.IsNullOrWhiteSpace(num))
            {
                return false;
            }

            string numberReg = @"^[1-9]\d*$";   //手机
            return Regex.IsMatch(num, numberReg);
        }

        /// <summary>
        /// 不同类型对象同名属性赋值
        /// </summary>
        /// <typeparam name="S">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="s">源对象</param>
        /// <param name="t">目标对象</param>
        public static void AutoMapping<S, T>(S s, T t)
        {
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            Type target = t.GetType();

            foreach (var pp in pps)
            {
                try
                {
                    PropertyInfo targetPP = target.GetProperty(pp.Name);
                    object value = pp.GetValue(s, null);

                    if (targetPP != null && value != null)
                    {
                        targetPP.SetValue(t, value, null);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static PropertyInfo[] GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 检测是否连接互联网
        /// </summary>
        /// <returns></returns>
        public static bool IsNetConnective()
        {
            var current = Connectivity.NetworkAccess;

            return current == NetworkAccess.Internet;

        }

        /// <summary>
        /// 正则表达式检测Email格式
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static bool CheckEmail(string Email)
        {
            bool Flag = false;
            string str = "([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,5})+";
            string[] result = GetPathPoint(Email, str);
            if (result != null)
            {
                Flag = result.Contains(Email) ? true : Flag;
            }
            return Flag;
        }

        /// <summary>
        /// 获取正则表达式匹配结果集
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regx">正则表达式</param>
        /// <returns></returns>
        private static string[] GetPathPoint(string value, string regx)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            bool isMatch = Regex.IsMatch(value, regx);
            if (!isMatch)
                return null;
            MatchCollection matchCol = Regex.Matches(value, regx);
            string[] result = new string[matchCol.Count];
            if (matchCol.Count > 0)
            {
                for (int i = 0; i < matchCol.Count; i++)
                {
                    result[i] = matchCol[i].Value;
                }
            }
            return result;
        }

        /// <summary>
        /// 检测和请求权限
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission) where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }


        /// <summary>
        /// 分离字符串，获取栋座和单元的数字信息
        /// </summary>
        /// <param name="str">要解析的字符串</param>
        /// <returns></returns>
        public static List<string> GetDigit(string str)
        {
            List<string> strList = new List<string>();
            MatchCollection ms;
            ms = Regex.Matches(str, @"\d+");
            foreach (Match m in ms)
            {
                strList.Add(m.Value);
            }
            return strList;
        }
    }
}
