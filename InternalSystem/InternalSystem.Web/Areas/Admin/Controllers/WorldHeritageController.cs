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
        private readonly IManagerService _managerService;

        public WorldHeritageController(IHelperServices helperServices, IWorldHeritageService worldHeritageService, ILogService log, IManagerService managerService)
        {
            _helperServices = helperServices;
            _worldHeritageService = worldHeritageService;
            _log = log;
            _managerService = managerService;
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

        [HttpPost]
        public ActionResult Create(WorldHeritage wh)
        {
            wh.HeritageType = 1;
            if (Request.Files.Count > 0)
            {
                wh.FileName = _helperServices.UpLoadImg("file", ""); //获取上传图片 
                wh.HeritageType = 2;
            }
            wh.CreatedUtc = DateTime.Now;
            wh.User = _managerService.Get(UserLogin.GetUserInfo().ManagerId);
            wh.IsEffect = 0;
            if (!string.IsNullOrEmpty(wh.Content))
                wh.Content = wh.Content.Replace(" ", "&nbsp").Replace("\r\n", "<br />");

            _worldHeritageService.Add(wh);
            return RedirectToAction("My");
        }

        public ActionResult Edit(int id)
        {
            var wh = _worldHeritageService.Get(id);
            if (!string.IsNullOrEmpty(wh.Content))
            {
                wh.Content = wh.Content.Replace("<br />", "\r\n").Replace("&nbsp", " ");
            }
            if (!string.IsNullOrEmpty(wh.Remarks))
            {
                wh.Remarks = wh.Remarks.Replace("<br />", "\r\n").Replace("&nbsp", " ");
            }
            return View(wh);
        }

        [HttpPost]
        public ActionResult Edit(WorldHeritage wh)
        {
            var old = _worldHeritageService.Get(wh.WorldHeritageId);
            old.Title = wh.Title;
            old.InventoryId = wh.InventoryId;
            old.TitleProper = wh.TitleProper;
            old.Title = wh.Title;
            old.FirstLevel = wh.FirstLevel;
            old.SecondLevel = wh.SecondLevel;
            old.ProjectCode = wh.ProjectCode;
            old.SubItemNumber = wh.SubItemNumber;
            old.Batch = wh.Batch;
            old.Notes = wh.Notes;
            old.ApplicationTime = wh.ApplicationTime;
            old.Nation = wh.Nation;
            old.NationalBranch = wh.NationalBranch;
            old.Gender = wh.Gender;
            old.Birth = wh.Birth;
            old.Education = wh.Education;
            old.Occupation = wh.Occupation;
            old.Successor = wh.Successor;
            old.Genre = wh.Genre;
            old.Municipalities = wh.Municipalities;
            old.Region = wh.Region;
            old.CountyLevelCity = wh.CountyLevelCity;
            old.Township = wh.Township;
            old.Village = wh.Village;
            old.DeclarationArea = wh.DeclarationArea;
            old.ProtectionUnit = wh.ProtectionUnit;
            old.EcologicalZoneProject = wh.EcologicalZoneProject;
            old.ProductiveProtectionBase = wh.ProductiveProtectionBase;
            old.ProvinceCode = wh.ProvinceCode;
            old.PostTime = wh.PostTime;
            old.Death = wh.Death;
            old.Adopt = wh.Adopt;
            old.Language = wh.Language;
            old.CreatorName = wh.CreatorName;
            old.ResponsibilityCreator = wh.ResponsibilityCreator;
            old.DataFormat = wh.DataFormat;
            old.DataProvider = wh.DataProvider;
            old.CollectionUnit = wh.CollectionUnit;
            old.DigitalFormat = wh.DigitalFormat;
            old.Size = wh.Size;
            old.Duration = wh.Duration;
            old.ResolvingPower = wh.ResolvingPower;
            old.KBps = wh.KBps;
            old.SamplingFrequency = wh.SamplingFrequency;
            old.Channels = wh.Channels;
            old.StoragePlace = wh.StoragePlace;
            old.DisplayLevel = wh.DisplayLevel;
            old.Description = wh.Description;
            old.DescriptionTime = wh.DescriptionTime;
            old.Organization = wh.Organization;
            old.Remarks = wh.Remarks;


            if (!string.IsNullOrEmpty(wh.Content))
                old.Content = wh.Content.Replace(" ", "&nbsp").Replace("\r\n", "<br />");

            if (Request.Files.Count > 0)
            {
                old.FileName = _helperServices.UpLoadImg("file", ""); //获取上传图片 
                old.HeritageType = 2;
            }

            _worldHeritageService.Update(old);
            return Content("<script>alert('编辑内容成功');window.location.href='" + Url.Action("My") + "';</script>");
        }

        public ActionResult HasCode(string code)
        {
            var result = _worldHeritageService.FindByCode(code);
            if (result != null)
                return CResult(200, "重复");
            return CResult(200, "");
        }

        public ActionResult Detail(int id)
        {
            var wh = _worldHeritageService.Get(id);
            return View(wh);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var old = _worldHeritageService.Get(id);
            if (old == null)
                return JumpUrl("My", "id错误");
            _worldHeritageService.Delete(id);
            return Content("<script>alert('删除成功');window.location.href='" + Url.Action("My") + "';</script>");
        }
    }
}