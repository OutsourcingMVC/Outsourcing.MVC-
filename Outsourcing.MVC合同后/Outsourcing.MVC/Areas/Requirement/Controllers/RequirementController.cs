using Outsourcing.EF.DAL;
using Outsourcing.MVC.Areas.Requirement.Models;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Outsourcing.MVC.Areas.Requirement.Controllers
{
    [Authorization]
    public class RequirementController :  Controller
    {
        #region DAL操作对象
        RequirementDAL requirementDAL = new RequirementDAL();
        CustomerCompnayDAL customercompnayDAL = new CustomerCompnayDAL();
        #endregion
        // GET: Requirement/Requirement
        [NoCache]
        public ActionResult Index(string ProjectName, string temp, int page = 1)
        {
            const int pageSize = 8;
            Models.RequirementModel viewModel = new RequirementModel();
            //IEnumerable<Model.OA.StaffMember> staffmembers = null;
            IEnumerable<EF.Requirement> Requirements = null;
            if (!string.IsNullOrWhiteSpace(ProjectName))
            {
                string ee = HttpUtility.UrlDecode(ProjectName);
                Requirements = requirementDAL.GetRequirementList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                Requirements = requirementDAL.GetRequirementList(string.Empty).AsQueryable();
            viewModel.Requirements = Requirements.OrderBy(m => m.ProjectName).ToPagedList(page, pageSize);
            viewModel.CustomerCompnays = customercompnayDAL.GetCustomerCompnayList(string.Empty);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Index(string ProjectName, int page = 1)
        {
            const int pageSize = 8;
            Models.RequirementModel viewModel = new RequirementModel();
            IEnumerable<EF.Requirement> Requirements = null;
            if (!string.IsNullOrWhiteSpace(ProjectName))
            {
                Requirements = requirementDAL.GetRequirementList(ProjectName).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ProjectName;
            }
            else
                Requirements = requirementDAL.GetRequirementList(string.Empty).AsQueryable();
            viewModel.Requirements = Requirements.OrderBy(m => m.ProjectName).ToPagedList(page, pageSize);
            viewModel.CustomerCompnays = customercompnayDAL.GetCustomerCompnayList(string.Empty);
         
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", viewModel);
            }
            return View(viewModel);
        }
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {
            Models.RequirementModel viewModel = new RequirementModel();
            EF.Requirement requirement = null;
            if (id > 0)
            {
                requirement = requirementDAL.GetRequirement(id);
            }
            if (requirement != null)
            {

                viewModel.Requirement = requirement;
                    viewModel.CustomerCompnay = customercompnayDAL.GetCustomerCompnay(Convert.ToInt32(requirement.CompnayId));
            }
                //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
                if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(viewModel);
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
                    var requirement = requirementDAL.GetRequirement(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count = requirementDAL.Delete(requirement);
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
    }
}