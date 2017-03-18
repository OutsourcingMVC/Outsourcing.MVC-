using Outsourcing.EF.DAL.Recruit;
using Outsourcing.EF.Model.Recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Recruit.Controllers
{
    public class RecruitController : Controller
    {
        #region DAL操作对象
        MyRecruitDAL dal = new MyRecruitDAL();
        #endregion

        // GET: Recruit/Recruit
        public ActionResult RecruitCenter(string loginid)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }

            ViewBag.types = Session["types"];
            List<RecruitModel> list = dal.GetRecruitList(Convert.ToInt32(loginid));
            return View();
            
        }

        public ActionResult CreateRecruit(string loginId,string id)
        {
            if (!string.IsNullOrEmpty(loginId))
            { ViewBag.LoginID = loginId; }
            else { ViewBag.LoginID = ""; }
            ViewBag.types = Session["types"];
            MySelfModel model = new MySelfModel();
            if (!string.IsNullOrWhiteSpace(id))
            {
                //model = dal.GetMySelfInfo(Convert.ToInt32(loginId),Convert.ToInt32(id));
            }
            return View(model);
        }

        public ActionResult CreateMyself(string loginid,string id)
        {
            
            return View();
        }
    }
}