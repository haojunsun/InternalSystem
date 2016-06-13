using System;
using System.Collections.Generic;
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

        [HttpPost]
        public ActionResult ImportFile()
        {
            return View();
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