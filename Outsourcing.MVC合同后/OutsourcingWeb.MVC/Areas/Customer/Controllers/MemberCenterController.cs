using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.Customer.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Customer.Controllers
{
    public class MemberCenterController : Controller
    {
        #region  dal操作对象
        RequirementDAL requirementDAL = new RequirementDAL();
        PersonalInfoDAL personalInfoDAL = new PersonalInfoDAL();
        PersonsEntrySetDAL personsEntrySetDal = new PersonsEntrySetDAL();
        #endregion
        // GET: Customer/MemberCenter
        //[NoCache]
        public ActionResult TalentManagement(string loginid, string type, string StaffName, string temp, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type)) { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            int outID = Convert.ToInt32(Session["ID"]);
            Models.MemberCenterViewModel viewModel = new MemberCenterViewModel();
            List<PersonalInfo> personalInfo = null;
            if (!string.IsNullOrWhiteSpace(StaffName))
            {
                StaffName = HttpUtility.UrlDecode(StaffName);
                personalInfo = personalInfoDAL.GetPersonList(string.Format(" OutsourcingCompanyId='{0}' and IsDelete = 1 and StaffName like '%{1}%' Order By CreateTime DESC", outID, StaffName));
                ViewBag.CurrentFilter = StaffName;
            }
            else
            {
                personalInfo = personalInfoDAL.GetPersonList(string.Format(" OutsourcingCompanyId='{0}' and IsDelete = 1 Order By CreateTime DESC", outID));
            }
            viewModel.PersonalInfo = personalInfo.OrderBy(d => d.PersonalInfoId).ToPagedList(page, 6);
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult TalentManagement(string loginid, string type, string PersonName, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type)) { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            int outID = Convert.ToInt32(Session["ID"]);
            Models.MemberCenterViewModel viewModel = new MemberCenterViewModel();
            IEnumerable<PersonalInfo> personalInfo = null;
            if (!string.IsNullOrWhiteSpace(PersonName))
            {
                PersonName = HttpUtility.UrlDecode(PersonName);
                personalInfo = personalInfoDAL.GetPersonList(string.Format(" OutsourcingCompanyId='{0}' and IsDelete = 1 and PersonName like '%{1}%' Order By CreateTime DESC", outID, PersonName)).AsQueryable();
                ViewBag.CurrentFilter = PersonName;
            }
            else
            {
                personalInfo = personalInfoDAL.GetPersonList(string.Format(" OutsourcingCompanyId='{0}' and IsDelete = 1 Order By CreateTime DESC", outID)).AsQueryable();
            }
            viewModel.PersonalInfo = personalInfo.ToPagedList(page, 7);
            return View(viewModel);
        }
        
        /// <summary>
        /// 所属的项目的查询
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="StaffName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult RecManagement(string loginid, string type, string StaffName, int page = 1)
        {

            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type)) { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            int outID = Convert.ToInt32(Session["ID"]);
            Models.MemberCenterViewModel viewModel = new MemberCenterViewModel();
            IEnumerable<Requirement> requirement = null;
            if (!string.IsNullOrWhiteSpace(StaffName))
            {
                StaffName = HttpUtility.UrlDecode(StaffName);
                requirement = requirementDAL.GetPersonList(string.Format(" b.CompnayId='{0}' AND b.IsDelete=1 and b.ProjectName like '%{1}%'", outID, StaffName)).AsQueryable();
                ViewBag.CurrentFilter = StaffName;
            }
            else
            {
                requirement = requirementDAL.GetPersonList(string.Format("  b.CompnayId='{0}' AND b.IsDelete=1", outID)).AsQueryable();
            }
            viewModel.Requirement = requirement.OrderByDescending(d => d.ArrivalTime.Value).ToPagedList(page, 6);
            return View(viewModel);
        }
        //删除
        [HttpPost]
        public ActionResult Delete(Requirement model, int id)
        {
            string msg = "删除失败";
            try
            {
                List<Requirement> requirement = requirementDAL.GetrequirementList(id);
                if (id > 0 && requirement != null)
                {
                    model.RequirementId = requirement[0].RequirementId;
                    model.ProjectName = requirement[0].ProjectName;
                    model.ArrivalTime = requirement[0].ArrivalTime;
                    model.JobName = requirement[0].JobName;
                    model.CompnayId = requirement[0].CompnayId;
                    model.Salary = requirement[0].Salary;
                    model.PublishTime = requirement[0].PublishTime;
                    model.ProjectAddress = requirement[0].ProjectAddress;
                    model.Property = requirement[0].Property;
                    model.Experience = requirement[0].Experience;
                    model.Education = requirement[0].Education;
                    model.RecNumber = requirement[0].RecNumber;
                    model.JobDescription = requirement[0].JobDescription;
                    model.ComAddress = requirement[0].ComAddress;
                    model.TecLanguage = requirement[0].TecLanguage;
                    model.Remark = requirement[0].Remark;
                    model.CreateUser = requirement[0].CreateUser;
                    model.CreateTime = requirement[0].CreateTime;
                    model.UpdateUser = requirement[0].UpdateUser;
                    model.UpdateTime = requirement[0].UpdateTime;
                    model.IsDelete = 0;
                    int result = requirementDAL.Update(model);
                    if (result > 0)
                    {
                        msg = "删除成功";
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
        //删除
        [HttpPost]
        public ActionResult PersonalInfoDelete(PersonalInfo model, int id)
        {
            string msg = "删除失败";
            try
            {
                List<PersonalInfo> personalInfo = personalInfoDAL.GetPersonalInfoList(id);
                if (id > 0 && personalInfo != null)
                {
                    model.PersonalInfoId = personalInfo[0].PersonalInfoId;
                    model.OutsourcingCompanyId = personalInfo[0].OutsourcingCompanyId;
                    model.PersonName = personalInfo[0].PersonName;
                    model.Sex = personalInfo[0].Sex;
                    model.Birthday = personalInfo[0].Birthday;
                    model.Telephone = personalInfo[0].Telephone;
                    model.Email = personalInfo[0].Email;
                    model.CaredID = personalInfo[0].CaredID;
                    model.Publications = personalInfo[0].Publications;
                    model.Education = personalInfo[0].Education;
                    model.GraduationSchool = personalInfo[0].GraduationSchool;
                    model.GraduationDate = personalInfo[0].GraduationDate;
                    model.CVStatue = personalInfo[0].CVStatue;
                    model.PeopleStatue = personalInfo[0].PeopleStatue;
                    model.CreateUser = personalInfo[0].CreateUser;
                    model.CreateTime = personalInfo[0].CreateTime;
                    model.UpdateUser = personalInfo[0].UpdateUser;
                    model.UpdateTime = personalInfo[0].UpdateTime;
                    model.IsDelete = 0;
                    int result = personalInfoDAL.Update(model);
                    if (result > 0)
                    {
                        msg = "删除成功";
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
        //编辑
        [HttpPost]
        public ActionResult Edit(int id)
        {
            List<PersonalInfo> personalInfo = personalInfoDAL.GetPersonalInfoList(id);
            return Json(personalInfo);
        }
        //编辑
        [HttpPost]
        public ActionResult UpdateData(PersonalInfo model, int PersonalInfoId)
        {
            string msg = "更新失败";
            try
            {
                List<PersonalInfo> personalInfo = personalInfoDAL.GetPersonalInfoList(PersonalInfoId);
                if (PersonalInfoId > 0 && personalInfo != null)
                {
                    model.OutsourcingCompanyId = personalInfo[0].OutsourcingCompanyId;
                    model.CreateUser = personalInfo[0].CreateUser;
                    model.CreateTime = personalInfo[0].CreateTime;
                    model.UpdateUser = personalInfo[0].UpdateUser;
                    model.UpdateTime = DateTime.Now;
                    model.IsDelete = 1;
                    int result = personalInfoDAL.Update(model);
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
        //查询
        //[HttpPost]
        //public ActionResult Inseach(string PersonNames)
        //{
        //    personalInfoDA
        //}
    }
}