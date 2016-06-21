using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using InternalSystem.Core.Basis;
using InternalSystem.Infrastructure.Services;
using NPOI.SS.Formula.Functions;

namespace InternalSystem.Web.Filters
{
    public class InternalSystemAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly LogService _log = new LogService();

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //先判断是否登录
            var currentUser = UserLogin.GetUserInfo();
            if (currentUser != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 验证未通过时默认是返回401错误，这里可对该行为进行自定义，跳转用户到指定的地址
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            #region old
            var currentUser = UserLogin.GetUserInfo();
            if (currentUser == null)
            {
                _log.Debug("用户Session过期" + HttpContext.Current.Session.SessionID);
                var returnUri = filterContext.RequestContext.HttpContext.Request.Url;

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            Areas = "Admin",
                            controller = "Account",
                            action = "Login"
                        })
                    );
            }
            #endregion
        }
    }
}