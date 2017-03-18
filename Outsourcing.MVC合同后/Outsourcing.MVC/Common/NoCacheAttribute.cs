using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Common
{
    /// <summary>
    /// 设置ajax action不进行缓存 by 杨虎斌 2015/07/24
    /// </summary>
    public class NoCacheAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        { }
    }
}