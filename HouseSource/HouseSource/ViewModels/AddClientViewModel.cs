using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HouseSource.Models;
using HouseSource.ResponseData;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Xamarin.Forms;

namespace HouseSource.ViewModels
{
    public class AddClientViewModel : BaseViewModel
    {
        #region 参数
        private string clientName;     //姓名
        public string ClientName
        {
            get { return clientName; }
            set { SetProperty(ref clientName, value); }
        }

        private string clientPhoneNum;     //电话
        public string ClientPhoneNum
        {
            get { return clientPhoneNum; }
            set { SetProperty(ref clientPhoneNum, value); }
        }

        private string trade;   //交易
        public string Trade
        {
            get { return trade; }
            set { SetProperty(ref trade, value); }
        }

        private string censusRegister;   //户籍
        public string CensusRegister
        {
            get { return censusRegister; }
            set { SetProperty(ref censusRegister, value); }
        }

        private string marriage;   //婚姻
        public string Marriage
        {
            get { return marriage; }
            set { SetProperty(ref marriage, value); }
        }

        private string usage;   //用途
        public string Usage
        {
            get { return usage; }
            set { SetProperty(ref usage, value); }
        }

        private string level;   //等级
        public string Level
        {
            get { return level; }
            set { SetProperty(ref level, value); }
        }

        private string decoration;   //装修
        public string Decoration
        {
            get { return decoration; }
            set { SetProperty(ref decoration, value); }
        }

        private string houseType;   //房屋类型
        public string HouseType
        {
            get { return houseType; }
            set { SetProperty(ref houseType, value); }
        }

        private string minBudget;     //最低预算
        public string MinBudget
        {
            get { return minBudget; }
            set { SetProperty(ref minBudget, value); }
        }

        private string maxBudget;     //最高预算
        public string MaxBudget
        {
            get { return maxBudget; }
            set { SetProperty(ref maxBudget, value); }
        }

        private string minArea;     //最小面积
        public string MinArea
        {
            get { return minArea; }
            set { SetProperty(ref minArea, value); }
        }

        private string maxArea;     //最大面积
        public string MaxArea
        {
            get { return maxArea; }
            set { SetProperty(ref maxArea, value); }
        }

        private string minFloor;     //最低楼层
        public string MinFloor
        {
            get { return minFloor; }
            set { SetProperty(ref minFloor, value); }
        }

        private string maxFloor;     //最高楼层
        public string MaxFloor
        {
            get { return maxFloor; }
            set { SetProperty(ref maxFloor, value); }
        }

        private string minRooms;     //最小房数
        public string MinRooms
        {
            get { return minRooms; }
            set { SetProperty(ref minRooms, value); }
        }

        private string maxRooms;     //最大房数
        public string MaxRooms
        {
            get { return maxRooms; }
            set { SetProperty(ref maxRooms, value); }
        }

        private string district;   //城区
        public string District
        {
            get { return district; }
            set { SetProperty(ref district, value); }
        }

        private string block;   //片区、街区
        public string Block
        {
            get { return block; }
            set { SetProperty(ref block, value); }
        }

        #endregion

        #region 筛选器
        private List<string> tradeList;   //交易类型列表
        public List<string> TradeList
        {
            get { return tradeList; }
            set { SetProperty(ref tradeList, value); }
        }

        private List<string> censusRegisterList;   //户籍列表
        public List<string> CensusRegisterList
        {
            get { return censusRegisterList; }
            set { SetProperty(ref censusRegisterList, value); }
        }

        private List<string> marriageList;   //婚姻列表
        public List<string> MarriageList
        {
            get { return marriageList; }
            set { SetProperty(ref marriageList, value); }
        }

        private List<string> usageList;   //用途列表
        public List<string> UsageList
        {
            get { return usageList; }
            set { SetProperty(ref usageList, value); }
        }

        private List<string> levelList;   //等级列表
        public List<string> LevelList
        {
            get { return levelList; }
            set { SetProperty(ref levelList, value); }
        }

        private List<string> decorationList;   //装修列表
        public List<string> DecorationList
        {
            get { return decorationList; }
            set { SetProperty(ref decorationList, value); }
        }

        private List<string> houseTypeList;     //房屋类型列表
        public List<string> HouseTypeList
        {
            get { return houseTypeList; }
            set { SetProperty(ref houseTypeList, value); }
        }

        private List<string> districtList;     //城区列表
        public List<string> DistrictList
        {
            get { return districtList; }
            set { SetProperty(ref districtList, value); }
        }

        private List<string> blockList;     //片区、街区列表
        public List<string> BlockList
        {
            get { return blockList; }
            set { SetProperty(ref blockList, value); }
        }

