using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Framework.Utility;

namespace Outsourcing.MVC.Common
{
    public class ControllerBase : Controller
    {
        //// GET: ControllerBase
        //public ActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// 当前登录的用户属性
        /// </summary>
        public EF.Account CurrentAccountInfo
        {
            get
            {
                if (Session["LoginedUser"] != null)

                    return Session["LoginedUser"] as EF.Account;

                else
                    return new EF.Account();
            }

        }


        //private const String USER_CONTEXT= "USER_CONTEXT";
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //检查用户Session,如果未登录，则导航到登录页
            //if (Session[USER_CONTEXT]==null)
            //{
            //    filterContext.RequestContext.HttpContext.Server.Transfer("~/login.html");
            //}
            //base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 如果是Ajax请求的话，清除浏览器缓存
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.SetNoStore();
            }
            base.OnResultExecuted(filterContext);
        }
    }
}