using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Public.Controllers
{
    public class ResetPasswordController : Controller
    {
        // GET: Public/ResetPassword
        public ActionResult ResetPassword()
        {
            return View();
        }
    }
}