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
    //[OutputCache(Duration = 10)]
    [Authorize]
    public class ArticleController : ControllerHelper
    {
        private readonly ISimpleAccountManager _simpleAccount;
        private readonly IArticleService _articleService;
        private readonly IHelperServices _helperServices;

        public ArticleController(ISimpleAccountManager simpleAccount, IArticleService articleService, IHelperServices helperServices)
            : base(simpleAccount, articleService)
        {
            _simpleAccount = simpleAccount;
            _articleService = articleService;
            _helperServices = helperServices;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelTag"></param>
        /// <param name="columnTag"></param>
        /// <param name="tagtype">0 频道 1 栏目</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ActionResult List(string channelTag, string columnTag, int tagtype, int page = 1, int size = 20)
        {
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;

            var list = new List<Article>();
            ViewBag.channelTag = channelTag;
            ViewBag.columnTag = columnTag;
            ViewBag.tagtype = tagtype;
            list = tagtype == 0 ? _articleService.GetByChannelTag(channelTag, pageIndex, pageSize, ref totalCount).ToList() : _articleService.GetByColumnTag(columnTag, pageIndex, pageSize, ref totalCount).ToList();
            var personsAsIPagedList = new StaticPagedList<Article>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        public ActionResult Delete(int id)
        {
            var old = _articleService.Get(id);
            if (old == null)
                return JumpUrl("List", "id错误");
            _articleService.Delete(id);
            return Content("<script>alert('删除成功');window.location.href='" + Url.Action("List", new
            {
                channelTag = old.ChannelTags,
                columnTag = old.ColumnTags,
                @tagtype = 1
            }) + "';</script>");
        }

        [HttpGet]
        public ActionResult Detail(int id = 0)
        {
            var article = _articleService.Get(id);
            return View(article);
        }

        [HttpGet]
        public ActionResult Create(string channelTag, string columnTag, int tagtype)
        {
            ViewBag.channelTag = channelTag;
            ViewBag.columnTag = columnTag;
            ViewBag.tagtype = tagtype;
            return View();
        }

        [HttpPost]
        public ActionResult Create(string channelTag, string columnTag, int tagtype, string title, string body, int isDraft)
        {
            var article = new Article();
            article.Body = body.Replace(" ", "&nbsp").Replace("\r\n", "<br />");
            article.Title = title;
            article.CreatedUtc = DateTime.Now;
            article.ChannelTags = channelTag;
            article.ColumnTags = columnTag;
            article.IsDraft = isDraft;
            article.IsRelease = article.IsDraft == 0 ? 1 : 0;
            article.ManagerName = _simpleAccount.GetUserElement().Name;
            HttpPostedFileBase hp = Request.Files["file1"];
            if (Request.Files.Count > 0)
            {
                article.TitleImageUrl = _helperServices.UpLoadImg("file", ""); //获取上传图片 
                if (hp != null)
                {
                    article.OtherImageUrl = _helperServices.UpLoadFile("file1", ""); //获取上传文件
                }
                //if (string.IsNullOrEmpty(article.TitleImageUrl))
                //    return Content("<script>alert('图片不能为空');window.location.href='" + Url.Action("Create", new
                //    {
                //        ViewBag.channelTag,
                //        ViewBag.columnTag,
                //        @tagtype = 1
                //    }) + "';</script>");
            }
            _articleService.Add(article);
            ViewBag.channelTag = channelTag;
            ViewBag.columnTag = columnTag;
            ViewBag.tagtype = tagtype;
            return Content("<script>alert('创建" + ViewBag.columnTag + "-" + ViewBag.columnTag + "内容成功');window.location.href='" + Url.Action("List", new
            {
                ViewBag.channelTag,
                ViewBag.columnTag,
                @tagtype = 1
            }) + "';</script>");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var article = _articleService.Get(id);
            if (!string.IsNullOrEmpty(article.Body))
            {
                article.Body = article.Body.Replace("<br />", "\r\n").Replace("&nbsp", " ");
            }
            return View(article);
        }

        [HttpPost]
        public ActionResult Edit(Article article)
        {
            var old = _articleService.Get(article.ArticleId);
            old.Title = article.Title;
            old.Body = article.Body.Replace(" ", "&nbsp").Replace("\r\n", "<br />"); ;
            old.ManagerName = _simpleAccount.GetUserElement().Name;
            old.IsDraft = article.IsDraft;
            old.IsRelease = old.IsDraft == 0 ? 1 : 0;
            HttpPostedFileBase hp = Request.Files["file1"];

            if (Request.Files.Count > 0)
            {
                article.TitleImageUrl = _helperServices.UpLoadImg("file", ""); //获取上传图片 
                if (!string.IsNullOrEmpty(article.TitleImageUrl))
                    old.TitleImageUrl = article.TitleImageUrl;
                if (hp != null)
                {
                    article.OtherImageUrl = _helperServices.UpLoadFile("file1", ""); //获取上传文件
                    if (!string.IsNullOrEmpty(article.OtherImageUrl))
                        old.OtherImageUrl = article.OtherImageUrl;
                }

            }
            _articleService.Update(old);

            return Content("<script>alert('编辑" + ViewBag.columnTag + "-" + ViewBag.columnTag + "内容成功');window.location.href='" + Url.Action("List", new
            {
                channelTag = old.ChannelTags,
                columnTag = old.ColumnTags,
                @tagtype = 1
            }) + "';</script>");
        }


        public ActionResult Release(int id)
        {
            var article = _articleService.Get(id);
            article.IsDraft = article.IsDraft == 1 ? 0 : 1;
            article.IsRelease = article.IsDraft == 0 ? 1 : 0;
            _articleService.Update(article);
            return Content("<script>alert('操作" + ViewBag.columnTag + "-" + ViewBag.columnTag + "内容成功');window.location.href='" + Url.Action("List", new
            {
                channelTag = article.ChannelTags,
                columnTag = article.ColumnTags,
                @tagtype = 1
            }) + "';</script>");
        }
    }
}