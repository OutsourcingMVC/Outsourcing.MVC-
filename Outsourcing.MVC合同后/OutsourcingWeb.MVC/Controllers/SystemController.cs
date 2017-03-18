using Outsourcing.EF;
using Outsourcing.EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OutsourcingWeb.MVC.Models;
using System.Security.Policy;
using System.Collections;

namespace OutsourcingWeb.MVC.Controllers
{
    public class SystemController : Controller
    {
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        CustomerCompnayDAL custCompnayDAL = new CustomerCompnayDAL();
        OutsourcingCompanyDAL outCompnayDAL = new OutsourcingCompanyDAL();
        // GET: System
        public ActionResult Index(string loginid)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            return View();
        }
        [HttpPost]
        //登录
        public ActionResult Login(CustomerOutsourc custOutsourc, string CompanyUserName, string Pwd)
        {
            string msg = "该账号不存在";
            Hashtable result = new Hashtable();
            List<CustomerOutsourc> custOut = custOutDAL.GetCountlList(CompanyUserName);
            if (custOut.Count > 0)
            {
                int types = Convert.ToInt32(custOut[0].Type);
                result["LoginID"] = custOut[0].CustomerOutID;
                Session["TemporaryID"] = custOut[0].CustomerOutID;
                if (types == 1)
                {
                    List<OutsourcingCompany> outCompany = outCompnayDAL.GetCountlList(CompanyUserName);
                    int statue = Convert.ToInt32(outCompany[0].AuditingStatue);
                    int IsDelete = Convert.ToInt32(outCompany[0].IsDelete);
                    int id = Convert.ToInt32(outCompany[0].CompnayId);
                    string pwd = custOut[0].Pwd;

                    if (IsDelete == 1)
                    {
                        if (pwd == Pwd)
                        {
                            msg = "登录成功";
                            Session["ID"] = id;
                            //result["LoginID"] = msg;
                            Session["CompanyUserName"] = CompanyUserName;
                            Session["pws"] = pwd;
                            Session["type"] = types;
                            result["msg"] = msg;
                        }
                        else
                        {
                            msg = "密码错误";
                            result["msg"] = msg;
                        }
                    }
                    else
                    {
                        result["msg"] = msg;
                    }
                }
                else
                {
                    List<CustomerCompnay> custCompany = custCompnayDAL.GetCountlList(CompanyUserName);
                    int IsDelete = Convert.ToInt32(custCompany[0].IsDelete);
                    int statue = Convert.ToInt32(custCompany[0].AuditingStatue);
                    int id = Convert.ToInt32(custCompany[0].CompnayId);
                    string pwd = custOut[0].Pwd;
                    if (IsDelete == 1)
                    {
                        if (pwd == Pwd)
                        {
                            msg = "登录成功";
                            Session["ID"] = id;
                            //result["LoginID"] = msg;
                            Session["CompanyUserName"] = CompanyUserName;
                            Session["pws"] = pwd;
                            Session["type"] = types;
                            result["msg"] = msg;
                        }
                        else
                        {
                            msg = "密码错误";
                            result["msg"] = msg;
                        }
                    }
                    else
                    {
                        result["msg"] = msg;
                    }
                }
            }
            else
            {
                result["msg"] = msg;
            }
            return Json(result);
        }

        [HttpPost]
        //退出
        public ActionResult Quit()
        {
            string msg = "退出成功";
            Session.Contents.Clear();
            Session.Abandon();
            return Content(msg);
        }
    }
}