using Outsourcing.EF;
using Outsourcing.EF.DAL;
using Outsourcing.MVC.Areas.Administrator.Models;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.Administrator.Controllers
{
    [Authorization]
    public class RoleController : Controller
    {
        #region DAL操作对象
        RoleDAL roleDAL = new RoleDAL();
        MenuDAL menuDAL = new MenuDAL();
        #endregion
        // GET: Administrator/Role
        public ActionResult Index(string RoleName, string temp, int page = 1)
        {

            const int pageSize = 8;
            Models.RoleViewModel viewModel = new RoleViewModel();
            IEnumerable<EF.Role> roles = null;

            if (!string.IsNullOrWhiteSpace(RoleName))
            {
                string ee = HttpUtility.UrlDecode(RoleName);
                roles = roleDAL.GetRoleList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                roles = roleDAL.GetRoleList(string.Empty).AsQueryable();
            viewModel.Roles = roles.OrderBy(d => d.RoleName).ToPagedList(page, pageSize);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string RoleName, int page = 1)
        {
            const int pageSize = 8;
            Models.RoleViewModel viewModel = new RoleViewModel();
            IEnumerable<EF.Role> roles = null;

            if (!string.IsNullOrWhiteSpace(RoleName))
            {
                roles = roleDAL.GetRoleList(RoleName).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = RoleName;
            }
            else
                roles = roleDAL.GetRoleList(string.Empty).AsQueryable();

            viewModel.Roles = roles.OrderBy(d => d.RoleName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", viewModel);
            }
            return View(viewModel);
        }
        // GET: Administrator/Role/Details/5
        [NoCache]
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {
            Models.RoleViewModel viewModel = new RoleViewModel();
            if (id > 0)
            {
                viewModel.Role = roleDAL.GetRoleByID(id);
                //var roleMenu_Rs = bllRoleMenu_R.GetModelList(string.Format("RoleID={0}", id));
                //var menus = bllMenu.GetModelList(string.Empty);

                //var menuList = from m in menus
                //               join rm in roleMenu_Rs
                //            on new { m.MenuID, viewModel.Role.RoleID } equals new { rm.MenuID, rm.RoleID }
                //               //.Where(r => a.AccountID == item.AccountID);
                //               select m;
                //viewModel.Menus = menuList;
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.CurrentFilter = HttpUtility.UrlEncode(SearchString);
            return View(viewModel);
        }
        // GET: Administrator/Role/Create
        [NoCache]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Role/Create
        [HttpPost]
        public ActionResult Create(EF.Role role)
        {
            string msg = "添加失败";
            try
            {
                if (role != null)
                {
                    if (Convert.ToInt32(roleDAL.GetRole(string.Format("RoleName='{0}' ", role.RoleName))) < 1)
                    {
                        // TODO: Add insert logic here
                        //添加角色
                        //role.RoleID = bllRole.Add(role);
                        using (var ct = new DB())
                        {
                            role.CreateUser = "admin";
                            role.UpdateUser = "admin";
                            role.CreateTime = DateTime.Now;
                            role.UpdateTime = DateTime.Now;
                            // ct.Account.Add(account);
                            role.RoleID = roleDAL.Insert(role);
                            if (role.RoleID <= 0)
                            {
                                return Content(msg);
                            }
                            //添加角色关联的菜单
                            if (!string.IsNullOrWhiteSpace(Request.Form["hidTreeID"]))
                            {
                                string menus = Request.Form["hidTreeID"];
                                int maxid = ct.Role.Max(item => item.RoleID);
                                //找到新添加角色
                                Role rolemodel = ct.Role.FirstOrDefault(x => x.RoleID == maxid);

                                //找到添加的对应的菜单
                                DbSet<Menu> set = ct.Set<Menu>();
                                string sql = "select * from Menu where MenuID in (" + menus + ")";
                                //var rolemodels =  from s in ct.Role
                                //                  from p in roles
                                //                  where s.RoleID==p
                                //                         select s;
                                List<Menu> menumodels = set.SqlQuery(sql).ToList();
                                //给账户添加角色 
                                foreach (Menu menu in menumodels)
                                {
                                    rolemodel.Menu.Add(menu);
                                }
                            }
                            ct.SaveChanges();
                            msg = "添加成功";
                            return Content(msg);
                        }
                    }
                    else
                    {
                        msg = "角色名称[" + role.RoleName + "]已存在";
                        //return RedirectToAction("Index");
                        return Content(msg);
                    }
                }
                return Content(msg);
            }
            catch
            {
                //return View();
                return Content(msg);
            }
        }
        // GET: Administrator/Role/Edit/5
        [NoCache]
        public ActionResult Edit(int id, int pageIndex, string SearchString)
        {
            Models.RoleViewModel viewModel = new RoleViewModel();
            if (id > 0)
            {
                viewModel.Role = roleDAL.GetRoleByID(id);
                //var roleMenu_Rs = bllRoleMenu_R.GetModelList(string.Format(" RoleID={0}", id));
                //var menus = bllMenu.GetModelList(string.Empty);

                //var menuList = from m in menus
                //               join rm in roleMenu_Rs
                //            on new { m.MenuID, viewModel.Role.RoleID } equals new { rm.MenuID, rm.RoleID }
                //               //.Where(r => a.AccountID == item.AccountID);
                //               select m;
                //viewModel.Menus = menuList;
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.CurrentFilter = HttpUtility.UrlEncode(SearchString);
            return View(viewModel);
        }

        // POST: Administrator/Role/Edit/5
        [HttpPost]
        public ActionResult Edit(EF.Role role)
        {
            //客户端已处理ajax重复提交问题(ajax提交一次，表单再提交一次)
            string msg = "保存失败";
            EF.Role findRole = null; 
            try
            {
                if (role != null)
                {

                    ////添加角色关联的菜单
                    
                        using (var ct = new DB())
                        {
                            findRole = ct.Role.Where(a => a.RoleID == role.RoleID).Include("Menu").FirstOrDefault();
                            //删除所有数据，再重新添加
                            List<Menu> menumodeles = findRole.Menu.ToList();
                            foreach (Menu menu in menumodeles)
                            {
                             findRole.Menu.Remove(menu);
                            }
                        //找到添加的对应的菜单
                        if (!string.IsNullOrWhiteSpace(Request.Form["hidTreeID"]))
                        {
                            string menus = Request.Form["hidTreeID"];
                            DbSet<Menu> set = ct.Set<Menu>();
                            string sql = "select * from Menu where MenuID in (" + menus + ")";
                            List<Menu> menumodels = set.SqlQuery(sql).ToList();
                            role.UpdateTime = DateTime.Now;
                            //给角色添加菜单 
                            foreach (Menu menu in menumodels)
                            {
                                findRole.Menu.Add(menu);
                            }
                            
                        }
                        ct.SaveChanges();
                    }
                    ////编辑角色
                    role.UpdateUser = "admin";
                    int result = roleDAL.Update(role);
                    if (result <= 0)
                    {
                        return Content(msg);
                    }
                    msg = "保存成功";
                    return Content(msg);
                }
                return Content(msg);
            }
            catch
            {
                //return View();
                return Content(msg);
            }
        }
        // GET: Administrator/Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: Administrator/Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string msg = "删除失败";
            try
            {
                if (id != 0)
                {

                    var role = roleDAL.GetRoleByID(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    roleDAL.Delete(role);
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
        /// 取角色对应的菜单（客户端ajax调用）
        /// </summary>
        /// <returns>json</returns>
        [HttpPost]
        public ActionResult GetMenuJsonByRole(int? roleid)
        {
            //string msg = "加载失败";
            //bool check=false;
            try
            {
                //var menuList = null;
                if (roleid.HasValue)
                {
                    //var role = roleDAL.GetRoleByID(roleid.Value);
                    //var roleMenu_Rs = bllRoleMenu_R.GetModelList(string.Format(" RoleID={0}", roleid));
                    var roleMenu_Rs = roleDAL.GetRoleByID(roleid.Value).Menu;
                    var menus = menuDAL.GetModelList("1=1");
                    var menuList = from m in menus
                                   join rm in roleMenu_Rs
                                   on new { m.MenuID } equals new { rm.MenuID } into temp
                                   //.Where(r => a.AccountID == item.AccountID);
                                   from tt in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       m
                                       ,
                                       ischecked = (tt == null ? false : true)
                                   };
                    List<JsonMenuModel> jsonMenuModels = new List<JsonMenuModel>();
                    foreach (var menu in menuList)
                    {
                        string id = menu.m.MenuID.ToString();
                        string name = menu.m.MenuName;
                        string pid = menu.m.PID.ToString();
                        bool ischecked = menu.ischecked;
                        JsonMenuModel model = new JsonMenuModel() { id = id, name = name, open = true, pId = pid, url = "", icon = "", title = name, ischecked = ischecked };
                        jsonMenuModels.Add(model);
                    }
                    return Json(jsonMenuModels);
                }
                else
                {
                    var menuList = menuDAL.GetModelList("1=1");
                    List<JsonMenuModel> jsonMenuModels = new List<JsonMenuModel>();
                    foreach (var menu in menuList)
                    {
                        string id = menu.MenuID.ToString();
                        string name = menu.MenuName;
                        string pid = menu.PID.ToString();
                        JsonMenuModel model = new JsonMenuModel() { id = id, name = name, open = false, pId = pid, url = "", icon = "", title = name };
                        jsonMenuModels.Add(model);
                    }
                    return Json(jsonMenuModels);
                }
                //if (menuList != null && menuList.Count() > 0)
                //{
                //StringBuilder sb = new StringBuilder();
                //sb.Append("[");
                //sb.Append("{\"id\": 0, \"pId\": \"-1\", \"name\": \"OA \", \"open\": \"true\" ,\"title\":\"OA\"},");
                //foreach (var menu in menuList)
                //{
                //    string id = menu.MenuID.ToString();
                //    string name = menu.MenuName;
                //    string pid = menu.PID.ToString();
                //    JosnMenuModel model = new JosnMenuModel() { id = id, name = name, open = false, pId = pid, url = "", icon = "", title = name };
                //    sb.Append(JsonHelper.DropToJson<JosnMenuModel>(model,"JSON"));
                //}
                ////替换掉最后一个逗号
                //sb.Replace(",", "", sb.Length - 1, 1);
                //sb.Append("]");
                //}
                //viewModel.Menus = menuList;
                //List<Model.OA.Menu> menu;
                //return RedirectToAction("Index");
            }
            catch
            {
                return Content(string.Empty);
                //return View();
            }
        }
    }
}