        #endregion

        private AreaInfo[] areaInfoArray{ get; set; }

        #region 命令
        public Command DistrictPickerSelectedItemChangedCommand { get; set; }
        public Command AddCommand { get; set; }
        public Command ClearCommand { get; set; }
        #endregion

        public AddClientViewModel()
        {
            Title = "新增客源";

            HouseTypeList = new List<string>
            {
                "多层", "高层", "小高层", "多层复式", "高层复式", "多层跃式",
                "高层跃式", "独立别墅", "联体别墅", "双拼别墅", "叠加别墅",
                "围院别墅", "裙楼", "四合院"
            };

            TradeList = new List<string> { "求购", "求租" };

            DecorationList = new List<string> { "毛坯", "清水", "简装", "中装", "精装", "豪装" };

            LevelList = new List<string> { "A", "B", "C" };

            UsageList = new List<string> { "住宅", "商住", "商铺", "网点", "写字楼", "厂房", "写厂", "铺厂", "仓库", "地皮", "工厂", "车位", "公寓", "其他" };

            MarriageList = new List<string> { "未婚", "已婚", "离婚" };

            CensusRegisterList = new List<string> { "外地", "本地" };

            InitDistrictPicker();

            DistrictPickerSelectedItemChangedCommand = new Command(() =>
            {
                GetAreas();
            }, () => { return true; });

            AddCommand = new Command(() =>
            {
                if (CheckInput())
                {
                    Add();
                }
            }, () => { return true; });

            ClearCommand = new Command(() =>
            {

            }, () => { return true; });


        }

        /// <summary>
        /// 初始化District选择器
        /// </summary>
        private async void InitDistrictPicker()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string content = await RestSharpService.GetDistrictList();

                string[] dists = content.TrimStart('{').TrimEnd('}').Split(',');

                DistrictList = new List<string>(dists);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取片区列表
        /// </summary>
        private async void GetAreas()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                AreaRD areaRD = await RestSharpService.GetAreaMsgByDistrictName(District);

