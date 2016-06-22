using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternalSystem.Core.Basis;
using InternalSystem.Core.Models;
using InternalSystem.Core.Services;
using InternalSystem.Infrastructure.Services;
using InternalSystem.Web.Filters;
using InternalSystem.Web.Helpers;
using PagedList;

namespace InternalSystem.Web.Areas.Admin.Controllers
{
    [InternalSystemAuthorize]
    public class WorldHeritageController : ControllerHelper
    {
        private readonly IHelperServices _helperServices;

        private readonly IWorldHeritageService _worldHeritageService;

        private readonly ILogService _log;

        public WorldHeritageController(IHelperServices helperServices, IWorldHeritageService worldHeritageService, ILogService log)
        {
            _helperServices = helperServices;
            _worldHeritageService = worldHeritageService;
            _log = log;
        }

        /// <summary>
        /// 我的资源
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ActionResult My(int page = 1, int size = 50)
        {
            var user = UserLogin.GetUserInfo();
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;
            var list = _worldHeritageService.My(user.ManagerId, pageIndex, pageSize, ref totalCount).ToList();
            var personsAsIPagedList = new StaticPagedList<WorldHeritage>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}