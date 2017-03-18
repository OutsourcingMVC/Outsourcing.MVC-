using Outsourcing.EF;
using Outsourcing.EF.DAL;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.Professional.Controllers
{
    [Authorization]
    public class ProfessionalsController : Controller
    {
        #region  DAL操作对象
        PersonalInfoDAL personalinfoDAL = new PersonalInfoDAL();
        #endregion
        // GET: Professional/Professionals
        [NoCache]
        public ActionResult Index(string personname, string temp, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.PersonalInfo> personalinfos = null;
            if (!string.IsNullOrWhiteSpace(personname))
            {
               
                string ee = HttpUtility.UrlDecode(personname);
                personalinfos = personalinfoDAL.GetPersonalInfoList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                personalinfos = personalinfoDAL.GetPersonalInfoList(string.Empty).AsQueryable();
            var model = personalinfos.OrderBy(m => m.PersonName).ToPagedList(page, pageSize);
            return View(model);

        }
        [HttpPost]
        public ActionResult Index(string personname, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.PersonalInfo> personalinfos = null;
            if (!string.IsNullOrWhiteSpace(personname))
            {
                personalinfos = personalinfoDAL.GetPersonalInfoList(personname).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = personname;
            }
            else
                personalinfos = personalinfoDAL.GetPersonalInfoList(string.Empty).AsQueryable();
            var model = personalinfos.OrderBy(m => m.PersonName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {            
            EF.PersonalInfo personalinfo = null;
            if (id > 0)
            {
                personalinfo = personalinfoDAL.GetPersonalInfo(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(personalinfo);
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
                    var personalinfo = personalinfoDAL.GetPersonalInfo(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count = personalinfoDAL.Delete(personalinfo);
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