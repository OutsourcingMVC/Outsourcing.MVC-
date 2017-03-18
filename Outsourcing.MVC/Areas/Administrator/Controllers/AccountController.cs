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
    public class AccountController : Controller
    {
        #region DAL操作对象
        AccountDAL accountDAL = new AccountDAL();
        RoleDAL roleDAL = new RoleDAL();
        #endregion
        // GET: Administrator/Account
        [NoCache]
        public ActionResult Index(string AccountName, string temp, int page = 1)
        {

            const int pageSize = 8;
            Models.AccountViewModel viewModel = new AccountViewModel();
            IEnumerable<EF.Account> accounts = null;
            if (!string.IsNullOrWhiteSpace(AccountName))
            {
                string accountName = HttpUtility.UrlDecode(AccountName);
                accounts = accountDAL.GetModelList(accountName).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = accountName;
            }
            else
                accounts = accountDAL.GetModelList(string.Empty).AsQueryable();
            viewModel.Accounts = accounts.OrderBy(a => a.AccountName).ToPagedList(page, pageSize);
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Index(string AccountName, int page = 1)
        {
            const int pageSize = 8;
            Models.AccountViewModel viewModel = new AccountViewModel();
            IEnumerable<EF.Account> accounts = null;
            if (!string.IsNullOrWhiteSpace(AccountName))
            {
                string accountName = HttpUtility.UrlDecode(AccountName);
                accounts = accountDAL.GetModelList(accountName).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = accountName;
            }
            else
                accounts = accountDAL.GetModelList(string.Empty).AsQueryable();
            var roles = roleDAL.GetModelList("1=1");
            // var accountRole_Rs = bllAccountRole_R.GetModelList(string.Empty);
            viewModel.Accounts = accounts.OrderBy(a => a.AccountName).ToPagedList(page, pageSize);
            /// viewModel.Roles = roles;
            // viewModel.AccountRole_Rs = accountRole_Rs;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", viewModel);
            }
            return View(viewModel);
        }
        // GET: Administrator/Account/Details/5
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {
            //组装viewModel
            Models.AccountViewModel viewModel = new AccountViewModel();
            if (id > 0)
            {
                var account = accountDAL.GetAccoutRole(id);
                if (account != null)
                {
                    //var account = staffMember.AccountID.HasValue ? bllAccount.GetModel(staffMember.AccountID.Value) : null;
                    //var department = staffMember.DepartmentID.HasValue ? bllDepartment.GetModel(staffMember.DepartmentID.Value) : null;
                    viewModel.Account = account;
                }
                //var roles = bllRole.GetModelList(string.Empty);
                //var accountRole_Rs = bllAccountRole_R.GetModelList(string.Empty);
                //viewModel.Roles = roles;
                //viewModel.AccountRole_Rs = accountRole_Rs;
                ////查找用户关联的角色
                //var selectedRoles = from r in roles
                //                    join ar in accountRole_Rs
                //                    on new { r.RoleID, viewModel.Account.AccountID } equals new { ar.RoleID, ar.AccountID }
                //                    //.Where(r => a.AccountID == item.AccountID);
                //                    select r;

                //已选择的角色
                ViewBag.selectedRoles = new SelectList(account.Role, "RoleID", "RoleName", -1);
                //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
                if (pageIndex > 1)
                    ViewBag.pageIndex = pageIndex;
                if (!string.IsNullOrWhiteSpace(SearchString))

                    ViewBag.CurrentFilter = HttpUtility.UrlDecode(SearchString);
            }
            return View(viewModel);
        }
        // GET: Administrator/Account/Create
        [NoCache]
        public ActionResult Create()
        {
            var sysRoles = roleDAL.GetModelList(" 1=1");
            ViewBag.roles = new SelectList(sysRoles, "RoleID", "RoleName", -1);
            return View();
        }

        // POST: Administrator/Account/Create
        [HttpPost]
        public ActionResult Create(EF.Account account)
        {
            string msg = "添加失败";

            try
            {
                if (account != null)
                {
                    if (Convert.ToInt32(accountDAL.GetRecordCount(string.Format("AccountName='{0}' ", account.AccountName))) < 1)
                    {
                        // TODO: Add insert logic here
                        //添加帐户
                        using (var ct = new DB())
                        {
                            // ct.Account.Add(account);
                            account.AccountID = accountDAL.Insert(account);
                            if (account.AccountID <= 0)
                            {
                                return Content(msg);
                            }
                            //添加帐户关联的角色
                            if (!string.IsNullOrWhiteSpace(Request.Form["selectedRoles"]))
                            {
                                string roles = Request.Form["selectedRoles"];
                                int maxid = ct.Account.Max(item => item.AccountID);
                                //找到新添加账户
                                Account accountmodel = ct.Account.FirstOrDefault(x => x.AccountID == maxid);

                                //找到添加的对应的角色
                                DbSet<Role> set = ct.Set<Role>();
                                string sql = "select * from Role where RoleID in (" + roles + ")";
                                //var rolemodels =  from s in ct.Role
                                //                  from p in roles
                                //                  where s.RoleID==p
                                //                         select s;
                                List<Role> rolemodels = set.SqlQuery(sql).ToList();
                                //给账户添加角色 
                                foreach (Role role in rolemodels)
                                {
                                    accountmodel.Role.Add(role);
                                }
                            }
                            ct.SaveChanges();
                            msg = "添加成功";
                            return Content(msg);
                        }
                    }
                    else
                    {
                        msg = "帐户名[" + account.AccountName + "]已存在";
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
        // GET: Administrator/Account/Edit/5
        [NoCache]
        public ActionResult Edit(int id, int pageIndex, string SearchString)
        {
            //组装viewModel
            Models.AccountViewModel viewModel = new AccountViewModel();
            var account = accountDAL.GetAccoutRole(id);
            if (account != null)
            {
                //var account = staffMember.AccountID.HasValue ? bllAccount.GetModel(staffMember.AccountID.Value) : null;
                //var department = staffMember.DepartmentID.HasValue ? bllDepartment.GetModel(staffMember.DepartmentID.Value) : null;
                viewModel.Account = account;
            }
            var roles = roleDAL.GetModelList(" 1=1");
            //var accountRole_Rs = bllAccountRole_R.GetModelList(string.Empty);
            //viewModel.Roles = roles;
            //viewModel.AccountRole_Rs = accountRole_Rs;
            ////查找用户关联的角色
            //var selectedRoles = from r in roles
            //                    join ar in accountRole_Rs
            //                    on new { r.RoleID, viewModel.Account.AccountID } equals new { ar.RoleID, ar.AccountID }
            //                    //.Where(r => a.AccountID == item.AccountID);
            //                    select r;
            //未选择的角色
            ViewBag.roles = new SelectList(roles.Except<EF.Role>(account.Role), "RoleID", "RoleName", -1);
            //已选择的角色
            ViewBag.selectedRoles = new SelectList(account.Role, "RoleID", "RoleName", -1);
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.CurrentFilter = HttpUtility.UrlDecode(SearchString);
            return View(viewModel);
        }

        // POST: Administrator/Account/Edit/5
        [HttpPost]
        public ActionResult Edit(EF.Account account)
        {
            //客户端已处理ajax重复提交问题(ajax提交一次，表单再提交一次)
            string msg = "保存失败";
            EF.Account editAccount = account,findAccount=null;
            try
            {
                if (account != null)
                {
                    // TODO: Add insert logic here
                    //添加帐户关联的角色
                    
                        using (var ct = new DB())
                        {
                        //account = ct.Account.Where(a => a.AccountID == account.AccountID).Include("Role").FirstOrDefault();
                            findAccount = ct.Account.Where(a => a.AccountID == editAccount.AccountID).Include("Role").FirstOrDefault();
                            //List<Role> rolemodeles = account.Role.ToList();
                            List<Role> rolemodeles = findAccount.Role.ToList();
                            //删除所有数据，再重新添加
                            foreach (Role role in rolemodeles)
                            { 
                                account.Role.Remove(role);
                            }
                        
                        //找到添加的对应的角色
                        if (!string.IsNullOrWhiteSpace(Request.Form["selectedRoles"]))
                        {
                            string roles = Request.Form["selectedRoles"];
                            DbSet<Role> set = ct.Set<Role>();
                            string sql = "select * from Role where RoleID in (" + roles + ")";
                            List<Role> rolemodels = set.SqlQuery(sql).ToList();
                            account.UpdateTime = DateTime.Now;
                            //accountroles.Role = rolemodels;
                            //给账户添加角色 
                            foreach (Role role in rolemodels)
                            {
                                findAccount.Role.Add(role);
                            }
                        }
                        ct.SaveChanges();
                    }
                    //编辑帐户
                    int result = accountDAL.Update(editAccount);
                    if (result < 0)
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
        // POST: Administrator/Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string msg = "删除失败";
            try
            {
                // TODO: Add delete logic here
                if (id > 0)
                {
                    //using (var ct = new DB())
                    //{
                    var account = accountDAL.GetAccoutRole(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    accountDAL.Delete(account);
                    // ct.SaveChanges();
                    // }
                }
                msg = "删除成功";
                return Content(msg);
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [NoCache]
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Validation(string username, string password)
        {

            LoginMessage msg = new LoginMessage();
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    if (Convert.ToInt32(accountDAL.GetRecordCount(string.Format(" AccountName='{0}'", username))) < 1)
                    {
                        msg.ErrorCount = 1;
                        msg.Message = string.Format("用户[{0}]不存在", username);
                    }
                    else if (Convert.ToInt32(accountDAL.GetRecordCount(string.Format(" AccountName='{0}' AND [Password]='{1}'", username, password))) < 1)
                    {
                        msg.ErrorCount = 1;
                        msg.Message = string.Format("用户[{0}]密码有误", username);
                    }
                    else
                    {
                        msg.ErrorCount = 0;
                        msg.Message = string.Format("验证成功", username);
                        var ct = new DB();
                        var Account = ct.Account;
                        var account = accountDAL.GetAccountByWhere(username, password);
                        var accountRole_Rs = accountDAL.GetAccoutRole(account.AccountID);

                        string roleids = string.Empty;
                        foreach (var i in accountRole_Rs.Role)
                        {
                            roleids = roleids + i.RoleID + ",";
                        };
                        msg.RoleID = roleids.Substring(0, roleids.Length - 1);
                        var accountRolename_Rs = roleDAL.GetModelList(string.Format(" RoleID in(" + msg.RoleID + ")"));
                        string roleNames = string.Empty;
                        accountRolename_Rs.ForEach(delegate (EF.Role ar)
                        {
                            roleNames += ar.RoleName + ",";
                        });
                        msg.RoleName = roleNames.Substring(0, roleNames.Length - 1);
                        msg.AccountID = account.AccountID.ToString();
                        msg.AccountName = account.AccountName;
                        string _AuthSaveKey = "LoginedUser";
                        Session[_AuthSaveKey] = account;
                        Session["Role"] = roleids;
                    }
                }
                else
                {
                    msg.ErrorCount = 1;
                    msg.Message = string.Format("用户名或密码不能为空", username);
                }
                return Json(msg);
            }
            catch
            {
                msg.ErrorCount = 1;
                msg.Message = string.Format("系统异常");
                return Json(msg);
            }
        }
    }
}