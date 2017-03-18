using Outsourcing.EF;
using Outsourcing.EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Public.Controllers
{
    public class IdentityController : Controller
    {
        #region DAL操作对象
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        AccountDAL account = new AccountDAL();
        #endregion
        // GET: Public/Identity
        public ActionResult Identity()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        //验证新注册用户名是否存在
        [HttpPost]
        public ActionResult VerificationName(string AccountName)
        {
            string msg = "该用户不存在";
            List<CustomerOutsourc> custOut = custOutDAL.GetCountlList(AccountName);
            if (custOut.Count > 0)
            {
                msg = "该用户已存在";
                return Content(msg);
            }
            else
            {
                return Content(msg);
            }
        }
        [HttpPost]
        public ActionResult Add(CustomerOutsourc custOutsourc, CustomerCompnay custCompnay, OutsourcingCompany outCompany, string CompanyUserName, string Email, string password, int num)
        {
            string msg = "注册失败";
            try
            {
                if (num == 1)
                {
                    //添加帐户
                    using (var ct = new DB())
                    {
                        //将新注册用户添加到CustomerCompnay和OutsourcingCompany的中间表
                        custOutsourc.CompanyUserName = CompanyUserName;
                        custOutsourc.Pwd = password;
                        custOutsourc.Type = num;
                        custOutsourc.CreateTime = DateTime.Now;
                        ct.CustomerOutsourc.Add(custOutsourc);
                        //将新注册用户添加到外包公司表OutsourcingCompany
                        outCompany.CompanyUserName = CompanyUserName;
                        outCompany.Pwd = password;
                        outCompany.Email = Email;
                        outCompany.AuditingStatue = 0;
                        outCompany.IsDelete = 1;
                        outCompany.CreateTime= DateTime.Now;
                        ct.OutsourcingCompany.Add(outCompany);
                        if (custOutsourc.CustomerOutID <= 0)
                        {
                            msg = "注册成功";
                        }
                        ct.SaveChanges();
                        return Content(msg);
                    }
                }
                else if (num == 2)
                {
                    //添加帐户
                    using (var ct = new DB())
                    {
                        //将新注册用户添加到CustomerCompnay和OutsourcingCompany的中间表
                        custOutsourc.CompanyUserName = CompanyUserName;
                        custOutsourc.Pwd = password;
                        custOutsourc.Type = num;
                        custOutsourc.CreateTime = DateTime.Now;
                        ct.CustomerOutsourc.Add(custOutsourc);
                        //将新注册用户添加到客户公司表CustomerCompnay
                        custCompnay.CompanyUserName = CompanyUserName;
                        custCompnay.Pwd = password;
                        custCompnay.Email = Email;
                        custCompnay.AuditingStatue = 0;
                        custCompnay.IsDelete = 1;
                        custCompnay.CreateTime = DateTime.Now;
                        ct.CustomerCompnay.Add(custCompnay);
                        if (custOutsourc.CustomerOutID <= 0)
                        {
                            msg = "注册成功";
                        }
                        ct.SaveChanges();
                        return Content(msg);
                    }
                }
                return Content(msg);
            }
            catch (Exception e)
            {
                return Content(msg);
            }
        }
    }
}