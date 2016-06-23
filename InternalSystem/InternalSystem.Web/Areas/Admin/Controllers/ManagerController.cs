﻿using System;
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
        public ActionResult Create(string channelTag, string columnTag, int tagtype, string title, string body, int isDraft)
        {
            var manager = new Manager();
            //article.Body = body.Replace(" ", "&nbsp").Replace("\r\n", "<br />");
            //article.Title = title;
            //article.CreatedUtc = DateTime.Now;
            //article.ChannelTags = channelTag;
            //article.ColumnTags = columnTag;
            //article.IsDraft = isDraft;
            //article.IsRelease = article.IsDraft == 0 ? 1 : 0;
            //article.ManagerName = _simpleAccount.GetUserElement().Name;
            HttpPostedFileBase hp = Request.Files["file1"];
            if (Request.Files.Count > 0)
            {
                //article.TitleImageUrl = _helperServices.UpLoadImg("file", ""); //获取上传图片 
                //if (hp != null)
                //{
                //    article.OtherImageUrl = _helperServices.UpLoadFile("file1", ""); //获取上传文件
                //}
                //if (string.IsNullOrEmpty(article.TitleImageUrl))
                //    return Content("<script>alert('图片不能为空');window.location.href='" + Url.Action("Create", new
                //    {
                //        ViewBag.channelTag,
                //        ViewBag.columnTag,
                //        @tagtype = 1
                //    }) + "';</script>");
            }
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

        public ActionResult ModifyPassword()
        {
            return View();
        }

    }
}