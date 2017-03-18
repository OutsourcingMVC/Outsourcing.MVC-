using Outsourcing.EF.DAL;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.Customer.Controllers
{
    [Authorization]
    public class BasicInformationController : Controller
    {
        #region DAL操作对象
        ContractManagementDAL contractDAL = new ContractManagementDAL();
        CustomerCompnayDAL customercompnayDAL = new CustomerCompnayDAL();
        #endregion
        // GET: Customer/BasicInformation
        [NoCache]
        public ActionResult Index(string companyname, string temp, int page = 1)
        {

            const int pageSize = 8;
            IEnumerable<EF.CustomerCompnay> customercompnays = null;
            if (!string.IsNullOrWhiteSpace(companyname))
            {
                string ee = HttpUtility.UrlDecode(companyname);
                customercompnays = customercompnayDAL.GetCustomerCompnayList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                customercompnays = customercompnayDAL.GetCustomerCompnayList(string.Empty).AsQueryable();
            var model = customercompnays.OrderBy(m => m.CompnayName).ToPagedList(page, pageSize);
            return View(model);

        }
        [HttpPost]
        public ActionResult Index(string companyname, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.CustomerCompnay> customercompnays = null;
            if (!string.IsNullOrWhiteSpace(companyname))
            {
                customercompnays = customercompnayDAL.GetCustomerCompnayList(companyname).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = companyname;
            }
            else
                customercompnays = customercompnayDAL.GetCustomerCompnayList(string.Empty).AsQueryable();
            var model = customercompnays.OrderBy(m => m.CompnayName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {
            EF.CustomerCompnay customercompnay = null;
            if (id > 0)
            {
                customercompnay = customercompnayDAL.GetCustomerCompnay(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(customercompnay);
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string msg = "删除失败";
            try
            {
                if (id != 0)
                {
                    var customercompnay = customercompnayDAL.GetCustomerCompnay(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count = customercompnayDAL.Delete(customercompnay);
                    if (count < 0)
                    {
                        return Content(msg);
                    }
                }
                // TODO: Add delete logic here
                //return RedirectToAction("Index");
                msg = "删除成功";
                return Content(msg);
            }
            catch
            {
                return Content(msg);
                //return View();
            }
        }

        [NoCache]
        public ActionResult Edit(int id, int reviewed, int pageIndex, string SearchString)
        {
            const int pageSize = 8;
            IEnumerable<EF.CustomerCompnay> customercompnays = null;

            EF.CustomerCompnay customercompnay = customercompnayDAL.GetCustomerCompnay(id);
            if (reviewed == 3)  //解禁标志
            {
                //解禁后置当前状态为通过状态；
                customercompnay.AuditingStatue = 1;
            }
            else
            {
                //
                customercompnay.AuditingStatue = reviewed;
            }

            customercompnayDAL.Update(customercompnay);
            ViewBag.CurrentFilter = SearchString;
            customercompnays = customercompnayDAL.GetCustomerCompnayList(string.Empty).AsQueryable();
            if (reviewed == 1)
            {
                #region  生成新的合同项
                EF.ContractManagement contract = contractDAL.GetContractByPartner(customercompnay.CompnayId);
                //EF.ContractManagement contract = new EF.ContractManagement();
                if (contract == null) //如果不存在，则添加新记录
                {
                    contract = new EF.ContractManagement();
                    contract.ID = Guid.NewGuid().ToString();
                    int k = customercompnay.CompnayId;

                    contract.Code = string.Format("KH-{0}", k.ToString("0000"));
                    contract.ContractName = string.Format("{0}-客户合同", customercompnay.CompnayName);
                    contract.ContractStatus = "已审核";
                    contract.ContractTypes = "客户合同";
                    contract.ContractPartner = customercompnay.CompnayName;
                    contract.RegisterId = customercompnay.CompnayId;
                    contract.PartnerType = "客户公司";
                    contract.CreateTime = DateTime.Now;
                    contract.UpdateTime = DateTime.Now;
                    contractDAL.Insert(contract);
                }
                else //如果存在，那么更新时间；
                {
                    contract.UpdateTime = DateTime.Now;
                    contractDAL.Update(contract);
                }
                #endregion
            }
            else
            {
                 //未通过 
            }
            var model = customercompnays.OrderBy(m => m.CompnayName).ToPagedList(pageIndex, pageSize);
            return View("Index",model);
        }
    }
}