using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternalSystem.Core.Services;
using InternalSystem.Infrastructure.Services;
using InternalSystem.Web.Helpers;

namespace InternalSystem.Web.Areas.Admin.Controllers
{

    public class SqlDependencyController  : ControllerHelper
    {
        private readonly ISimpleAccountManager _simpleAccount;
        private readonly IArticleService _articleService;
        private readonly IHelperServices _helperServices;

        public SqlDependencyController(ISimpleAccountManager simpleAccount, IArticleService articleService, IHelperServices helperServices)
            : base(simpleAccount, articleService)

        {
            _simpleAccount = simpleAccount;
            _articleService = articleService;
            _helperServices = helperServices;
        }

        [OutputCache(CacheProfile = "SqlDependencyCache")]
        // GET: Admin/SqlDependency
        public ActionResult Index()
        {
            var list = _articleService.List().OrderByDescending(x => x.CreatedUtc).Take(10).ToList();
            ViewBag.CurrentTime = System.DateTime.Now;
            return View(list);
        }
    }
}