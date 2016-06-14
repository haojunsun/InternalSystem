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

namespace InternalSystem.Web.Areas.Admin.Controllers
{
    public class ImportController : Controller
    {
        private readonly IHelperServices _helperServices;

        private readonly IWorldHeritageService _worldHeritageService;

        private readonly ILogService _log;
        public ImportController(IWorldHeritageService worldHeritageService, IHelperServices helperServices, ILogService logService)
        {
            _helperServices = helperServices;
            _log = logService;
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
            DirectoryInfo di = new DirectoryInfo(path+"\\fyexcel");
            DirectoryInfo[] dir = di.GetDirectories();//获取子文件夹列表
            var wh = new List<WorldHeritage>();

            foreach (FileInfo file in di.GetFiles())
            {
                Console.WriteLine(file.Name);
                //ExcelHelper eh = new ExcelHelper("C:\\Users\\Administrator\\Desktop\\fyexcel\\" + file.Name);
                DataTable dt = new DataTable();
                dt = ExcelToDataTable(path + "\\fyexcel\\" + file.Name);
                //Console.WriteLine(dt.Rows.count);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var wh1 = new WorldHeritage();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j == 0)
                            wh1.InventoryId = dt.Rows[i][j].ToString();
                        if (j == 1)
                            wh1.ArtificialId = dt.Rows[i][j].ToString();
                        if (j == 2)
                            wh1.TitleProper = dt.Rows[i][j].ToString();
                        if (j == 3)
                            wh1.Title = dt.Rows[i][j].ToString();
                        if (j == 4)
                            wh1.FirstLevel = dt.Rows[i][j].ToString();
                        if (j == 5)
                            wh1.SecondLevel = dt.Rows[i][j].ToString();
                        if (j == 6)
                            wh1.ProjectCode = dt.Rows[i][j].ToString();
                        if (j == 7)
                            wh1.SubItemNumber = dt.Rows[i][j].ToString();
                        if (j == 8)
                            wh1.Batch = dt.Rows[i][j].ToString();
                        if (j == 9)
                            wh1.Notes = dt.Rows[i][j].ToString();
                        if (j == 10)
                            wh1.ApplicationTime = dt.Rows[i][j].ToString();
                        if (j == 11)
                            wh1.Nation = dt.Rows[i][j].ToString();
                        if (j == 12)
                            wh1.NationalBranch = dt.Rows[i][j].ToString();
                        if (j == 13)
                            wh1.Gender = dt.Rows[i][j].ToString();
                        if (j == 14)
                            wh1.Birth = dt.Rows[i][j].ToString();
                        if (j == 15)
                            wh1.Education = dt.Rows[i][j].ToString();
                        if (j == 16)
                            wh1.Occupation = dt.Rows[i][j].ToString();
                        if (j == 17)
                            wh1.Successor = dt.Rows[i][j].ToString();
                        if (j == 19)
                            wh1.Genre = dt.Rows[i][j].ToString();
                        if (j == 20)
                            wh1.Municipalities = dt.Rows[i][j].ToString();
                        if (j == 21)
                            wh1.Region = dt.Rows[i][j].ToString();
                        if (j == 22)
                            wh1.CountyLevelCity = dt.Rows[i][j].ToString();
                        if (j == 23)
                            wh1.Township = dt.Rows[i][j].ToString();
                        if (j == 24)
                            wh1.Village = dt.Rows[i][j].ToString();
                        if (j == 25)
                            wh1.DeclarationArea = dt.Rows[i][j].ToString();
                        if (j == 26)
                            wh1.ProtectionUnit = dt.Rows[i][j].ToString();
                        if (j == 27)
                            wh1.EcologicalZoneProject = dt.Rows[i][j].ToString();
                        if (j == 28)
                            wh1.ProductiveProtectionBase = dt.Rows[i][j].ToString();
                        if (j == 29)
                            wh1.ProvinceCode = dt.Rows[i][j].ToString();
                        if (j == 30)
                            wh1.PostTime = dt.Rows[i][j].ToString();
                        if (j == 31)
                            wh1.Death = dt.Rows[i][j].ToString();
                        if (j == 32)
                            wh1.Adopt = dt.Rows[i][j].ToString();
                        if (j == 33)
                            wh1.Language = dt.Rows[i][j].ToString();
                        if (j == 34)
                            wh1.CreatorName = dt.Rows[i][j].ToString();
                        if (j == 35)
                            wh1.ResponsibilityCreator = dt.Rows[i][j].ToString();
                        if (j == 36)
                            wh1.DataFormat = dt.Rows[i][j].ToString();
                        if (j == 37)
                            wh1.DataProvider = dt.Rows[i][j].ToString();
                        if (j == 38)
                            wh1.CollectionUnit = dt.Rows[i][j].ToString();
                        if (j == 39)
                            wh1.DigitalFormat = dt.Rows[i][j].ToString();
                        if (j == 40)
                            wh1.Size = dt.Rows[i][j].ToString();
                        if (j == 41)
                            wh1.Duration = dt.Rows[i][j].ToString();
                        if (j == 42)
                            wh1.ResolvingPower = dt.Rows[i][j].ToString();
                        if (j == 43)
                            wh1.KBps = dt.Rows[i][j].ToString();
                        if (j == 44)
                            wh1.SamplingFrequency = dt.Rows[i][j].ToString();
                        if (j == 45)
                            wh1.Channels = dt.Rows[i][j].ToString();
                        if (j == 46)
                            wh1.StoragePlace = dt.Rows[i][j].ToString();
                        if (j == 47)
                            wh1.DisplayLevel = dt.Rows[i][j].ToString();
                        if (j == 48)
                            wh1.Description = dt.Rows[i][j].ToString();
                        if (j == 49)
                            wh1.DescriptionTime = dt.Rows[i][j].ToString();
                        if (j == 50)
                            wh1.Audit = dt.Rows[i][j].ToString();
                        if (j == 51)
                            wh1.AuditTime = dt.Rows[i][j].ToString();
                        if (j == 52)
                            wh1.Organization = dt.Rows[i][j].ToString();
                        if (j == 53)
                            wh1.Remarks = dt.Rows[i][j].ToString();
                        if (j == 54)
                            wh1.url = dt.Rows[i][j].ToString();
                    }
                    wh.Add(wh1);
                }
                //eh.DataTableToExcel(dt, true, @"C:\Users\Administrator\Desktop\模板.xls", pro + "\\" + file.Name, pro);
            }

            _worldHeritageService.Import(wh);//一次性 录入数据 没测试 导入的时候你测试一下

            #endregion
            return RedirectToAction("List");
        }
        private IWorkbook workbook = null;
        private FileStream fs = null;
        //从excel导出到datatable
        public DataTable ExcelToDataTable(string fileName)
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

                //获取sheet 默认第一个
                sheet = workbook.GetSheetAt(0);

                if (sheet != null)
                {
                    int firstrownum = 5;
                    IRow firstRow = sheet.GetRow(firstrownum);
                    int cellCount = 55;//firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    //添加列头
                    for (int i = 0; i < 56; i++)
                    {
                        string cellValue = i.ToString();
                        DataColumn column = new DataColumn(cellValue);
                        data.Columns.Add(column);
                    }
                    startRow = 5;

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

    }
}