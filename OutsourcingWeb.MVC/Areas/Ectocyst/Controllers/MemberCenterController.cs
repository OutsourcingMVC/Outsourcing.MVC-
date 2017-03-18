using Outsourcing.EF;
using Outsourcing.EF.DAL;
using Outsourcing.Framework.Utility;
using OutsourcingWeb.MVC.Areas.Ectocyst.Models;
using OutsourcingWeb.MVC.Common;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using Aspose.Words.Tables;

namespace OutsourcingWeb.MVC.Areas.Ectocyst.Controllers
{
    public class MemberCenterController : Controller
    {
        #region  DAL操作对象
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        CustomerCompnayDAL custCompnayDAL = new CustomerCompnayDAL();
        OutsourcingCompanyDAL outCompnayDAL = new OutsourcingCompanyDAL();
        PersonalInfoDAL personalInfoDAL = new PersonalInfoDAL();
        #endregion
        // GET: Ectocyst/MemberCenter

      
        public ActionResult PersonaData(string loginid)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            List<CustomerOutsourc> custOut = custOutDAL.GetlList("CustomerOutID=" + loginid);
            if (custOut.Count > 0)
            {
                string types = custOut[0].Type.ToString();
                ViewBag.Type = types;
            }
            return View();
        }
        [NoCache]
        public ActionResult TalentManagement(string loginid, string type, string StaffName, string temp, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type)) { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            int outID = Convert.ToInt32(Session["ID"]);
            Models.PersonalInfoViewModel viewModel = new PersonalInfoViewModel();
            IEnumerable<PersonalInfo> personalInfo = null;
            if (!string.IsNullOrWhiteSpace(StaffName))
            {
                StaffName = HttpUtility.UrlDecode(StaffName);
                personalInfo = personalInfoDAL.GetPersonList(string.Format(" OutsourcingCompanyId='{0}' and IsDelete = 1 and PeopleStatue!=0 and StaffName like '%{1}%' Order By CreateTime DESC", outID, StaffName)).AsQueryable();
                ViewBag.CurrentFilter = StaffName;
            }
            else
            {
                personalInfo = personalInfoDAL.GetPersonList(string.Format(" OutsourcingCompanyId='{0}' and PeopleStatue!=0 and IsDelete = 1 Order By CreateTime DESC", outID)).AsQueryable();
            }
            viewModel.PersonalInfo = personalInfo.ToPagedList(page, 6);
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
            Models.PersonalInfoViewModel viewModel = new PersonalInfoViewModel();
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
        //添加人才信息
        [HttpPost]
        public ActionResult AddData(PersonalInfo personalInfo)
        {
            string msg = "添加失败";
            try
            {
                using (var ct = new DB())
                {
                    
                    personalInfo.OutsourcingCompanyId = Session["ID"].ToString();
                    personalInfo.IsDelete = 1;
                    personalInfo.CreateTime = DateTime.Now;
                    personalInfo.UpdateTime = DateTime.Now;
                    personalInfo.CreateUser = Session["CompanyUserName"].ToString();
                    personalInfo.UpdateUser = Session["CompanyUserName"].ToString();
                    ct.PersonalInfo.Add(personalInfo);
                    if (personalInfo.PersonalInfoId <= 0)
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

        /// <summary>
        ///导入简历
        /// </summary>
        /// <returns></returns>
        public ActionResult PutInWord()
        {
            try
            {
                Aspose.Words.Document doc = new Aspose.Words.Document();
                string str = doc.ToTxt();
                string s = str.Replace("时    间：", "|").Replace("地    点：", "|").Replace("人员：", "|").Replace("主 持 人：", "|").Replace("议    程：", "|").Replace("领导：", "|");
                string[] st = s.Split('|');
                //  赋值，保存方法
            }
            catch (Exception ex)
            {
                //ShowMessage("模板文件格式有误，请修改后重新导入");
                return null;
            }
        
          
            return View();
        }

        //删除
        [HttpPost]
        public ActionResult Delete(PersonalInfo model, int id)
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
        //编辑视图
        [HttpPost]
        public ActionResult Edit(int id)
        {
            //ViewBag.tid = id;
            Session["id"] = id;
            List<PersonalInfo> personalInfo = personalInfoDAL.GetPersonalInfoList(id);
            return Json (personalInfo);
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
    }
}