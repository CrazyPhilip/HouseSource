using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HouseSource.Models;
using HouseSource.ResponseData;
using HouseSource.Views;
using HouseSource.Services;
using HouseSource.Utils;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HouseSource.ViewModels
{
    public class AddHouseViewModel : BaseViewModel
    {
        private ObservableCollection<string> imageList;   //图片列表
        public ObservableCollection<string> ImageList
        {
            get { return imageList; }
            set { SetProperty(ref imageList, value); }
        }

        private List<string> tradeList;   //租售类型
        public List<string> TradeList
        {
            get { return tradeList; }
            set { SetProperty(ref tradeList, value); }
        }

        private List<string> propertyTypeList;   //房型列表
        public List<string> PropertyTypeList
        {
            get { return propertyTypeList; }
            set { SetProperty(ref propertyTypeList, value); }
        }

        private List<string> decorationList;   //装修列表
        public List<string> DecorationList
        {
            get { return decorationList; }
            set { SetProperty(ref decorationList, value); }
        }

        private List<string> directionList;   //房屋朝向列表
        public List<string> DirectionList
        {
            get { return directionList; }
            set { SetProperty(ref directionList, value); }
        }

        private List<string> propertyUseTypeList;   //使用类型列表
        public List<string> PropertyUseTypeList
        {
            get { return propertyUseTypeList; }
            set { SetProperty(ref propertyUseTypeList, value); }
        }

        private List<string> propertySourceList;   //来源类型列表
        public List<string> PropertySourceList
        {
            get { return propertySourceList; }
            set { SetProperty(ref propertySourceList, value); }
        }

        private List<string> propertyRightList;   //房屋产权列表
        public List<string> PropertyRightList
        {
            get { return propertyRightList; }
            set { SetProperty(ref propertyRightList, value); }
        }

        private List<string> credentialsList;   //证件类型列表
        public List<string> CredentialsList
        {
            get { return credentialsList; }
            set { SetProperty(ref credentialsList, value); }
        }

        private List<string> statusList;   //房屋现状列表
        public List<string> StatusList
        {
            get { return statusList; }
            set { SetProperty(ref statusList, value); }
        }

        private List<string> lookWaysList;   //看房方式列表
        public List<string> LookWaysList
        {
            get { return lookWaysList; }
            set { SetProperty(ref lookWaysList, value); }
        }

        private List<string> commissionPayList;   //付佣列表
        public List<string> CommissionPayList
        {
            get { return commissionPayList; }
            set { SetProperty(ref commissionPayList, value); }
        }

        private List<string> paymentList;   //付款方式列表
        public List<string> PaymentList
        {
            get { return paymentList; }
            set { SetProperty(ref paymentList, value); }
        }

        private List<string> furnitureList;   //家具
        public List<string> FurnitureList
        {
            get { return furnitureList; }
            set { SetProperty(ref furnitureList, value); }
        }

        private List<string> ownershipList;   //权属列表
        public List<string> OwnershipList
        {
            get { return ownershipList; }
            set { SetProperty(ref ownershipList, value); }
        }

        private ObservableCollection<string> buildingList;   //栋座列表
        public ObservableCollection<string> BuildingList
        {
            get { return buildingList; }
            set { SetProperty(ref buildingList, value); }
        }

        BuildingInfo[] RawBuildingList { get; set; }   //栋座列表  原始

        private ObservableCollection<string> unitList;   //单元列表
        public ObservableCollection<string> UnitList
        {
            get { return unitList; }
            set { SetProperty(ref unitList, value); }
        }

        private List<string> floorList;   //楼层列表
        public List<string> FloorList
        {
            get { return floorList; }
            set { SetProperty(ref floorList, value); }
        }

        private List<string> totalFloorList;   //总楼层列表
        public List<string> TotalFloorList
        {
            get { return totalFloorList; }
            set { SetProperty(ref totalFloorList, value); }
        }

        private EstateItemInfo estate;   //小区
        public EstateItemInfo Estate
        {
            get { return estate; }
            set { SetProperty(ref estate, value); }
        }

        /*
        private List<string> bedroomList;   //室 列表
        public List<string> BedroomList
        {
            get { return bedroomList; }
            set { SetProperty(ref bedroomList, value); }
        }

        private List<string> diningroomList;   //厅 列表
        public List<string> DiningroomList
        {
            get { return diningroomList; }
            set { SetProperty(ref diningroomList, value); }
        }

        private List<string> bathroomList;   //卫 列表
        public List<string> BathroomList
        {
            get { return bathroomList; }
            set { SetProperty(ref bathroomList, value); }
        }

        private List<string> balconyList;   //阳台 列表
        public List<string> BalconyList
        {
            get { return balconyList; }
            set { SetProperty(ref balconyList, value); }
        }*/

        private AddHousePara para;   //Comment
        public AddHousePara Para
        {
            get { return para; }
            set { SetProperty(ref para, value); }
        }

        private DateTime releaseDate;   //交房日
        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { SetProperty(ref releaseDate, value); }
        }

        private DateTime entrustDate;   //挂牌日
        public DateTime EntrustDate
        {
            get { return entrustDate; }
            set { SetProperty(ref entrustDate, value); }
        }

        private List<string> builtYearList;   //建造年代列表
        public List<string> BuiltYearList
        {
            get { return builtYearList; }
            set { SetProperty(ref builtYearList, value); }
        }

        private bool flagWeb;   //对外展示
        public bool FlagWeb
        {
            get { return flagWeb; }
            set { SetProperty(ref flagWeb, value); }
        }

        private bool flagMWWY;   //满五唯一
        public bool FlagMWWY
        {
            get { return flagMWWY; }
            set { SetProperty(ref flagMWWY, value); }
        }

        private bool flagWDY;   //无抵押
        public bool FlagWDY
        {
            get { return flagWDY; }
            set { SetProperty(ref flagWDY, value); }
        }

        private bool flagKDK;   //可贷款
        public bool FlagKDK
        {
            get { return flagKDK; }
            set { SetProperty(ref flagKDK, value); }
        }

        private bool flagXSFY;   //新上房源
        public bool FlagXSFY
        {
            get { return flagXSFY; }
            set { SetProperty(ref flagXSFY, value); }
        }

        private string buildNo;   //栋座
        public string BuildNo
        {
            get { return buildNo; }
            set { SetProperty(ref buildNo, value); }
        }

        private string unit;   //单元号
        public string Unit
        {
            get { return unit; }
            set { SetProperty(ref unit, value); }
        }

        public Command AddImageCommand { get; set; }
        public Command<string> DoubleTappedCommand { get; set; }
        public Command AddCommand { get; set; }
        public Command ClearCommand { get; set; }
        public Command ToEstateSelectCommand { get; set; }
        public Command<int> GetUnitsCommand { get; set; }

        public AddHouseViewModel()
        {
            ImageList = new ObservableCollection<string>();
            TradeList = new List<string> { "出售", "出租" };
            DirectionList = new List<string> { "南北", "东西", "南", "北", "东", "西", "东南", "西南", "东北", "西北" };
            DecorationList = new List<string> { "毛坯", "简装", "中装", "精装", "豪装", "其它" };
            PropertyTypeList = new List<string> { "多层", "高层", "小高层", "多层复式", "高层复式", "多层跃式", "高层跃式", "独立别墅", "联体别墅", "双拼别墅", "叠加别墅", "围院别墅", "裙楼", "四合院" };
            PropertySourceList = new List<string> { "上店", "驻守", "贴单", "网络", "朋友", "同行", "安居客", "58", "房天下", "68好房", "平台推荐" };
            PropertyUseTypeList = new List<string> { "住宅", "商住", "商铺", "网点", "写字楼", "厂房", "写厂", "铺厂", "仓库", "地皮", "工厂", "车位", "公寓", "其他" };
            PropertyRightList = new List<string> { "单独所有", "共有", "单位产权" };
            CredentialsList = new List<string> { "不动产证", "房权证", "购房合同", "委托公证", "法院拍卖", "判决书", "离婚协议", "继承" };
            StatusList = new List<string> { "自住", "空房", "出租", "经商", "查封" };
            LookWaysList = new List<string> { "预约", "有钥匙", "借钥匙", "直接看" };
            CommissionPayList = new List<string> { "满佣", "折扣", "拒付", "商议" };
            PaymentList = new List<string> { "一次性", "按揭", "垫资解押", "月付", "季度付", "半年付", "年付商议" };
            FurnitureList = new List<string> { "少量", "全配", "无", "可协商" };
            OwnershipList = new List<string> { "商品房", "房改房", "经济适用房", "集体房", "军产房", "其它房" };

            BuiltYearList = new List<string>();
            for (int i = 1970; i <= 2030; i++)
            {
                BuiltYearList.Add(i.ToString());
            }

            Para = new AddHousePara();
            Estate = new EstateItemInfo();

            //BedroomList = new List<string>();
            //DiningroomList = new List<string>(); 
            //BathroomList = new List<string>();
            //BalconyList = new List<string>();
            //for (int i = 0; i < 10; i++)
            //{
            //    BedroomList.Add()
            //}

            AddImageCommand = new Command(async () =>
            {
                await GetReadPermissionAsync();

                List<string> list = await DependencyService.Get<IImagePickerService>().PickImageAsync();

                list?.ForEach(item => ImageList.Add(item));
                //if (list != null)
                //{
                //foreach (var item in streams)
                //{
                //stack.Children.Add(new Image() { Source = ImageSource.FromStream(() => item) });
                //stack.Children.Add(new Image() { Source = item });

                //}
                //image.Source = ImageSource.FromStream(() => stream);
                //byte[] bytes = new byte[stream.Length];
                //stream.Read(bytes, 0, bytes.Length);
                //stream.Seek(0, SeekOrigin.Begin);
                //
                //string base64 = Convert.ToBase64String(bytes);
                //Console.WriteLine(base64);
                //}
            }, () => { return true; });

            DoubleTappedCommand = new Command<string>(async (index) =>
            {
                bool result = await Application.Current.MainPage.DisplayAlert("删除", "要删除这张照片吗？", "确定", "取消");
                if (result)
                {
                    ImageList.Remove(index);
                }
            }, (index) => { return true; });

            AddCommand = new Command(() =>
            {
                if (CheckInput())
                {
                    AddNewHouse();
                }
            }, () => { return true; });

            ClearCommand = new Command(() =>
            {

            }, () => { return true; });

            ToEstateSelectCommand = new Command(async () =>
            {
                EstateSelectPage estateSelectPage = new EstateSelectPage();
                await Application.Current.MainPage.Navigation.PushAsync(estateSelectPage);
            }, () => { return true; });

            GetUnitsCommand = new Command<int>((index) =>
            {
                GetUnits(RawBuildingList[index].BuildingID);
            }, (index) => { return true; });

        }

        /// <summary>
        /// 获取栋座
        /// </summary>
        public async void GetBuildings()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                BuildingRD buildingRD = await RestSharpService.GetDongzuoByEstateID(Estate.EstateID);

                if (buildingRD.Msg == "EmptyList")
                {
                    CrossToastPopUp.Current.ShowToastError("该小区下没有栋座信息，拒绝新增，请在电脑端完善后再进行录入。", ToastLength.Short);
                    return;
                }
                else
                {
                    BuildingList = new ObservableCollection<string>();
                    foreach (var item in buildingRD.Dongzuo)
                    {
                        BuildingList.Add(item.BuildingName);
                    }
                    RawBuildingList = buildingRD.Dongzuo;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取单元列表
        /// </summary>
        public async void GetUnits(string buildingID)
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Short);
                    return;
                }

                string result = await RestSharpService.GetCellByBuildingID(buildingID);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    JObject jObject = JObject.Parse(result);
                    if (jObject["Msg"].ToString() == "EmptyList")
                    {
                        CrossToastPopUp.Current.ShowToastError("该栋座下没有单元信息", ToastLength.Short);
                        return;
                    }
                    else if (jObject["Msg"].ToString() == "Success")
                    {
                        string value = jObject["Cell"][0]["Cell"].ToString();
                        if (value == "000" || value == "NULL" || value == "" || value == "undefined")
                        {
                            UnitList = new ObservableCollection<string> { "无" };
                        }
                        else
                        {
                            int number = int.Parse(value.Substring(1));
                            string type = value.Substring(0, 1);

                            UnitList = new ObservableCollection<string>();

                            switch (type)
                            {
                                case "0":
                                    {
                                        for (int i = 0; i < number; i++)
                                        {
                                            UnitList.Add(i + 1 + "单元");
                                        }
                                    }
                                    break;

                                case "A":
                                    {
                                        string[] charArr = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q",
                                            "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

                                        for (int i = 0; i < number; i++)
                                        {
                                            UnitList.Add(charArr[i] + "单元");
                                        }
                                    }
                                    break;

                                case "T":
                                    {
                                        string[] chnArr = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };

                                        for (int i = 0; i < number; i++)
                                        {
                                            UnitList.Add(chnArr[i] + "单元");
                                        }
                                    }
                                    break;

                                case "D":
                                    {
                                        string[] chnArr = { "东", "南", "西", "北", "中", "东南", "西南", "东北", "西北" };

                                        for (int i = 0; i < number; i++)
                                        {
                                            UnitList.Add(chnArr[i] + "单元");
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
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

            if (string.IsNullOrWhiteSpace(Estate.CityName))
            {
                CrossToastPopUp.Current.ShowToastError("房源城市不能为空，请重新选择楼盘", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Estate.DistrictName))
            {
                CrossToastPopUp.Current.ShowToastError("房源城市不能为空，请重新选择楼盘", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Estate.EstateID))
            {
                CrossToastPopUp.Current.ShowToastError("房源城市不能为空，请重新选择楼盘", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(BuildNo))
            {
                CrossToastPopUp.Current.ShowToastError("栋座不能为空，请重新填写栋座", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Unit))
            {
                CrossToastPopUp.Current.ShowToastError("单元不能为空，请重新填写栋座", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.Floor))
            {
                CrossToastPopUp.Current.ShowToastError("楼层不能为空，请填写楼层", ToastLength.Long);
                return false;
            }
            else
            {
                if (!Regex.IsMatch(Para.Floor, IntNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("楼层格式不对，只能是数字，请重新填写楼层", ToastLength.Long);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(Para.FloorAll))
            {
                CrossToastPopUp.Current.ShowToastError("楼层不能为空，请填写楼层", ToastLength.Long);
                return false;
            }
            else
            {
                if (!Regex.IsMatch(Para.FloorAll, IntNumReg))
                {
                    CrossToastPopUp.Current.ShowToastError("楼层格式不对，只能是数字，请重新填写楼层", ToastLength.Long);
                    return false;
                }
            }

            if (int.Parse(Para.Floor) > int.Parse(Para.FloorAll))
            {
                CrossToastPopUp.Current.ShowToastError("总楼层不能小于楼层，请重新填写总楼层", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.RoomNo))
            {
                CrossToastPopUp.Current.ShowToastError("房号不能为空，请重新选择楼盘", ToastLength.Long);
                return false;
            }
            else
            {
                if (!Regex.IsMatch(Para.RoomNo, @"^[0-9a-zA-Z]+$"))
                {
                    CrossToastPopUp.Current.ShowToastError("房号格式不对，只能是数字或字母，请重新填写房号", ToastLength.Long);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(Para.Title))
            {
                CrossToastPopUp.Current.ShowToastError("房源标题不能为空，请重新选择楼盘", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.Trade))
            {
                CrossToastPopUp.Current.ShowToastError("交易类型不能为空", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.PropertyDirection))
            {
                CrossToastPopUp.Current.ShowToastError("朝向不能为空，请选择朝向", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.Price))
            {
                CrossToastPopUp.Current.ShowToastError(Para.Trade == "出售" ? "售价不能为空，请重新填写售价" : "租金不能为空，请重新填写租金", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(Para.Price, FloatNumReg))
            {
                CrossToastPopUp.Current.ShowToastError(Para.Trade == "出售" ? "售价格式不对，只能是数字，请重新填写售价" : "租金格式不对，只能是数字，请重新填写租金", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.Square))
            {
                CrossToastPopUp.Current.ShowToastError("面积不能为空，请重新填写面积", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(Para.Square, FloatNumReg))
            {
                CrossToastPopUp.Current.ShowToastError("面积格式不对，只能是数字，请重新填写面积", ToastLength.Long);
                return false;
            }


            if (string.IsNullOrWhiteSpace(Para.CountF) || string.IsNullOrWhiteSpace(Para.CountT) || string.IsNullOrWhiteSpace(Para.CountW) || string.IsNullOrWhiteSpace(Para.CountY))
            {
                CrossToastPopUp.Current.ShowToastError("户型不能为空，或者户型不全，请重新填写户型", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(Para.CountF, IntNumReg) || !Regex.IsMatch(Para.CountT, IntNumReg) || !Regex.IsMatch(Para.CountW, IntNumReg) || !Regex.IsMatch(Para.CountY, IntNumReg) )
            {
                CrossToastPopUp.Current.ShowToastError("户型格式不对，只能是数字，请重新填写户型", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.OwnerName))
            {
                CrossToastPopUp.Current.ShowToastError("业主姓名不能为空，请重新填写业主姓名", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Para.OwnerMobile))
            {
                CrossToastPopUp.Current.ShowToastError("业主电话不能为空，请重新填写业主电话", ToastLength.Long);
                return false;
            }

            if (!Regex.IsMatch(Para.OwnerMobile, @"[0-9-()（）]{7,18}") || !Regex.IsMatch(Para.OwnerMobile, @"0?(13|14|15|18)[0-9]{9}"))
            {
                CrossToastPopUp.Current.ShowToastError("业主电话格式不对，请重新填写正确的电话", ToastLength.Long);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 提交新增
        /// </summary>
        private async void AddNewHouse()
        {
            try
            {
                Para.DBName = GlobalVariables.LoggedUser.DBName;
                Para.CityName = Estate.CityName;
                Para.DistrictName = Estate.DistrictName;
                Para.EstateID = Estate.EstateID;
                Para.BuildNo = BuildNo + Unit;
                Para.PriceUnit = Para.Trade == "出售" ? (Double.Parse(Para.Price) * 10000 / Double.Parse(Para.Square)).ToString() : (Double.Parse(Para.Price) / Double.Parse(Para.Square)).ToString();
                Para.EmpNoOrTel = GlobalVariables.LoggedUser.EmpNo;

                Para.FlagMWWY = FlagMWWY ? "1" : "0";
                Para.FlagWDY = FlagWDY ? "1" : "0";
                Para.FlagKDK = FlagKDK ? "1" : "0";
                Para.FlagXSFY = FlagXSFY ? "1" : "0";

                Para.HandOverDate = ReleaseDate.Year + "-" + ReleaseDate.Month + "-" + ReleaseDate.Day;
                Para.HangDate = EntrustDate.Year + "-" + EntrustDate.Month + "-" + EntrustDate.Day;

                CommonRD responseData = await RestSharpService.AddNewHouse(Para, ImageList);

                switch (responseData.Msg)
                {
                    case "CompleteSuccess": 
                        { 
                            CrossToastPopUp.Current.ShowToastSuccess("房源新增成功", ToastLength.Long);
                            await Application.Current.MainPage.Navigation.PopAsync();
                        } break;
                    case "SQLSuccess":
                        {
                            CrossToastPopUp.Current.ShowToastSuccess("房源新增成功", ToastLength.Long);
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        break;
                    case "SQLSuccessButUploadFailed": CrossToastPopUp.Current.ShowToastError("房源数据已加入数据库，但图片上传失败，请在电脑端进行补录操作", ToastLength.Long); break;
                    case "SQLSuccessAndUploadSemiSuccess": CrossToastPopUp.Current.ShowToastError("房源数据已加入数据库，但部分图片上传失败，请在电脑端进行补录操作", ToastLength.Long); break;
                    case "SQLExistProperty": CrossToastPopUp.Current.ShowToastWarning("该房源已存在", ToastLength.Long); break;
                    default: CrossToastPopUp.Current.ShowToastError("房源新增失败", ToastLength.Long); break;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task GetReadPermissionAsync()
        {
            var status = await Tools.CheckAndRequestPermissionAsync(new Permissions.StorageRead());
            if (status != PermissionStatus.Granted)
            {
                CrossToastPopUp.Current.ShowToastMessage("获取存储权限：" + status, ToastLength.Long);
                return;
            }
        }
    }
}
