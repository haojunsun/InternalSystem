using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternalSystem.Web.Filters;
using InternalSystem.Web.Helpers;

namespace InternalSystem.Web.Areas.Admin.Controllers
{
    [InternalSystemAuthorize]
    public class HomeController : ControllerHelper
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}