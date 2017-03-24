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
using System.IO;

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
            if (!string.IsNullOrEmpty(wh.FileName))
            {
                wh.HeritageType = 0;
                wh.DataFormat = "视频";
                wh.FileName = "~/Uploads/video/" + wh.FileName;
            }
            else
            {
                wh.HeritageType = 0;
                if (Request.Files.Count > 0)
                {
                    wh.FileName = _helperServices.UpLoadImg("file", ""); //获取上传图片 
                    if (!string.IsNullOrEmpty(wh.FileName))
                    {
                        wh.HeritageType = 2;
                        wh.DataFormat = "图片";
                    }
                }
                if (!string.IsNullOrEmpty(wh.Content))
                {
                    wh.HeritageType = 1;
                    wh.DataFormat = "文本";
                    wh.Content = wh.Content.Replace(" ", "&nbsp").Replace("\r\n", "<br />");
                }
            }
            wh.CreatedUtc = DateTime.Now;
            wh.User = _managerService.Get(UserLogin.GetUserInfo().ManagerId);
            wh.IsEffect = 0;
            //wh.Description = _managerService.Get(UserLogin.GetUserInfo().ManagerId).Name;
            ////wh.DescriptionTime = DateTime.Now.ToString();
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
            if (!string.IsNullOrEmpty(wh.Note))
            {
                wh.Note = wh.Note.Replace("<br />", "\r\n").Replace("&nbsp", " ");
            }
            return View(wh);
        }

        [HttpPost]
        public ActionResult Edit(WorldHeritage wh)
        {
            var old = _worldHeritageService.Get(wh.WorldHeritageId);
            old.InventoryId = wh.InventoryId;
            old.TitleProper = wh.TitleProper;
            old.ArtificialId = wh.ArtificialId;
            old.TitleProper = wh.TitleProper;

            old.OtherTitle = wh.OtherTitle;
            old.FirstLevelOfArtClassification = wh.FirstLevelOfArtClassification;
            old.SecondLevelOfArtClassification = wh.SecondLevelOfArtClassification;

            old.ThirdLevelOfArtClassification = wh.ThirdLevelOfArtClassification;
            old.FirstLevelOfEthnicGroup = wh.FirstLevelOfEthnicGroup;
            old.SecondLevelOfEthnicGroup = wh.SecondLevelOfEthnicGroup;

            old.Subgroup = wh.Subgroup;
            old.FirstLevelOfSocialAttributes = wh.FirstLevelOfSocialAttributes;
            old.SecondLevelOfSocialAttributes = wh.SecondLevelOfSocialAttributes;

            old.TimesProperty = wh.TimesProperty;
            old.FirstLevelOfElements = wh.FirstLevelOfElements;
            old.SecondLevelOfElements = wh.SecondLevelOfElements;

            old.ThirdLevelOfElements = wh.ThirdLevelOfElements;
            old.FirstLevelOfCharacter = wh.FirstLevelOfCharacter;
            old.SecondLevelOfCharacter = wh.SecondLevelOfCharacter;

            old.ArtSchool = wh.ArtSchool;
            old.PerformingForm = wh.PerformingForm;
            old.Structure = wh.Structure;

            old.SongPattern = wh.SongPattern;
            old.BeatsPattern = wh.BeatsPattern;
            old.MusicModeName = wh.MusicModeName;

            old.GongCheModeName = wh.GongCheModeName;
            old.NumberedMusicalNotationModeName = wh.NumberedMusicalNotationModeName;
            old.JiuGongDaChengModeName = wh.JiuGongDaChengModeName;

            old.FirstLevelOfSubjectClassification = wh.FirstLevelOfSubjectClassification;
            old.SecondLevelOfResourcesClassification = wh.SecondLevelOfResourcesClassification;
            old.ThirdLevelOfResourcesClassification = wh.ThirdLevelOfResourcesClassification;

            old.Keywords = wh.Keywords;
            old.Abstract = wh.Abstract;
            old.NameOfAwards = wh.NameOfAwards;

            old.YearOrTimeOfAwards = wh.YearOrTimeOfAwards;
            old.Awarder = wh.Awarder;
            old.Type = wh.Type;

            old.Name = wh.Name;
            old.Role = wh.Role;
            old.NationalityOfRelatedPeople = wh.NationalityOfRelatedPeople;

            old.BirthplaceOfRelatedPeople = wh.BirthplaceOfRelatedPeople;
            old.GenderOfRelatedPeople = wh.GenderOfRelatedPeople;
            old.AgeOfRelatedPeople = wh.AgeOfRelatedPeople;

            old.EducationLevelOfRelatedPeople = wh.EducationLevelOfRelatedPeople;
            old.VocationOfRelatedPeople = wh.VocationOfRelatedPeople;
            old.TemporalType = wh.TemporalType;

            old.FromHistoricalCalendar = wh.FromHistoricalCalendar;
            old.ToHistoricalCalendar = wh.ToHistoricalCalendar;
            old.FromADCalendar = wh.FromADCalendar;

            old.ToADCalendar = wh.ToADCalendar;
            old.SpatialType = wh.SpatialType;
            old.HistoricalPlaceName = wh.HistoricalPlaceName;

            old.PresentPlaceName = wh.PresentPlaceName;
            old.OtherPlaceName = wh.OtherPlaceName;
            old.Province = wh.Province;

            old.City = wh.City;
            old.County = wh.County;
            old.Town = wh.Town;

            old.Village = wh.Village;
            old.Venues = wh.Venues;
            old.LongitudeLatitude = wh.LongitudeLatitude;

            old.Language = wh.Language;
            old.HasPart = wh.HasPart;
            old.IsPartOf = wh.IsPartOf;

            old.Refer = wh.Refer;
            old.HasFormat = wh.HasFormat;

            old.NameReferences = wh.NameReferences;
            old.NameofReferencesCreator = wh.NameofReferencesCreator;
            old.RoleofReferencesCreator = wh.RoleofReferencesCreator;

            old.ISBNISRC = wh.ISBNISRC;
            old.ReferencesFormat = wh.ReferencesFormat;
            old.AcquisitionMethod = wh.AcquisitionMethod;

            old.ProviderOfReferences = wh.ProviderOfReferences;
            old.RepositoryName = wh.RepositoryName;
            old.Publisher = wh.Publisher;

            old.PublicationDate = wh.PublicationDate;
            old.PlaceOfPublication = wh.PlaceOfPublication;
            old.DigitalObjectFormat = wh.DigitalObjectFormat;

            old.Size = wh.Size;
            old.Duration = wh.Duration;
            old.DigitalSpecification = wh.DigitalSpecification;

            old.AudioVideoBitRate = wh.AudioVideoBitRate;
            old.AudioSamplingFrequency = wh.AudioSamplingFrequency;
            old.NumberOfChannels = wh.NumberOfChannels;

            old.FilePath = wh.FilePath;
            old.DisplayType = wh.DisplayType;
            old.CDNumber = wh.CDNumber;

            old.Holder = wh.Holder;
            old.RightsDate = wh.RightsDate;
            old.Licens = wh.Licens;

            old.Note = wh.Note;

            if (!string.IsNullOrEmpty(wh.Content))
                old.Content = wh.Content.Replace(" ", "&nbsp").Replace("\r\n", "<br />");

            if (Request.Files.Count > 0)
            {
                old.FileName = _helperServices.UpLoadImg("file", ""); //获取上传图片 

                //old.HeritageType = 2;
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
            ViewBag.user = UserLogin.GetUserInfo();
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
            var user = UserLogin.GetUserInfo();
            var old = _worldHeritageService.Get(id);
            if (old == null)
                return JumpUrl("My", "id错误");
            _worldHeritageService.Delete(id);
            if (user.Authority == 2)
                return Content("<script>alert('删除成功');window.location.href='" + Url.Action("My") + "';</script>");
            else
                return Content("<script>alert('删除成功');window.location.href='" + Url.Action("List") + "';</script>");
        }

        public ActionResult Verify(int id)
        {
            var old = _worldHeritageService.Get(id);
            if (old == null)
                return JumpUrl("My", "id错误");
            old.IsEffect = 0;
            _worldHeritageService.Update(old);
            return Content("<script>alert('申请完成');window.location.href='" + Url.Action("My") + "';</script>");
        }


        public ActionResult List(string key, int page = 1, int size = 50)
        {
            ViewBag.user = UserLogin.GetUserInfo();
            ViewBag.key = key;
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;
            var list = _worldHeritageService.List(key, pageIndex, pageSize, ref totalCount).ToList();
            var personsAsIPagedList = new StaticPagedList<WorldHeritage>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ActionResult Release(string key, int page = 1, int size = 50)
        {
            ViewBag.user = UserLogin.GetUserInfo();
            ViewBag.key = key;
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;
            var list = _worldHeritageService.Release(key, pageIndex, pageSize, ref totalCount).ToList();
            var personsAsIPagedList = new StaticPagedList<WorldHeritage>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        public ActionResult ChangeState(int id, int state)
        {
            var user = UserLogin.GetUserInfo();
            if (user.Authority == 2)
            {
                return Content("<script>alert('无权限');window.location.href='" + Url.Action("List") + "';</script>");
            }
            var old = _worldHeritageService.Get(id);
            if (old == null)
                return JumpUrl("List", "id错误");

            old.IsEffect = state;
            old.User = _managerService.Get(user.ManagerId);
            old.Audit = _managerService.Get(user.ManagerId).Name;
            old.AuditTime = DateTime.Now.ToString();
            _worldHeritageService.Update(old);
            return Content("<script>alert('审核完成');window.location.href='" + Url.Action("List") + "';</script>");
        }

        public ActionResult ChangeRelease(int id, int state)
        {
            var user = UserLogin.GetUserInfo();
            if (user.Authority == 2 || user.Authority == 1)
            {
                return Content("<script>alert('无权限');window.location.href='" + Url.Action("List") + "';</script>");
            }
            var old = _worldHeritageService.Get(id);
            if (old == null)
                return JumpUrl("List", "id错误");

            old.IsShow = state;
            old.Release = _managerService.Get(user.ManagerId);
            old.ReleaseDateTime = DateTime.Now.ToString();

            _worldHeritageService.Update(old);
            if (user.Authority == 0)
                return Content("<script>alert('发布完成');window.location.href='" + Url.Action("List") + "';</script>");
            else
                return Content("<script>alert('发布完成');window.location.href='" + Url.Action("Release") + "';</script>");
        }

        //视频上传
        public ActionResult Upload()
        {
            var httpfile = Request.Files["Filedata"];
            string fileName = Path.GetFileName(httpfile.FileName);
            string dir = "/Uploads/video/";
            string fullDir = dir + fileName;
            httpfile.SaveAs(Request.MapPath(fullDir));
            return Content(fullDir);
        }

        //资源属性介绍
        public ActionResult Attribute()
        {
            return View();
        }

    }
}