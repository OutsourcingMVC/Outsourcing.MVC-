using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.Public.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Public.Controllers
{
    public class PublishMessageController : Controller
    {
        #region  DAL操作对象
        RequirementDAL requirementDAL = new RequirementDAL();
        CustomerCompnayDAL custCompnayDAL = new CustomerCompnayDAL();
        #endregion
        // GET: Public/PublishMessage
        public ActionResult PublishMessage(string loginid,string type)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            return View();
        }
        [HttpPost]
        public ActionResult AddDate(Requirement requirement, string JobDescription, string ComProfile)
        {
            int compnayId = Convert.ToInt32(Session["ID"]);
            if (compnayId != 0)
            { ViewBag.LoginID = compnayId; }
            else { ViewBag.LoginID = ""; }
            string jobDescription = HttpUtility.UrlDecode(JobDescription);
            string comProfile = HttpUtility.UrlDecode(ComProfile);
            string msg = "添加失败";
            try
            {
                using (var ct = new DB())
                {
                    requirement.CompnayId = compnayId;
                    requirement.JobDescription = jobDescription;
                    requirement.ComProfile = comProfile;
                    requirement.IsDelete = 1;
                    requirement.CreateTime = DateTime.Now;
                    requirement.UpdateTime = DateTime.Now;
                    requirement.CreateUser = Session["CompanyUserName"].ToString();
                    requirement.UpdateUser = Session["CompanyUserName"].ToString();
                    ct.Requirement.Add(requirement);
                    if (requirement.RequirementId <= 0)
                    {
                        msg = "添加成功";
                    }
                    ct.SaveChanges();
                }
                return Content(msg);
            }
            catch (Exception dbEx)
            {
                return Content(msg);
            }
        }
        public ActionResult Edit(string loginid, string type, int RequirementId)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type)) { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            //var aId = Session["TemporaryID"];
            //if (!string.IsNullOrEmpty(aId.ToString()))
            //{ ViewBag.LoginID = aId; }
            //else { ViewBag.LoginID = ""; }
            //RequirementEditViewModel viewModel = new RequirementEditViewModel();
            //List<Requirement> requirement = requirementDAL.GetrequirementList(RequirementId);
            //viewModel.RequirementView = requirement.FirstOrDefault();
            //return Json(requirement);

            return View();
        }
        [HttpPost]
        public ActionResult GetDate(int RequirementId)
        {
            Session["td"] = RequirementId;
            //ViewBag.id = RequirementId;
            List<Requirement> requirement = requirementDAL.GetrequirementList(RequirementId);
            return Json(requirement);
        }
        [HttpPost]
        public ActionResult UpDate(Requirement model, int RequirementId)
        {
            string msg = "更新失败";
            try
            {
                List<Requirement> requirement = requirementDAL.GetrequirementList(RequirementId);
                if (requirement != null)
                {
                    model.CompnayId = requirement[0].CompnayId;
                    model.IsDelete = requirement[0].IsDelete;
                    model.CreateTime = requirement[0].CreateTime;
                    model.UpdateTime = DateTime.Now;
                    model.CreateUser = requirement[0].CreateUser;
                    model.UpdateUser = requirement[0].UpdateUser;
                    int result = requirementDAL.Update(model);
                    if (result > 0)
                    {
                        msg = "更新成功";
                        return Content(msg);
                    }
                }
                else
                {
                    return Content(msg);
                }
            }
            catch (Exception dbEx)
            {
                return Content(msg);
            }
            return Content(msg);
        }
    }
}