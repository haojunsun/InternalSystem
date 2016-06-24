using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using InternalSystem.Core;
using InternalSystem.Core.Basis;
using InternalSystem.Core.Services;
using InternalSystem.Infrastructure.Services;
using InternalSystem.Web.Helpers;
using InternalSystem.Core.Models;
using InternalSystem.Web.Filters;


namespace InternalSystem.Web.Areas.Admin.Controllers
{
    [InternalSystemAuthorize]
    public class ManagerController : ControllerHelper
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;

        public ManagerController(IManagerService managerService, IHelperServices helperServices)
        {
            _managerService = managerService;
            _helperServices = helperServices;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ActionResult List(int page = 1, int size = 20)
        {
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;

            var list = new List<Manager>();
            list = _managerService.List(pageIndex, pageSize, ref totalCount).ToList();
            var personsAsIPagedList = new StaticPagedList<Manager>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        public ActionResult Delete(int id)
        {
            var old = _managerService.Get(id);
            if (old == null)
                return JumpUrl("List", "id错误");
            _managerService.Delete(id);
            return Content("<script>alert('删除成功');window.location.href='" + Url.Action("List") + "';</script>");
        }

        [HttpGet]
        public ActionResult Detail(int id = 0)
        {
            var article = _managerService.Get(id);
            return View(article);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, string pass)
        {
            var manager = new Manager();
            manager.Authority = 1;
            manager.Invalid = 0;
            manager.Name = name;
            manager.LoginId = name;
            manager.Pass = _helperServices.MD5Encrypt(pass);
            manager.CreatedUtc=DateTime.Now;
            _managerService.Add(manager);

            return Content("<script>alert('创建管理员成功');window.location.href='" + Url.Action("List") + "';</script>");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var article = _managerService.Get(id);
            //if (!string.IsNullOrEmpty(article.Body))
            //{
            //    article.Body = article.Body.Replace("<br />", "\r\n").Replace("&nbsp", " ");
            //}
            return View(article);
        }

        [HttpPost]
        public ActionResult Edit(Article article)
        {
            var old = _managerService.Get(article.ArticleId);
            //old.Title = article.Title;
            //old.Body = article.Body.Replace(" ", "&nbsp").Replace("\r\n", "<br />"); ;
            //old.ManagerName = _simpleAccount.GetUserElement().Name;
            //old.IsDraft = article.IsDraft;
            //old.IsRelease = old.IsDraft == 0 ? 1 : 0;
            HttpPostedFileBase hp = Request.Files["file1"];

            //if (Request.Files.Count > 0)
            //{
            //    article.TitleImageUrl = _helperServices.UpLoadImg("file", ""); //获取上传图片 
            //    if (!string.IsNullOrEmpty(article.TitleImageUrl))
            //        old.TitleImageUrl = article.TitleImageUrl;
            //    if (hp != null)
            //    {
            //        article.OtherImageUrl = _helperServices.UpLoadFile("file1", ""); //获取上传文件
            //        if (!string.IsNullOrEmpty(article.OtherImageUrl))
            //            old.OtherImageUrl = article.OtherImageUrl;
            //    }

            //}
            _managerService.Update(old);

            return Content("<script>alert('编辑管理员成功');window.location.href='" + Url.Action("List") + "';</script>");
        }

        public ActionResult ModifyPassword(int id=0)
        {
            if (id == 0)
            {
                var user = UserLogin.GetUserInfo();
                return View(user);
            }
            else
            {
                var user = _managerService.Get(id);
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult ModifyPassword(Manager user,string pass)
        {
            var old = _managerService.Get(user.ManagerId);
            old.Pass = _helperServices.MD5Encrypt(pass);
            _managerService.Update(old);
            return Content("<script>alert('编辑内容成功');</script>");
        }

        public ActionResult Invalid(int id, int state)
        {
            var user = UserLogin.GetUserInfo();
            if (user.Authority != 0)
            {
                return Content("<script>alert('无权限');window.location.href='" + Url.Action("List") + "';</script>");
            }
            var old = _managerService.Get(id);
            if (old == null)
                return JumpUrl("List", "id错误");

            old.Invalid = state;
            _managerService.Update(old);
            return Content("<script>alert('编辑完成');window.location.href='" + Url.Action("List") + "';</script>");
        }
    }





}