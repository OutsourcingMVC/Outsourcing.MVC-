using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Outsourcing.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    //使用系统默认的路由
            //    //defaults: new { controller = "System", action = "Index", id = UrlParameter.Optional },
            //    //使用自定义路由（Area下的某个action）
            //    defaults: new
            //    {
            //        controller = "Account",
            //        action = "Login",
            //        id = UrlParameter.Optional
            //    },
            //    namespaces: new string[] { "SGIONI.NS.MVC.Areas.Administrator.Controllers" }
            //).DataTokens.Add("Area", "Administrator");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
