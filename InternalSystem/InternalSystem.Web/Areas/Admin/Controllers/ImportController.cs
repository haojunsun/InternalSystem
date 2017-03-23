using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using InternalSystem.Core;
using InternalSystem.Core.Services;
using InternalSystem.Infrastructure.Services;
using InternalSystem.Web.Helpers;
using InternalSystem.Core.Models;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Text.RegularExpressions;

namespace InternalSystem.Web.Areas.Admin.Controllers
{
    public class ImportController : Controller
    {
        private readonly IHelperServices _helperServices;

        private readonly IWorldHeritageService _worldHeritageService;

        private readonly ILogService _log;
        private readonly IManagerService _managerService;

        public ImportController(IWorldHeritageService worldHeritageService, IHelperServices helperServices, ILogService logService, IManagerService managerService)
        {
            _helperServices = helperServices;
            _log = logService;
            _managerService = managerService;
            _worldHeritageService = worldHeritageService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导入 的post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImportFile()
        {
            string path = HttpContext.Server.MapPath("~/Uploads/");
            //var file = SaveImg(path, Request.Files["file"]);
            //StreamReader sr = new StreamReader(path + file, System.Text.Encoding.Default);
            //string data = sr.ReadToEnd();
            //sr.Close();
            #region 解析 文件 导入 数据 _worldHeritageService

            //解析文件 读取文件 导出datatable
            DirectoryInfo di = new DirectoryInfo(path + "\\fyexcel");
            DirectoryInfo[] dir = di.GetDirectories();//获取子文件夹列表
            var wh = new List<WorldHeritage>();

            foreach (FileInfo file in di.GetFiles())
            {
                Console.WriteLine(file.Name);
                //ExcelHelper eh = new ExcelHelper("C:\\Users\\Administrator\\Desktop\\fyexcel\\" + file.Name);
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();

                dt = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 0, 6, 98);
                dt1 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 1, 5, 10);
                dt2 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 2, 5, 6);
                dt3 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 3, 5, 12);
                dt4 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 4, 2, 3);

                //Console.WriteLine(dt.Rows.count);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var wh1 = new WorldHeritage();
                    var rgcode = dt.Rows[i][1].ToString();
                    if (string.IsNullOrEmpty(rgcode))
                    {
                        break;
                    }
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j == 0)//系统编码
                            wh1.InventoryId = dt.Rows[i][j].ToString();
                        if (j == 1)//人工编码
                        {
                            wh1.ArtificialId = dt.Rows[i][j].ToString();
                            wh1.RelatedPeople_ArtificialID = dt.Rows[i][j].ToString();
                            wh1.FileName = "~/Uploads/importVideo/" + dt.Rows[i][j].ToString() + ".mp4";
                        }
                        if (j == 2)//正题名
                            wh1.TitleProper = dt.Rows[i][j].ToString();
                        if (j == 3)//其他题名
                            wh1.OtherTitle = dt.Rows[i][j].ToString();
                        if (j == 4)//艺术门类1级
                            wh1.FirstLevelOfArtClassification = dt.Rows[i][j].ToString();
                        if (j == 5)//艺术门类2级
                            wh1.SecondLevelOfArtClassification = dt.Rows[i][j].ToString();
                        if (j == 6)//艺术门类3级
                            wh1.ThirdLevelOfArtClassification = dt.Rows[i][j].ToString();
                        if (j == 7)//民族1级
                            wh1.FirstLevelOfEthnicGroup = dt.Rows[i][j].ToString();
                        if (j == 8)//民族2级
                            wh1.SecondLevelOfEthnicGroup = dt.Rows[i][j].ToString();
                        if (j == 9)//民族3级
                            wh1.Subgroup = dt.Rows[i][j].ToString();
                        if (j == 10)//社会属性1级
                            wh1.FirstLevelOfSocialAttributes = dt.Rows[i][j].ToString();
                        if (j == 11)//社会属性2级
                            wh1.SecondLevelOfSocialAttributes = dt.Rows[i][j].ToString();
                        if (j == 12)//时代属性
                            wh1.TimesProperty = dt.Rows[i][j].ToString();
                        if (j == 13)//组成要素1级
                            wh1.FirstLevelOfElements = dt.Rows[i][j].ToString();
                        if (j == 14)//组成要素2级
                            wh1.SecondLevelOfElements = dt.Rows[i][j].ToString();
                        if (j == 15)//组成要素3级
                            wh1.ThirdLevelOfElements = dt.Rows[i][j].ToString();
                        if (j == 16)//角色行当1级
                            wh1.FirstLevelOfCharacter = dt.Rows[i][j].ToString();
                        if (j == 17)//角色行当2级
                            wh1.SecondLevelOfCharacter = dt.Rows[i][j].ToString();
                        if (j == 18)//艺术流派
                            wh1.ArtSchool = dt.Rows[i][j].ToString();
                        if (j == 19)//表演形式
                            wh1.PerformingForm = dt.Rows[i][j].ToString();
                        if (j == 20)//结构体
                            wh1.Structure = dt.Rows[i][j].ToString();
                        if (j == 21)//曲牌名
                            wh1.SongPattern = dt.Rows[i][j].ToString();
                        if (j == 22)//曲牌板式
                            wh1.BeatsPattern = dt.Rows[i][j].ToString();
                        if (j == 23)//律吕宫调 调式
                            wh1.MusicModeName = dt.Rows[i][j].ToString();
                        if (j == 24)//律吕宫调 谱系
                            wh1.GongCheModeName = dt.Rows[i][j].ToString();
                        if (j == 25)//律吕宫调 系统
                            wh1.NumberedMusicalNotationModeName = dt.Rows[i][j].ToString();
                        if (j == 26)//律吕宫调 九宫大
                            wh1.JiuGongDaChengModeName = dt.Rows[i][j].ToString();
                        if (j == 27)//资源属性 一级分类
                            wh1.FirstLevelOfSubjectClassification = dt.Rows[i][j].ToString();
                        if (j == 28)//资源属性 二级分类
                            wh1.SecondLevelOfResourcesClassification = dt.Rows[i][j].ToString();
                        if (j == 29)//资源属性 三级分类
                            wh1.ThirdLevelOfResourcesClassification = dt.Rows[i][j].ToString();
                        if (j == 30)//关键词
                            wh1.Keywords = dt.Rows[i][j].ToString();
                        if (j == 31)//摘要
                            wh1.Abstract = dt.Rows[i][j].ToString();
                        if (j == 32)//奖项名称
                            wh1.NameOfAwards = dt.Rows[i][j].ToString();
                        if (j == 33)//获奖年度
                            wh1.YearOrTimeOfAwards = dt.Rows[i][j].ToString();
                        if (j == 34)//颁奖单位
                            wh1.Awarder = dt.Rows[i][j].ToString();
                        if (j == 35)//类型
                            wh1.Type = dt.Rows[i][j].ToString();
                        if (j == 36)//人物组织名称
                            wh1.Name = dt.Rows[i][j].ToString();
                        if (j == 37)//责任方式
                            wh1.Role = dt.Rows[i][j].ToString();
                        //if (j == 38)//饰演角色
                        //    wh1.TitleProper = dt.Rows[i][j].ToString();
                        if (j == 39)//人物信息 民族
                            wh1.NationalityOfRelatedPeople = dt.Rows[i][j].ToString();
                        if (j == 40)//人物信息 籍贯
                            wh1.BirthplaceOfRelatedPeople = dt.Rows[i][j].ToString();
                        if (j == 41)//人物信息 性别
                            wh1.GenderOfRelatedPeople = dt.Rows[i][j].ToString();
                        if (j == 42)//人物信息 年龄
                            wh1.AgeOfRelatedPeople = dt.Rows[i][j].ToString();
                        if (j == 43)//人物信息 文化程度
                            wh1.EducationLevelOfRelatedPeople = dt.Rows[i][j].ToString();
                        if (j == 44)//人物信息 职业
                            wh1.VocationOfRelatedPeople = dt.Rows[i][j].ToString();
                        if (j == 45)//时间类型 
                            wh1.TemporalType = dt.Rows[i][j].ToString();
                        if (j == 46)//历史纪年 起始
                            wh1.FromHistoricalCalendar = dt.Rows[i][j].ToString();
                        if (j == 47)//历史纪年 结束
                            wh1.ToHistoricalCalendar = dt.Rows[i][j].ToString();
                        if (j == 48)//公元纪年 起始
                            wh1.FromADCalendar = dt.Rows[i][j].ToString();
                        if (j == 49)//公元纪年 结束
                            wh1.ToADCalendar = dt.Rows[i][j].ToString();
                        if (j == 50)//空间类型
                            wh1.SpatialType = dt.Rows[i][j].ToString();
                        if (j == 51)//地域 时地名
                            wh1.HistoricalPlaceName = dt.Rows[i][j].ToString();
                        if (j == 52)//地域 今地名
                            wh1.PresentPlaceName = dt.Rows[i][j].ToString();
                        if (j == 53)//地域 其他地名
                            wh1.OtherPlaceName = dt.Rows[i][j].ToString();
                        if (j == 54)//行政区划 省
                            wh1.Province = dt.Rows[i][j].ToString();
                        if (j == 55)//行政区划 市
                            wh1.City = dt.Rows[i][j].ToString();
                        if (j == 56)//行政区划 区
                            wh1.County = dt.Rows[i][j].ToString();
                        if (j == 57)//行政区划 乡
                            wh1.Town = dt.Rows[i][j].ToString();
                        if (j == 58)//行政区划 村
                            wh1.Village = dt.Rows[i][j].ToString();
                        if (j == 59)//场地
                            wh1.Venues = dt.Rows[i][j].ToString();
                        if (j == 60)//经纬度
                            wh1.LongitudeLatitude = dt.Rows[i][j].ToString();
                        if (j == 61)//语言
                            wh1.Language = dt.Rows[i][j].ToString();
                        if (j == 62)//包含
                            wh1.HasPart = dt.Rows[i][j].ToString();
                        if (j == 63)//包含于
                            wh1.IsPartOf = dt.Rows[i][j].ToString();
                        if (j == 64)//参照
                            wh1.Refer = dt.Rows[i][j].ToString();
                        if (j == 65)//有格式为
                            wh1.HasFormat = dt.Rows[i][j].ToString();
                        if (j == 66)//出处 资料名称
                            wh1.NameReferences = dt.Rows[i][j].ToString();
                        if (j == 67)//资料创建者名称
                            wh1.NameofReferencesCreator = dt.Rows[i][j].ToString();
                        if (j == 68)//资料创建者责任方式
                            wh1.RoleofReferencesCreator = dt.Rows[i][j].ToString();
                        if (j == 69)//国际标准书号
                            wh1.ISBNISRC = dt.Rows[i][j].ToString();
                        if (j == 70)//原资料格式类型
                            wh1.ReferencesFormat = dt.Rows[i][j].ToString();
                        if (j == 71)//获取方式
                            wh1.AcquisitionMethod = dt.Rows[i][j].ToString();
                        if (j == 72)//资料提供者
                            wh1.ProviderOfReferences = dt.Rows[i][j].ToString();
                        if (j == 73)//资料典藏单位
                            wh1.RepositoryName = dt.Rows[i][j].ToString();
                        if (j == 74)//出版者
                            wh1.Publisher = dt.Rows[i][j].ToString();
                        if (j == 75)//出版时间
                            wh1.PublicationDate = dt.Rows[i][j].ToString();
                        if (j == 76)//出版地
                            wh1.PlaceOfPublication = dt.Rows[i][j].ToString();
                        if (j == 77)//数字化格式
                            wh1.DigitalObjectFormat = dt.Rows[i][j].ToString();
                        if (j == 78)//大小
                            wh1.Size = dt.Rows[i][j].ToString();
                        if (j == 79)//时长
                            wh1.Duration = dt.Rows[i][j].ToString();
                        if (j == 80)//分辨率
                            wh1.DigitalSpecification = dt.Rows[i][j].ToString();
                        if (j == 81)//视频数据码率
                            wh1.AudioVideoBitRate = dt.Rows[i][j].ToString();
                        if (j == 82)//音频采样频率
                            wh1.AudioSamplingFrequency = dt.Rows[i][j].ToString();
                        if (j == 83)//声道数
                            wh1.NumberOfChannels = dt.Rows[i][j].ToString();
                        if (j == 84)//存储地址
                            wh1.FilePath = dt.Rows[i][j].ToString();
                        if (j == 85)//显示级别
                            wh1.DisplayType = dt.Rows[i][j].ToString();
                        if (j == 86)//备份光盘
                            wh1.CDNumber = dt.Rows[i][j].ToString();
                        if (j == 87)//授权者
                            wh1.Holder = dt.Rows[i][j].ToString();
                        if (j == 88)//授权起止时间
                            wh1.RightsDate = dt.Rows[i][j].ToString();
                        if (j == 89)//授权条款
                            wh1.Licens = dt.Rows[i][j].ToString();
                        if (j == 90)//著录者
                            wh1.Recorder = dt.Rows[i][j].ToString();
                        if (j == 91)//著录时间
                            wh1.RecordingTime = dt.Rows[i][j].ToString();
                        if (j == 92)//更新记录者
                            wh1.Modifier = dt.Rows[i][j].ToString();
                        if (j == 93)//更新记录时间
                            wh1.ModifiedDate = dt.Rows[i][j].ToString();
                        if (j == 94)//审核者
                            wh1.MetadataAuditor = dt.Rows[i][j].ToString();
                        if (j == 95)//审核时间
                            wh1.MetadataAuditoTime = dt.Rows[i][j].ToString();
                        if (j == 96)//管理机构
                            wh1.MetadataManagement = dt.Rows[i][j].ToString();
                        if (j == 97)//备注
                            wh1.Note = dt.Rows[i][j].ToString();
                    }
                    //附表 人物
                    for (int a = 0; a < dt1.Rows.Count; a++)
                    {
                        if (dt1.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt1.Rows[a][1].ToString() != "")
                                wh1.RelatedPeople_Name = string.IsNullOrEmpty(wh1.RelatedPeople_Name) ? dt1.Rows[a][1].ToString() : wh1.RelatedPeople_Name + ',' + dt1.Rows[a][1].ToString();
                            if (dt1.Rows[a][3].ToString() != "")
                                wh1.RelatedPeople_Role = string.IsNullOrEmpty(wh1.RelatedPeople_Role) ? dt1.Rows[a][3].ToString() : wh1.RelatedPeople_Role + ',' + dt1.Rows[a][3].ToString();
                            if (dt1.Rows[a][4].ToString() != "")
                                wh1.RelatedPeople_NationalityOfRelatedPeople = string.IsNullOrEmpty(wh1.RelatedPeople_NationalityOfRelatedPeople) ? dt1.Rows[a][4].ToString() : wh1.RelatedPeople_NationalityOfRelatedPeople + ',' + dt1.Rows[a][4].ToString();
                            if (dt1.Rows[a][5].ToString() != "")
                                wh1.RelatedPeople_BirthplaceOfRelatedPeople = string.IsNullOrEmpty(wh1.RelatedPeople_BirthplaceOfRelatedPeople) ? dt1.Rows[a][5].ToString() : wh1.RelatedPeople_BirthplaceOfRelatedPeople + ',' + dt1.Rows[a][5].ToString();
                            if (dt1.Rows[a][6].ToString() != "")
                                wh1.RelatedPeople_GenderOfRelatedPeople = string.IsNullOrEmpty(wh1.RelatedPeople_GenderOfRelatedPeople) ? dt1.Rows[a][6].ToString() : wh1.RelatedPeople_GenderOfRelatedPeople + ',' + dt1.Rows[a][6].ToString();
                            if (dt1.Rows[a][7].ToString() != "")
                                wh1.RelatedPeople_AgeOfRelatedPeople = string.IsNullOrEmpty(wh1.RelatedPeople_AgeOfRelatedPeople) ? dt1.Rows[a][7].ToString() : wh1.RelatedPeople_AgeOfRelatedPeople + ',' + dt1.Rows[a][7].ToString();
                            if (dt1.Rows[a][8].ToString() != "")
                                wh1.RelatedPeople_EducationLevelOfRelatedPeople = string.IsNullOrEmpty(wh1.RelatedPeople_EducationLevelOfRelatedPeople) ? dt1.Rows[a][8].ToString() : wh1.RelatedPeople_EducationLevelOfRelatedPeople + ',' + dt1.Rows[a][8].ToString();
                            if (dt1.Rows[a][9].ToString() != "")
                                wh1.RelatedPeople_VocationOfRelatedPeople = string.IsNullOrEmpty(wh1.RelatedPeople_VocationOfRelatedPeople) ? dt1.Rows[a][9].ToString() : wh1.RelatedPeople_VocationOfRelatedPeople + ',' + dt1.Rows[a][9].ToString();
                        }
                    }
                    //附表时间
                    for (int a = 0; a < dt2.Rows.Count; a++)
                    {
                        if (dt2.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt2.Rows[a][0].ToString() != "")
                                wh1.CoverageTemporal_ArtificialID = string.IsNullOrEmpty(wh1.CoverageTemporal_ArtificialID) ? dt2.Rows[a][0].ToString() : wh1.CoverageTemporal_ArtificialID + ',' + dt2.Rows[a][0].ToString();
                            if (dt2.Rows[a][1].ToString() != "")
                                wh1.CoverageTemporal_TemporalType = string.IsNullOrEmpty(wh1.CoverageTemporal_TemporalType) ? dt2.Rows[a][1].ToString() : wh1.CoverageTemporal_TemporalType + ',' + dt2.Rows[a][1].ToString();
                            if (dt2.Rows[a][2].ToString() != "")
                                wh1.CoverageTemporal_FromHistoricalCalendar = string.IsNullOrEmpty(wh1.CoverageTemporal_FromHistoricalCalendar) ? dt2.Rows[a][2].ToString() : wh1.CoverageTemporal_FromHistoricalCalendar + ',' + dt2.Rows[a][2].ToString();
                            if (dt2.Rows[a][3].ToString() != "")
                                wh1.CoverageTemporal_ToHistoricalCalendar = string.IsNullOrEmpty(wh1.CoverageTemporal_ToHistoricalCalendar) ? dt2.Rows[a][3].ToString() : wh1.CoverageTemporal_ToHistoricalCalendar + ',' + dt2.Rows[a][3].ToString();
                            if (dt2.Rows[a][4].ToString() != "")
                                wh1.CoverageTemporal_FromADCalendar = string.IsNullOrEmpty(wh1.CoverageTemporal_FromADCalendar) ? dt2.Rows[a][4].ToString() : wh1.CoverageTemporal_FromADCalendar + ',' + dt2.Rows[a][4].ToString();
                            if (dt2.Rows[a][5].ToString() != "")
                                wh1.CoverageTemporal_ToADCalendar = string.IsNullOrEmpty(wh1.CoverageTemporal_ToADCalendar) ? dt2.Rows[a][5].ToString() : wh1.CoverageTemporal_ToADCalendar + ',' + dt2.Rows[a][5].ToString();
                        }
                    }
                    //附表空间
                    for (int a = 0; a < dt3.Rows.Count; a++)
                    {
                        if (dt3.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt3.Rows[a][0].ToString() != "")
                                wh1.CoverageSpatial_ArtificialID = string.IsNullOrEmpty(wh1.CoverageSpatial_ArtificialID) ? dt3.Rows[a][0].ToString() : wh1.CoverageSpatial_ArtificialID + ',' + dt3.Rows[a][0].ToString();
                            if (dt3.Rows[a][1].ToString() != "")
                                wh1.CoverageSpatial_SpatialType = string.IsNullOrEmpty(wh1.CoverageSpatial_SpatialType) ? dt3.Rows[a][1].ToString() : wh1.CoverageSpatial_SpatialType + ',' + dt3.Rows[a][1].ToString();
                            if (dt3.Rows[a][2].ToString() != "")
                                wh1.CoverageSpatial_HistoricalPlaceName = string.IsNullOrEmpty(wh1.CoverageSpatial_HistoricalPlaceName) ? dt3.Rows[a][2].ToString() : wh1.CoverageSpatial_HistoricalPlaceName + ',' + dt3.Rows[a][2].ToString();
                            if (dt3.Rows[a][3].ToString() != "")
                                wh1.CoverageSpatial_PresentPlaceName = string.IsNullOrEmpty(wh1.CoverageSpatial_PresentPlaceName) ? dt3.Rows[a][3].ToString() : wh1.CoverageSpatial_PresentPlaceName + ',' + dt3.Rows[a][3].ToString();
                            if (dt3.Rows[a][4].ToString() != "")
                                wh1.CoverageSpatial_OtherPlaceName = string.IsNullOrEmpty(wh1.CoverageSpatial_OtherPlaceName) ? dt3.Rows[a][4].ToString() : wh1.CoverageSpatial_OtherPlaceName + ',' + dt3.Rows[a][4].ToString();
                            if (dt3.Rows[a][5].ToString() != "")
                                wh1.CoverageSpatial_province = string.IsNullOrEmpty(wh1.CoverageSpatial_province) ? dt3.Rows[a][5].ToString() : wh1.CoverageSpatial_province + ',' + dt3.Rows[a][5].ToString();
                            if (dt3.Rows[a][6].ToString() != "")
                                wh1.CoverageSpatial_City = string.IsNullOrEmpty(wh1.CoverageSpatial_City) ? dt3.Rows[a][6].ToString() : wh1.CoverageSpatial_City + ',' + dt3.Rows[a][6].ToString();
                            if (dt3.Rows[a][7].ToString() != "")
                                wh1.CoverageSpatial_County = string.IsNullOrEmpty(wh1.CoverageSpatial_County) ? dt3.Rows[a][7].ToString() : wh1.CoverageSpatial_County + ',' + dt3.Rows[a][7].ToString();
                            if (dt3.Rows[a][8].ToString() != "")
                                wh1.CoverageSpatial_Town = string.IsNullOrEmpty(wh1.CoverageSpatial_Town) ? dt3.Rows[a][8].ToString() : wh1.CoverageSpatial_Town + ',' + dt3.Rows[a][8].ToString();
                            if (dt3.Rows[a][9].ToString() != "")
                                wh1.CoverageSpatial_Village = string.IsNullOrEmpty(wh1.CoverageSpatial_Village) ? dt3.Rows[a][9].ToString() : wh1.CoverageSpatial_Village + ',' + dt3.Rows[a][9].ToString();
                            if (dt3.Rows[a][10].ToString() != "")
                                wh1.CoverageSpatial_Venues = string.IsNullOrEmpty(wh1.CoverageSpatial_Venues) ? dt3.Rows[a][10].ToString() : wh1.CoverageSpatial_Venues + ',' + dt3.Rows[a][10].ToString();
                            if (dt3.Rows[a][11].ToString() != "")
                                wh1.CoverageSpatial_LongitudeLatitude = string.IsNullOrEmpty(wh1.CoverageSpatial_LongitudeLatitude) ? dt3.Rows[a][11].ToString() : wh1.CoverageSpatial_LongitudeLatitude + ',' + dt3.Rows[a][11].ToString();
                        }
                    }
                    //附表资料创建者
                    for (int a = 0; a < dt4.Rows.Count; a++)
                    {
                        if (dt4.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt4.Rows[a][0].ToString() != "")
                                wh1.CreatorofReferences_ArtificialID = string.IsNullOrEmpty(wh1.CreatorofReferences_ArtificialID) ? dt4.Rows[a][0].ToString() : wh1.CreatorofReferences_ArtificialID + ',' + dt4.Rows[a][0].ToString();
                            if (dt4.Rows[a][1].ToString() != "")
                                wh1.CreatorofReferences_NameofCreator = string.IsNullOrEmpty(wh1.CreatorofReferences_NameofCreator) ? dt4.Rows[a][1].ToString() : wh1.CreatorofReferences_NameofCreator + ',' + dt4.Rows[a][1].ToString();
                            if (dt4.Rows[a][2].ToString() != "")
                                wh1.CreatorofReferences_RoleofReferencesCreator = string.IsNullOrEmpty(wh1.CreatorofReferences_RoleofReferencesCreator) ? dt4.Rows[a][2].ToString() : wh1.CreatorofReferences_RoleofReferencesCreator + ',' + dt4.Rows[a][2].ToString();
                        }
                    }

                    //wh1.User = _managerService.Get(4);
                    wh1.CreatedUtc = DateTime.Now;
                    wh1.IsShow = 1;
                    wh1.ReleaseDateTime = DateTime.Now.ToString();
                    wh1.HeritageType = 0;
                    wh1.IsEffect = 1;
                    wh.Add(wh1);

                }//for

                //eh.DataTableToExcel(dt, true, @"C:\Users\Administrator\Desktop\模板.xls", pro + "\\" + file.Name, pro);
            }//foreach

            _worldHeritageService.Import(wh);//一次性 录入数据 没测试 导入的时候你测试一下

            #endregion
            return RedirectToAction("List");
        }
        private IWorkbook workbook = null;
        private FileStream fs = null;
        //从excel导出到datatable
        public DataTable ExcelToDataTable(string fileName, int tableIndex, int startRowCount, int totalRowCount)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                //判断excel版本
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003
                    workbook = new HSSFWorkbook(fs);

                //获取sheet 通过参数指定哪个表
                sheet = workbook.GetSheetAt(tableIndex);

                if (sheet != null)
                {
                    int firstrownum = startRowCount;
                    IRow firstRow = sheet.GetRow(firstrownum);
                    int cellCount = totalRowCount;//firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    //添加列头
                    for (int i = 0; i < totalRowCount + 1; i++)
                    {
                        string cellValue = i.ToString();
                        DataColumn column = new DataColumn(cellValue);
                        data.Columns.Add(column);
                    }
                    startRow = startRowCount;

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = 0; j <= cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                            else
                                dataRow[j] = "";
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string SaveImg(string path, HttpPostedFileBase file)
        {
            if (file != null)
            {
                //string nameImg = System.Guid.NewGuid().ToString(); //创建图片新的名称
                string strPath = file.FileName;//获得上传图片的路径
                string type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower(); //获得上传图片的类型(后缀名)
                string filename = file.FileName;//nameImg + "." + type;
                if (ValidateImg(type))
                {
                    string filePath = Path.Combine(path, filename);
                    file.SaveAs(filePath);
                    return filename;
                }
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// 验证图片格式
        /// </summary>
        /// <param name="imgName"></param>
        /// <returns></returns>
        public static bool ValidateImg(string imgName)
        {
            string[] imgType = new string[] { "gif", "jpg", "png", "bmp", "xls", "xlsx", "csv" };

            int i = 0;
            bool blean = false;
            string message = string.Empty;

            //判断是否为Image类型文件
            while (i < imgType.Length)
            {
                if (imgName.Equals(imgType[i].ToString()))
                {
                    blean = true;
                    break;
                }
                else if (i == (imgType.Length - 1))
                {
                    break;
                }
                else
                {
                    i++;
                }
            }
            return blean;
        }
        public ActionResult List(int page = 1, int size = 50)
        {
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;
            var list = _worldHeritageService.List(pageIndex, pageSize, ref totalCount).ToList();
            var personsAsIPagedList = new StaticPagedList<WorldHeritage>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        /// <summary>
        /// 验证信息写入反馈 返回结果类
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ContentResult CResult(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            return Content(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstlevel"></param>
        /// <param name="dataformat"></param>
        /// <param name="nation"></param>
        /// <param name="municipalities"></param>
        /// <param name="title"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
       
        //public ActionResult Search(string firstlevel, string dataformat, string nation, string municipalities, string title, int pageSize, int pageIndex)
        //{
        //    var result = _worldHeritageService.Search(pageIndex, pageSize, firstlevel, dataformat, nation, municipalities, title);
        //    return Json(result, JsonRequestBehavior.DenyGet);
        //}

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="key">关键字（匹配字段 正题名 社会属性一级分类 组成要素一级分类 艺术流派 表演形式 曲牌名 板式 关键字 类型 人物/组织名称） </param>
        /// <param name="firstLevelOfArtClassification">艺术门类一级 </param>
        /// <param name="secondLevelOfEthnicGroup">民族-二级</param>
        /// <param name="type">类型Type</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NewSearch(string key,string firstLevelOfArtClassification,string secondLevelOfEthnicGroup,string type, int pageSize, int pageIndex)
        {
            var result = _worldHeritageService.NewSearch( key, firstLevelOfArtClassification, secondLevelOfEthnicGroup, type,  pageSize,  pageIndex);
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Find(int id)
        {
            var result = _worldHeritageService.Get(id);
            return Json(result, JsonRequestBehavior.DenyGet);
        }

    }
}