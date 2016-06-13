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

            var file = SaveImg(path, Request.Files["file"]);
            StreamReader sr = new StreamReader(path + file, System.Text.Encoding.Default);
            string data = sr.ReadToEnd();
            sr.Close();
            #region 解析 文件 导入 数据 _worldHeritageService

            var wh = new WorldHeritage();
            _worldHeritageService.Add(wh);

            #endregion
            return RedirectToAction("List");
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