                if (areaRD.Msg == "Success")
                {
                    List<string> areaList = new List<string>();

                    areaInfoArray = areaRD.AreaMsg;

                    foreach (var item in areaRD.AreaMsg)
                    {
                        areaList.Add(item.AreaName);
                    }

                    BlockList = areaList;
                    return;
                }
                else if (areaRD.Msg == "EmptyList")
                {
                    CrossToastPopUp.Current.ShowToastWarning("该城区下没有片区信息！", ToastLength.Short);
                    BlockList = new List<string>();
                    return;
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("获取片区列表失败！", ToastLength.Short);
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// 上传服务器
        /// </summary>
        private async void Add()
        {
            string areaID = "";
            foreach (var item in areaInfoArray)
            {
                if (Block == item.AreaName)
                {
                    areaID = item.AreaID;
                }
            }

            AddClientPara addClientPara = new AddClientPara
            {
                CustName = ClientName,
                CustMobile = ClientPhoneNum,
                Trade = Trade,
                Country = CensusRegister,
                PropertyFloor = Marriage,
                PropertyUsage = Usage,

                CustGrade = Level == null || Level == "" ? "*" : Level,
                PropertyDecoration = Decoration == null || Decoration == "" ? "*" : Decoration,
                PropertyType = HouseType == null || HouseType == "" ? "*" : HouseType,
                PriceMin = MinBudget == null || MinBudget == "" ? "0" : MinBudget,
                PriceMax = MaxBudget == null || MaxBudget == "" ? "0" : MaxBudget,
                SquareMin = MinArea == null || MinArea == "" ? "0" : MinArea,
                SquareMax = MaxArea == null || MaxArea == "" ? "0" : MaxArea,
                FoorStart = MinFloor == null || MinFloor == "" ? "*" : MinFloor,
                FoorEnd = MaxFloor == null || MaxFloor == "" ? "*" : MaxFloor,
                CountF = MaxRooms == null || MaxRooms == "" ? "*" : MaxRooms,
                CountFStart = MinRooms == null || MinRooms == "" ? "*" : MinRooms,
                DistrictName = District == null || District == "" ? "*" : District,
                AreaID = areaID == null || areaID == "" ? "*" : areaID,

                DBName = "cd",
                EmpNoOrTel = GlobalVariables.LoggedUser.EmpNo
            };//此处EmpNoOrTel只能是电话号码

            CommonRD responseData = await RestSharpService.AddNewClient(addClientPara);

            if (responseData.Msg == "Success")
            {
                CrossToastPopUp.Current.ShowToastSuccess("客源新增成功", ToastLength.Long);
            }
            else if (responseData.Msg == "KYExists")
            {
                CrossToastPopUp.Current.ShowToastError("新增失败，重复的客源，拒绝新增", ToastLength.Long);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError("客源新增失败", ToastLength.Long);
            }
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            string IntNumReg = @"^\d+$";   //非负整数
            string FloatNumReg = @"^\d+(\.\d+)?$";    //非负浮点数
            //string TelReg = @"^(\d{3,4}-)?\d{6,8}$";   //电话
            string CellPhoneReg = @"^[1]+[3,4,5,7,8,9]+\d{9}$";   //手机

            if (ClientName == null || ClientName == "")
            {
                CrossToastPopUp.Current.ShowToastError("客源姓名不能为空，请填写姓名", ToastLength.Long);
                return false;
            }

            if (ClientPhoneNum == null || ClientPhoneNum == "")
            {
                CrossToastPopUp.Current.ShowToastError("客源电话不能为空，请填写姓名", ToastLength.Long);
                return false;
            }
            else if (!Regex.IsMatch(ClientPhoneNum, CellPhoneReg))
            {
                CrossToastPopUp.Current.ShowToastError("电话格式不对，请重新填写正确的电话", ToastLength.Long);
                return false;
            }

            if (Trade == null || Trade == "")
            {
                CrossToastPopUp.Current.ShowToastError("交易类型不能为空", ToastLength.Long);
                return false;
            }

            if (CensusRegister == null || CensusRegister == "")
            {
                CrossToastPopUp.Current.ShowToastError("户籍不能为空，请选择", ToastLength.Long);
                return false;
            }

            if (Marriage == null || Marriage == "")
            {
                CrossToastPopUp.Current.ShowToastError("婚姻不能为空，请选择", ToastLength.Long);
                return false;
            }

            if (Usage == null || Usage == "")
            {
                CrossToastPopUp.Current.ShowToastError("用途不能为空，请选择", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(District))
            {
                CrossToastPopUp.Current.ShowToastError("请选择城区", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Block))
            {
                CrossToastPopUp.Current.ShowToastError("请选择片区", ToastLength.Long);
                return false;
            }

            //预算
            if (MinBudget != null && MinBudget != "")
            {
                if (!Regex.IsMatch(MinBudget, FloatNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最低预算格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MaxBudget != null && MaxBudget != "")
            {
                if (!Regex.IsMatch(MaxBudget, FloatNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最高预算格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MinBudget != null && MinBudget != "" && MaxBudget != null && MaxBudget != "")
            {
                if (float.Parse(MinBudget) > float.Parse(MaxBudget))
                {
                    CrossToastPopUp.Current.ShowToastError("最高预算不能小于最低预算，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            //面积
            if (MinArea != null && MinArea != "")
            {
                if (!Regex.IsMatch(MinArea, FloatNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最小面积格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MaxArea != null && MaxArea != "")
            {
                if (!Regex.IsMatch(MaxArea, FloatNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最大面积格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MinArea != null && MinArea != "" && MaxArea != null && MaxArea != "")
            {
                if (float.Parse(MinArea) > float.Parse(MaxArea))
                {
                    CrossToastPopUp.Current.ShowToastError("最大面积不能小于最小面积，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            //楼层
            if (MinFloor != null && MinFloor != "")
            {
                if (!Regex.IsMatch(MinFloor, IntNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最低楼层格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MaxFloor != null && MaxFloor != "")
            {
                if (!Regex.IsMatch(MaxFloor, IntNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最高楼层格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MinFloor != null && MinFloor != "" && MaxFloor != null && MaxFloor != "")
            {
                if (int.Parse(MinFloor) > int.Parse(MaxFloor))
                {
                    CrossToastPopUp.Current.ShowToastError("最高楼层不能小于最低楼层，请重新填写", ToastLength.Long);
                    return false;
                }
            }
            //房数
            if (MinRooms != null && MinRooms != "")
            {
                if (!Regex.IsMatch(MinRooms, IntNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最小房数格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MaxRooms != null && MaxRooms != "")
            {
                if (!Regex.IsMatch(MaxRooms, IntNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("最大房数格式不对，只能是数字，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            if (MinRooms != null && MinRooms != "" && MaxRooms != null && MaxRooms != "")
            {
                if (int.Parse(MinRooms) > int.Parse(MaxRooms))
                {
                    CrossToastPopUp.Current.ShowToastError("最大房数不能小于最小房数，请重新填写", ToastLength.Long);
                    return false;
                }
            }

            return true;
        }
    }
}
