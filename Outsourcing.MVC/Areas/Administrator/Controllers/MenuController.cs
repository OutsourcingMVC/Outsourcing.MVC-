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

namespace Outsourcing.MVC.Areas.Administrator.Controllers
{
   
    [Authorization]
    public class MenuController : Controller
    {
        #region  DAL操作对象
        MenuDAL menuDAL = new MenuDAL();

        //RoleDAL roleDAL = new RoleDAL();
        // GET: Administrator/Menu
        #endregion
        [NoCache]
        public ActionResult Index(string MenuName, string temp, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.Menu> menus = null;
            if (!string.IsNullOrWhiteSpace(MenuName))
            {
                string ee = HttpUtility.UrlDecode(MenuName);
                menus = menuDAL.GetMenuList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                menus = menuDAL.GetMenuList(string.Empty).AsQueryable();
            var model = menus.OrderBy(m => m.MenuName).ToPagedList(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string MenuName, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.Menu> menus = null;
            if (!string.IsNullOrWhiteSpace(MenuName))
            {
                menus = menuDAL.GetMenuList(MenuName).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = MenuName;
            }
            else
                menus = menuDAL.GetModelList(string.Empty).AsQueryable();
            var model = menus.OrderBy(m => m.MenuName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }
        // GET: Administrator/Menu/Details/5
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {
            EF.Menu menu = null;
            if (id > 0)
            {
                menu = menuDAL.GetMenu(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(menu);
        }
        // GET: Administrator/Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Menu/Create
        [HttpPost]
        public ActionResult Create(EF.Menu menu)
        {
            string msg = "添加失败";
            //YJY.EMS.Model.OA.Department member = new YJY.EMS.Model.OA.Department();
            try
            {
                // TODO: Add insert logic here
                menu.CreateUser = "admin";
                menu.UpdateUser = "admin";
                menu.UpdateTime = DateTime.Now;
                menu.CreateTime = DateTime.Now;
                menu.MenuID = menuDAL.Insert(menu);
                if (menu.MenuID <= 0)
                {
                    return Content(msg);
                }
                msg = "添加成功";
                //return RedirectToAction("Index");
                return Content(msg);
            }
            catch
            {
                //return View();
                return Content(msg);
            }
        }
        // GET: Administrator/Menu/Edit/5
        [NoCache]
        public ActionResult Edit(int id, int pageIndex, string SearchString)
        {
            EF.Menu menu = null;
            if (id > 0)
            {
                menu = menuDAL.GetMenu(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(menu);
        }

        // POST: Administrator/Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EF.Menu model)
        {
            //客户端已处理ajax重复提交问题(ajax提交一次，表单再提交一次)
            string msg = "保存失败";
            try
            {
                // TODO: Add update logic here
                //var statistical = bllStatistical.GetModel(id);
                if (id > 0 && model != null)
                {
                    //return RedirectToAction("Index");
                    model.CreateUser = "admin";
                    model.UpdateUser = "admin";
                    model.UpdateTime = DateTime.Now;
                    model.CreateTime = DateTime.Now;
                    model.MenuID = id;
                    int result = menuDAL.Update(model);
                    if (result>0)
                        msg = "保存成功";
                    //return RedirectToAction("Index");
                }
                return Content(msg);
            }
            catch
            {
                //return View();
                return Content(msg);
            }
        }
        // GET: Administrator/Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: Administrator/Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string msg = "删除失败";
            try
            {
                if (id != 0)
                {
                    var menu = menuDAL.GetMenu(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count=menuDAL.Delete(menu);
                    if (count<0)
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

        /// <summary>
        /// 加载菜单(在首页生成菜单时使用) 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns>分部页</returns>
        [HttpPost]
        public ActionResult Menu(string roleids)
        {
            var ct = new DB();
            //var role = ct.Role.Where(c => c.RoleID == 1).FirstOrDefault();
            var role = ct.Role.Where(c => c.RoleID == 1).FirstOrDefault();
            var roleMenu_Rs = role.Menu;
            string menuids = string.Empty;
            foreach (var i in roleMenu_Rs)
            {
                
               menuids = menuids + i.MenuID + ",";
                
            };
            menuids = menuids.Substring(0, menuids.Length - 1);
            var menus = menuDAL.GetModelList(string.Format(" MenuID IN ({0})", menuids));
            
            if (menus != null && menus.Count > 0)
            {
                menus = menus.Distinct().ToList();
                List<JsonMenuModel> jsonMenuModels = new List<JsonMenuModel>();
                foreach (var menu in menus)
                {
                    string id = menu.MenuID.ToString();
                    string name = menu.MenuName;
                    string pid = menu.PID.ToString();
                    JsonMenuModel model = new JsonMenuModel()
                    {
                        id = id,
                        name = name,
                        open = true,
                        pId = pid,
                        url = menu.Url,
                        icon = "",
                        title = name
                    };
                    jsonMenuModels.Add(model);
                }
                //HttpPost请求局部刷新时走这里
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_PartialMenu", jsonMenuModels);
                }
                return View(jsonMenuModels);
            }

            //this.HttpContext.Server.Transfer("~/login.html");
            //return Redirect("~/login.html");
            return View();
        }
    }
}