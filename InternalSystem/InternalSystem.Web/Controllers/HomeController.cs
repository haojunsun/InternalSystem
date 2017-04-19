using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternalSystem.Web.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }
        public ActionResult ImgList()
        {
            return View();
        }

        public ActionResult ImgDetail()
        {
            return View();
        }
        public ActionResult MusicList()
        {
            return View();
        }

        public ActionResult MusicDetail()
        {
            return View();
        }
        public ActionResult TextList()
        {
            return View();
        }

        public ActionResult TextDetail()
        {
            return View();
        }
        public ActionResult VideoNList()
        {
            return View();
        }

        public ActionResult VideoDetail()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}