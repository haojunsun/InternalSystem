using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using InternalSystem.Core.Basis;
using InternalSystem.Core.Models;
using InternalSystem.Core.Services;
using InternalSystem.Infrastructure.Services;
using InternalSystem.Web.Helpers;

namespace InternalSystem.Web.Areas.Admin.Controllers
{
    public class AccountController : ControllerHelper
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;

        public AccountController(IManagerService managerService, IHelperServices helperServices)
        {
            _managerService = managerService;
            _helperServices = helperServices;
        }

        public ActionResult Login()
        {
            return View(false);
        }

        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string username, string password, string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode))
            {
                return Content("<script>alert('验证码为空！');window.location.href='Register';</script>");
            }
            if (Session["ValidateCode"].ToString().ToLower() != verificationCode.ToLower())
            {
                return Content("<script>alert('验证码错误！');window.location.href='Register';</script>");
            }
            var manager = new Manager();
            manager.Authority = 2;
            manager.CreatedUtc = DateTime.Now;
            manager.LoginId = username;
            manager.Name = username;
            manager.Pass = _helperServices.MD5Encrypt(password);
            _managerService.Add(manager);
            return Content("<script>alert('跳转登陆！');window.location.href='Login';</script>");
        }

        [HttpPost]
        public ActionResult Login(string username, string password, bool remember = false)
        {
            var pass = _helperServices.MD5Encrypt(password);
            var result = _managerService.Login(username, pass);
            if (result == null)
            {
                return View(true);
            }

            _helperServices.SetSession("SESSION_USER_INFO", result);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 验证码的校验
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ActionResult CheckCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}

