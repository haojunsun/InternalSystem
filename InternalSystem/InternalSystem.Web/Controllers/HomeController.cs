using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternalSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //首页
        public ActionResult Main()
        {
            return View();
        }
        //视频列表页
        public ActionResult VideoList()
        {
            return View();
        }

        //详情页
        public ActionResult Detail()
        {
            ViewBag.Url = "~/Uploads/1/14098101030102040102.mp4";
            return View();
        }

        //非遗介绍页面
        public ActionResult ICHintroduce()
        {
            return View();
        }

        //引导页
        public ActionResult GuidePage()
        {
            return View();
        }
    }
}