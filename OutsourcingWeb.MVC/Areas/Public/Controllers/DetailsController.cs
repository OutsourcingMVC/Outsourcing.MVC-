using Outsourcing.EF;
using Outsourcing.EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Public.Controllers
{
    public class DetailsController : Controller
    {
        #region  DAL操作对象
        RequirementDAL requirementDAL = new RequirementDAL();
        #endregion
        // GET: Public/Details
        public ActionResult Details()
        {
            int compnayId = Convert.ToInt32(Session["ID"]);
            if (compnayId != 0)
            { ViewBag.LoginID = compnayId; }
            else { ViewBag.LoginID = ""; }
            return View();
        }
        [HttpPost]
        public ActionResult GetDateList(int RequirementId)
        {
            int compnayId = Convert.ToInt32(Session["ID"]);
            if (compnayId != 0)
            { ViewBag.LoginID = compnayId; }
            else { ViewBag.LoginID = ""; }
            List<Requirement> requirement = requirementDAL.GetrequirementList(RequirementId);
            return Json(requirement);
        }
    }
}