using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternalSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int? exLevel)
        {
            ViewBag.ExLevel = exLevel;
            return View();
        }

        public ActionResult Main(int? exLevel)
        {
            ViewBag.ExLevel = exLevel;
            return View();
        }
    }
}