using Outsourcing.EF;
using Outsourcing.EF.DAL;
using Outsourcing.EF.DAL.Image;
using Outsourcing.EF.DAL.Recruit;
using Outsourcing.EF.Model.Index;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        MyRecruitDAL recruitDal = new MyRecruitDAL();
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        CustomerCompnayDAL custCompnayDAL = new CustomerCompnayDAL();
        OutsourcingCompanyDAL outCompnayDAL = new OutsourcingCompanyDAL();
        ImageDAL dal = new ImageDAL();

       
        public ActionResult Index(string loginid)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            ViewBag.Types = "";
           
           IndexShow model = new IndexShow();
            if (ViewBag.Types=="")
            {
                model.requirementList = dal.GetRequirementList("");
               
            }
            if (Session["types"] != null)
            {
                string type = Convert.ToString(Session["types"]);
                switch (type)
                {
                   
                    case "1":
                        model.requirementList = dal.GetRequirementList("");
                        //model.waitEmployerList = dal.GetWaitEmployerList(Convert.ToInt32(type));
                        break;
                    case "2":
                        model.persionalInfoList = dal.GetPersonalInfoList("");
                        break;
                }

                model.ptrcinfoList = dal.GetPtrcInfoList(Convert.ToInt32(type));
                ViewBag.Types = type;

            }


            return View(model);
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
                Session["LoginID"] = custOut[0].CustomerOutID;
                Session["TemporaryID"] = custOut[0].CustomerOutID;
                Session["types"] = types;
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

        public FileContentResult GetImg(int id, string type)
        {
            //int id = 1;
            //string type = "图片切换";
            var list = dal.GetImage(type);
            ImageFile model = new ImageFile();
            if (id <= list.Count && list.Count > 0 && list.Any())
            {
                if (list[id - 1] != null)
                {
                    model = list[id - 1];
                    ViewBag.ImgName = model.ImageName;
                    if (model.ImageFilePath != null)
                    {
                        return File(model.ImageFilePath, "image/jpg", model.ID.ToString() + model.ImageName);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else { return null; }
        }

        public FileContentResult GetPlatformImage(string type)
        {
            var model = dal.GetPlatformImage(type);
            if (model != null)
                return File(model.ImageFilePath, "image/jpg", model.ID.ToString() + model.ImageName);
            else
                return null;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Header()
        //{
        //    return PartialView();
        //}

        public ActionResult Nav(string loginid)
        {
            if (Session["types"] == null)
            {
                return Redirect("/System/Index");
            }
            else
            {
                List<Menu> list = recruitDal.GetMenuList(Convert.ToInt32(Session["types"]));
                ViewBag.LoginID = loginid;
                return PartialView(list);
            }
        }
    }
}