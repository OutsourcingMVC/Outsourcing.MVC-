using Outsourcing.MVC.Areas.Administrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.EF;
using Outsourcing.EF.DAL;
using Outsourcing.MVC.Common;
using PagedList;
using Outsourcing.Framework.Utility;
using System.Data.Entity;

namespace Outsourcing.MVC.Areas.Outsourcing.Controllers
{
    [Authorization]
    public class BasicInformationController : Controller
    {
        #region DAL操作对象
        ContractManagementDAL contractDAL = new ContractManagementDAL();//合同管理类的调用
        OutsourcingCompanyDAL outsourcingcompanyDAL = new OutsourcingCompanyDAL();//外包公司类的调用
        #endregion
        // GET: Outsourcing/BasicInformation
        //
        [NoCache]
        public ActionResult Index(string companyusername, string temp, int page = 1)
        {

            const int pageSize = 8;
            IEnumerable<EF.OutsourcingCompany> outsourcingcompanys = null;
            if (!string.IsNullOrWhiteSpace(companyusername))
            {
                string ee = HttpUtility.UrlDecode(companyusername);
                outsourcingcompanys = outsourcingcompanyDAL.GetOutsourcingCompanyList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                outsourcingcompanys = outsourcingcompanyDAL.GetOutsourcingCompanyList(string.Empty).AsQueryable();
            var model = outsourcingcompanys.OrderBy(m => m.CompnayName).ToPagedList(page, pageSize);
            return View(model);

        }
        [HttpPost]
        public ActionResult Index(string companyusername, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.OutsourcingCompany> outsourcingcompanys = null;
            if (!string.IsNullOrWhiteSpace(companyusername))
            {
                outsourcingcompanys = outsourcingcompanyDAL.GetOutsourcingCompanyList(companyusername).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = companyusername;
            }
            else
                outsourcingcompanys = outsourcingcompanyDAL.GetOutsourcingCompanyList(string.Empty).AsQueryable();
            var model = outsourcingcompanys.OrderBy(m => m.CompnayName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }
        // GET: Outsourcing/BasicInformation/Details/5
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {
            EF.OutsourcingCompany outsourcingcompany = null;
            if (id > 0)
            {
                outsourcingcompany = outsourcingcompanyDAL.GetOutsourcingCompany(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(outsourcingcompany);
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
                    var outsourcingcompany = outsourcingcompanyDAL.GetOutsourcingCompany(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count = outsourcingcompanyDAL.Delete(outsourcingcompany);
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
            IEnumerable<EF.OutsourcingCompany> outsourcingcompanys = null;

            EF.OutsourcingCompany outsourcingcompany = outsourcingcompanyDAL.GetOutsourcingCompany(id);
             if (reviewed == 3)  //解禁标志
            {
                //解禁后置当前状态为通过状态；
                outsourcingcompany.AuditingStatue = 1;
            }
            else
            {
                //
                outsourcingcompany.AuditingStatue = reviewed;
            }

            //outsourcingcompany.AuditingStatue = reviewed;
            outsourcingcompanyDAL.Update(outsourcingcompany);
            ViewBag.CurrentFilter = SearchString;

            outsourcingcompanys = outsourcingcompanyDAL.GetOutsourcingCompanyList(string.Empty).AsQueryable();
            if (reviewed == 1)
            {
                #region  生成新的合同项
                EF.ContractManagement contract = contractDAL.GetContractByPartner(outsourcingcompany.CompnayId);
                //EF.ContractManagement contract = new EF.ContractManagement();
                if (contract == null) //如果不存在，则添加新记录
                {
                    contract = new EF.ContractManagement();
                    contract.ID = Guid.NewGuid().ToString();
                    int k = outsourcingcompany.CompnayId;

                    contract.Code = string.Format("WB-{0}", k.ToString("0000"));
                    contract.ContractName = string.Format("{0}-外包合同", outsourcingcompany.CompnayName);
                    contract.ContractStatus = "已审核";
                    contract.ContractTypes = "外包合同";
                    contract.ContractPartner = outsourcingcompany.CompnayName;
                    contract.RegisterId = outsourcingcompany.CompnayId;
                    contract.PartnerType = "外包公司";
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
            var model = outsourcingcompanys.OrderBy(m => m.CompnayName).ToPagedList(pageIndex, pageSize);
            return View("Index", model);
        }
    }
}