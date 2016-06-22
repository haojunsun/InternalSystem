using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using InternalSystem.Core.Basis;
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
            return View();
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
    }
}