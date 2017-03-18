using Outsourcing.EF.DAL;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.ContractManagement.Controllers
{
    public class ContractManagementController : Controller
    {
        #region  DAL操作对象
        ContractManagementDAL contractDAL = new ContractManagementDAL();//合同管理类的调用
        #endregion
        // GET: ContractManagement/ContractManagement
        [NoCache]
        public ActionResult Index(string ContractCode, string temp, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.ContractManagement> ContractManagementManagements = null;
            ContractManagementManagements = contractDAL.GetModelList(ContractCode);
            if (ViewBag.pageIndex != null && ViewBag.pageIndex != 0)
            {
                page = ViewBag.pageIndex;
            }
            var model = ContractManagementManagements.OrderBy(m => m.Code).ToPagedList(page, pageSize);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string ContractCode, int page = 1)
        {
            const int pageSize = 8;

            IEnumerable<EF.ContractManagement> ContractManagementManagements = null;
            string where = string.Format("code like '%{0}%'", ContractCode);

            ContractManagementManagements = contractDAL.GetModelList(where);
            var model = ContractManagementManagements.OrderBy(m => m.Code).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }

        // GET: ContractManagement/ContractManagement/Details/5
        public ActionResult Details(string id, int pageIndex, string SearchString)
        {
            ContractManagement.Models.ContractManagementViewModel viewModel = new Models.ContractManagementViewModel();
            try
            {
                viewModel.ContractManagement = contractDAL.GetContractByWhere(id);
                if (ViewBag.pageIndex != null)
                {
                    ViewBag.pageIndex = pageIndex;
                }
                if (!string.IsNullOrWhiteSpace(SearchString))
                    ViewBag.CurrentFilter = HttpUtility.UrlEncode(SearchString);
            }
            catch
            { }

            return View(viewModel);
        
        }

        // GET: ContractManagement/ContractManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractManagement/ContractManagement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractManagement/ContractManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContractManagement/ContractManagement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractManagement/ContractManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContractManagement/ContractManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, int pageIndex, string SearchString)
        {
            string msg = "删除失败";
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    EF.ContractManagement contractManagement = contractDAL.GetContractByWhere(id);
                    if (contractManagement != null)
                        contractDAL.Delete(contractManagement);

                }
                //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
                if (pageIndex > 1)
                    ViewBag.pageIndex = pageIndex;
                if (!string.IsNullOrWhiteSpace(SearchString))
                    ViewBag.CurrentFilter = HttpUtility.UrlEncode(SearchString);

                msg = "删除成功";
                return Content(msg);
            }
            catch
            {
                return View();
            }
        }
    }
}
