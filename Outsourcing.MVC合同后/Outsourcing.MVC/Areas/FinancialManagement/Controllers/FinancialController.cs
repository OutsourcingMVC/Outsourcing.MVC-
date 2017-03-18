using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.EF.DAL;
using PagedList;
using Outsourcing.MVC.Common;
using Outsourcing.EF;

namespace Outsourcing.MVC.Areas.FinancialManagement.Controllers
{
    /// <summary>
    /// 财务管理
    /// </summary>
    public class FinancialController : Controller
    {
        #region DAL操作对象
        FinancialDAL financialDAL = new FinancialDAL();
        #endregion
        // GET: FinancialManagement/Financial

        [NoCache]
        public ActionResult Index(string FinancialCode, string temp, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.FinancialManagement> financialManagements = null;
            financialManagements = financialDAL.GetModelList(FinancialCode);
            if (ViewBag.pageIndex!=null && ViewBag.pageIndex!=0)
            {
                page = ViewBag.pageIndex;
            }
            var model = financialManagements.OrderBy(m => m.Code).ToPagedList(page, pageSize);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string FinancialCode, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.FinancialManagement> financialManagements = null;
            financialManagements = financialDAL.GetModelList(FinancialCode);
            var model = financialManagements.OrderBy(m => m.Code).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }

        // GET: FinancialManagement/Financial/Details/5
        [NoCache]
        public ActionResult Details(string id, int pageIndex, string SearchString)
        {            
             Models.FinancialManagementViewModel viewModel = new Models.FinancialManagementViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                viewModel.FinancialManagement = financialDAL.GetFinancialByWhere(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.CurrentFilter = HttpUtility.UrlEncode(SearchString);

            return View(viewModel);
        }

        // GET: FinancialManagement/Financial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialManagement/Financial/Create
        [HttpPost]
        public ActionResult Create(EF.FinancialManagement financialManagement)
        {
            string msg = "添加失败";
            try
            {
                financialManagement.ID = Guid.NewGuid().ToString();
                int count = financialDAL.Insert(financialManagement);
                if (count <= 0)
                {
                    return Content(msg);
                }
                msg = "添加成功";
                //return RedirectToAction("Index");
                return Content(msg);
            }
            catch
            {
                return Content(msg);
            }
        }

        // GET: FinancialManagement/Financial/Edit/5
        [NoCache]
        public ActionResult Edit(string id, int pageIndex, string SearchString)
        {
            //组装viewModel
            Models.FinancialManagementViewModel viewModel = new Models.FinancialManagementViewModel();
            var Financial = financialDAL.GetFinancialByWhere(id);
            if (Financial != null)
            {
                viewModel.FinancialManagement = Financial;
            }

            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.CurrentFilter = HttpUtility.UrlDecode(SearchString);
            return View(viewModel);            
        }

        // POST: FinancialManagement/Financial/Edit/5
        [HttpPost]
        public ActionResult Edit(EF.FinancialManagement financialManagement)
        {
            //客户端已处理ajax重复提交问题(ajax提交一次，表单再提交一次)
            string msg = "保存失败";
            try
            {
                if (financialManagement != null)
                {
                    //更新内容
                    int result = financialDAL.Update(financialManagement);
                    if (result < 0)
                    {
                        return Content(msg);
                    }
                    msg = "保存成功";
                    return Content(msg);
                }
                return Content(msg);
            }
            catch(Exception ex)
            {
                return Content(msg);
            }
        }

        // GET: FinancialManagement/Financial/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialManagement/Financial/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, int pageIndex, string SearchString)
        {
            string msg = "删除失败";
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(id))
                {
                    EF.FinancialManagement financialManagement = financialDAL.GetFinancialByWhere(id);
                    if (financialManagement != null)
                        financialDAL.Delete(financialManagement);

                }
                //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
                if (pageIndex > 1)
                    ViewBag.pageIndex = pageIndex;
                if (!string.IsNullOrWhiteSpace(SearchString))
                    ViewBag.CurrentFilter = HttpUtility.UrlEncode(SearchString);

                msg = "删除成功";
                return Content(msg);
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
