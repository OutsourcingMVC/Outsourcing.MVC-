using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.Public.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OutsourcingWeb.MVC.Areas.Public.Controllers
{
    public class MemberCenterController : Controller
    {
        #region  DAL操作对象
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        CustomerCompnayDAL custCompnayDAL = new CustomerCompnayDAL();
        OutsourcingCompanyDAL outCompnayDAL = new OutsourcingCompanyDAL();
        #endregion
        // GET: Public/MemberCenter
        public ActionResult ModifyPassword(string loginid, string type)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type)) { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            return View();
        }
      
        public ActionResult PersonaData(string loginid)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            int type = Convert.ToInt32(Session["type"]);
            ViewBag.Type = type;
            
            return View();
        }
        [HttpPost]
        public JsonResult GetData()
        {
            int outID = Convert.ToInt32(Session["ID"]);
            int type = Convert.ToInt32(Session["type"]);

            string resStr = string.Empty;
            if (type == 1)
            {
                //MemberCenterViewModel memberCenterViewModel = new MemberCenterViewModel();
                //memberCenterViewModel.outConmpany = outCompnayDAL.GetList(outID);
                //return Json(memberCenterViewModel);
                resStr = outCompnayDAL.GetListJSON(outID);
            }
            else
            {
                //MemberCenterViewModel memberCenterViewModel = new MemberCenterViewModel();                
                //memberCenterViewModel.custConmpany = custCompnayDAL.GetList(outID);                
                //return Json(memberCenterViewModel);
                resStr = custCompnayDAL.GetListJSON(outID);
            }
            return Json(resStr);
        }
        //保存数据
        public ActionResult SaveData(OutsourcingCompany model, CustomerCompnay view)
        {
            string msg = "保存成功";
            string pws = Session["pws"].ToString();
            string CompanyUserName = Session["CompanyUserName"].ToString();
            int type = Convert.ToInt32(Session["type"]);
            if (type == 1)
            {
                List<OutsourcingCompany> outCompany = outCompnayDAL.GetCountlList(CompanyUserName);
                string email = outCompany[0].Email;
                string createTime = outCompany[0].CreateTime.ToString();
                int id = Convert.ToInt32(outCompany[0].CompnayId);
                string pwd = outCompany[0].Pwd;
                int auditingStatue = Convert.ToInt32(outCompany[0].AuditingStatue);
                if (model != null)
                {
                    model.CompnayId = id;
                    model.CompanyUserName = CompanyUserName;
                    model.Pwd = pwd;
                    model.Email = email;
                    model.IsDelete = 1;
                    model.AuditingStatue = auditingStatue;
                    model.CreateTime = Convert.ToDateTime(createTime);
                    model.UpdateTime = DateTime.Now;
                    int result = outCompnayDAL.Update(model);
                    if (result > 0)
                    {
                        return Content(msg);
                    }
                    else
                    {
                        msg = "保存失败";
                        return Content(msg);
                    }
                }
            }
            else
            {
                List<CustomerCompnay> custCompany = custCompnayDAL.GetCountlList(CompanyUserName);
                string email = custCompany[0].Email;
                string createTime = custCompany[0].CreateTime.ToString();
                int id = Convert.ToInt32(custCompany[0].CompnayId);
                string pwd = custCompany[0].Pwd;
                if (view != null)
                {
                    view.CompnayId = id;
                    view.CompanyUserName = CompanyUserName;
                    view.Pwd = pwd;
                    view.Email = email;
                    view.IsDelete = 1;
                    view.AuditingStatue = 1;
                    view.CreateTime = Convert.ToDateTime(createTime);
                    view.UpdateTime = DateTime.Now;
                    int result = custCompnayDAL.Update(view);
                    if (result > 0)
                    {
                        return Content(msg);
                    }
                    else
                    {
                        msg = "保存失败";
                        return Content(msg);
                    }
                }
            }
            return Content(msg);
        }
        [HttpPost]
        public ActionResult GetPasswordData(CustomerOutsourc custOutsourc, CustomerCompnay custCompnay, OutsourcingCompany outCompany, string password)
        {
            string msg = "保存成功";
            string pws = Session["pws"].ToString();
            string CompanyUserName = Session["CompanyUserName"].ToString();
            int type = Convert.ToInt32(Session["type"]);
            if (type == 1)
            {
                List<CustomerOutsourc> Cusout = custOutDAL.GetCountlList(CompanyUserName);
                List<OutsourcingCompany> Company = outCompnayDAL.GetCountlList(CompanyUserName);
                if (pws == password)
                {
                    if (outCompany != null && custOutsourc != null)
                    {
                        //修改中间表
                        custOutsourc.CustomerOutID = Cusout[0].CustomerOutID;
                        custOutsourc.CompanyUserName = CompanyUserName;
                        custOutsourc.Type = Cusout[0].Type;
                        custOutsourc.CreateTime = Cusout[0].CreateTime;
                        //修改外包表
                        outCompany.CompnayId = Company[0].CompnayId;
                        outCompany.CompanyUserName = CompanyUserName;
                        outCompany.Email = Company[0].Email;
                        outCompany.CompnayName = Company[0].CompnayName;
                        outCompany.EnglishName = Company[0].EnglishName;
                        outCompany.LegalRepresentative = Company[0].LegalRepresentative;
                        outCompany.LegalTelephone = Company[0].LegalTelephone;
                        outCompany.UnitResponsible = Company[0].UnitResponsible;
                        outCompany.ResponsibleTelephone = Company[0].ResponsibleTelephone;
                        outCompany.Address = Company[0].Address;
                        outCompany.EnterpriseNum2 = Company[0].EnterpriseNum2;
                        outCompany.RegistrationAuthority = Company[0].RegistrationAuthority;
                        outCompany.BusinessScope = Company[0].BusinessScope;
                        outCompany.IsDelete = Company[0].IsDelete;
                        outCompany.AuditingStatue = Company[0].AuditingStatue;
                        outCompany.CreateTime = Company[0].CreateTime;
                        outCompany.UpdateTime = DateTime.Now;
                        int result = custOutDAL.Update(custOutsourc);
                        int results = outCompnayDAL.Update(outCompany);
                        if (result > 0 && results > 0)
                        {
                            return Content(msg);
                        }
                    }
                    else
                    {
                        msg = "保存失败";
                        return Content(msg);
                    }
                }
                else
                {
                    msg = "原密码错误";
                    return Content(msg);
                }
                return Content(msg);
            }
            else
            {
                List<CustomerOutsourc> Cusout = custOutDAL.GetCountlList(CompanyUserName);
                List<CustomerCompnay> Company = custCompnayDAL.GetCountlList(CompanyUserName);
                if (pws == password)
                {
                    if (custCompnay != null && custOutsourc != null)
                    {
                        //修改中间表
                        custOutsourc.CustomerOutID = Cusout[0].CustomerOutID;
                        custOutsourc.CompanyUserName = CompanyUserName;
                        custOutsourc.Type = Cusout[0].Type;
                        custOutsourc.CreateTime = Cusout[0].CreateTime;
                        //修改客户表
                        custCompnay.CompnayId = Company[0].CompnayId;
                        custCompnay.CompanyUserName = CompanyUserName;
                        custCompnay.Email = Company[0].Email;
                        custCompnay.CompnayName = Company[0].CompnayName;
                        custCompnay.EnglishName = Company[0].EnglishName;
                        custCompnay.LegalRepresentative = Company[0].LegalRepresentative;
                        custCompnay.LegalTelephone = Company[0].LegalTelephone;
                        custCompnay.UnitResponsible = Company[0].UnitResponsible;
                        custCompnay.ResponsibleTelephone = Company[0].ResponsibleTelephone;
                        custCompnay.Address = Company[0].Address;
                        custCompnay.EnterpriseNum2 = Company[0].EnterpriseNum2;
                        custCompnay.RegistrationAuthority = Company[0].RegistrationAuthority;
                        custCompnay.BusinessScope = Company[0].BusinessScope;
                        custCompnay.IsDelete = Company[0].IsDelete;
                        custCompnay.AuditingStatue = Company[0].AuditingStatue;
                        custCompnay.CreateTime = Company[0].CreateTime;
                        custCompnay.UpdateTime = DateTime.Now;
                        int result = custOutDAL.Update(custOutsourc);
                        int results = custCompnayDAL.Update(custCompnay);
                        if (result > 0 && results > 0)
                        {
                            return Content(msg);
                        }
                    }
                    else
                    {
                        msg = "保存失败";
                        return Content(msg);
                    }
                }
                else
                {
                    msg = "原密码错误";
                    return Content(msg);
                }
                return Content(msg);
            }
        }
    }
}
