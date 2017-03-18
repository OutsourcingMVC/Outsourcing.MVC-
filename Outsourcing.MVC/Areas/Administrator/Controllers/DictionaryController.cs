using Outsourcing.EF.DAL;
using Outsourcing.Framework.Utility;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.Administrator.Controllers
{
    [Authorization]
    public class DictionaryController : Controller
    {
        #region  DAL操作对象
        DictionaryDAL dictionaryDAL = new DictionaryDAL();
        DictionaryItemDAL dictionaryItemDAL = new DictionaryItemDAL();
        #endregion
        // GET: Administrator/Dictionary字典列表分页
        [NoCache]
        public ActionResult Index(string DictionaryName, string temp, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.Dictionary> dictionarys = null;
            if (!string.IsNullOrWhiteSpace(DictionaryName))
            {
                string ee = HttpUtility.UrlDecode(DictionaryName);
                dictionarys = dictionaryDAL.GetDictionaryList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                dictionarys = dictionaryDAL.GetDictionaryList(string.Empty).AsQueryable();
            var model = dictionarys.OrderBy(m => m.ClassName).ToPagedList(page, pageSize);
            return View(model);
        }
        /// <summary>
        /// 分页字典数据操作
        /// </summary>
        /// <param name="DictionaryName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string DictionaryName, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.Dictionary> dictionarys = null;
            if (!string.IsNullOrWhiteSpace(DictionaryName))
            {
                dictionarys = dictionaryDAL.GetDictionaryList(DictionaryName).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = DictionaryName;
            }
            else
                dictionarys = dictionaryDAL.GetDictionaryList(string.Empty).AsQueryable();
            var model = dictionarys.OrderBy(m => m.ClassName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 添加新的字典数据操作
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(EF.Dictionary dictionary)
        {
            string msg = "添加失败";
            //YJY.EMS.Model.OA.Department member = new YJY.EMS.Model.OA.Department();
            try
            {
                // TODO: Add insert logic here
                dictionary.CreateUser = "admin";
                dictionary.UpdateUser = "admin";
                dictionary.UpdateTime = DateTime.Now;
                dictionary.CreateTime = DateTime.Now;
                dictionary.DictionaryID = dictionaryDAL.Insert(dictionary);
                if (dictionary.DictionaryID <= 0)
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
        /// <summary>
        /// 查询字典数据操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="SearchString"></param>
        /// <returns></returns>
        [NoCache]
        public ActionResult Edit(int id, int pageIndex, string SearchString)
        {
            EF.Dictionary dictionary = null;
            if (id > 0)
            {
                dictionary = dictionaryDAL.GetDictionary(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(dictionary);
        }
        /// <summary>
        /// 编辑字典数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, EF.Dictionary model)
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
                    model.DictionaryID = id;
                    int result = dictionaryDAL.Update(model);
                    if (result > 0)
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
        /// <summary>
        /// 删除字典数据操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string msg = "删除失败";
            try
            {
                if (id != 0)
                {
                    var dictionary = dictionaryDAL.GetDictionary(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count = dictionaryDAL.Delete(dictionary);
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
        public ActionResult DictionaryItemIndex(int Dictionaryid,string DictionaryItemName, string temp, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.DictionaryItem> dictionarys = null;
            if (!string.IsNullOrWhiteSpace(DictionaryItemName))
            {
                string ee = HttpUtility.UrlDecode(DictionaryItemName);
                dictionarys = dictionaryItemDAL.GetDictionaryItemModelList("DictionaryID="+ Dictionaryid + " and ItemName="+ DictionaryItemName + "").AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
                
            }
            else
                dictionarys = dictionaryItemDAL.GetDictionaryItemModelList("DictionaryID=" + Dictionaryid + "").AsQueryable();
            //int ee = HttpUtility.UrlDecode(Dictionaryid);
            ViewBag.DictionaryID = HttpUtility.UrlDecode(Dictionaryid.ToString());
            var model = dictionarys.OrderBy(m => m.ItemName).ToPagedList(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult DictionaryItemIndex(int Dictionaryid,string DictionaryItemName, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.DictionaryItem> dictionaryItems = null;
            if (!string.IsNullOrWhiteSpace(DictionaryItemName))
            {
                dictionaryItems = dictionaryItemDAL.GetDictionaryItemModelList("DictionaryID=" + Dictionaryid + " and ItemName=" + DictionaryItemName + "").AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = DictionaryItemName;
            }
            else
                dictionaryItems = dictionaryItemDAL.GetDictionaryItemModelList("DictionaryID=" + Dictionaryid + "").AsQueryable();
            ViewBag.DictionaryID = HttpUtility.UrlDecode(Dictionaryid.ToString());
            var model = dictionaryItems.OrderBy(m => m.ItemName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageDictionaryItemIndex", model);
            }
            return View(model);
        }
        public ActionResult DictionaryItemCreate(int DictionaryID)
        {
            ViewBag.DictionaryID = HttpUtility.UrlDecode(DictionaryID.ToString());
            return View();

        }
        /// <summary>
        /// 添加字典子项操作
        /// </summary>
        /// <param name="DictionaryID"></param>
        /// <param name="dictionaryItem"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DictionaryItemCreate(int DictionaryID,EF.DictionaryItem dictionaryItem)
        {
            string msg = "添加失败";
            //YJY.EMS.Model.OA.Department member = new YJY.EMS.Model.OA.Department();
            try
            {
                // TODO: Add insert logic here
                dictionaryItem.DictionaryID = DictionaryID;
                dictionaryItem.CreateUser = "admin";
                dictionaryItem.UpdateUser = "admin";
                dictionaryItem.UpdateTime = DateTime.Now;
                dictionaryItem.CreateTime = DateTime.Now;
                dictionaryItem.DictionaryID = dictionaryItemDAL.Insert(dictionaryItem);
                if (dictionaryItem.DictionaryID <= 0)
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
        public ActionResult DictionaryItemEdit(int DictionaryID,int id, int pageIndex, string SearchString)
        {
            EF.DictionaryItem dictionaryItem = null;
            if (id > 0)
            {
                dictionaryItem = dictionaryItemDAL.GetDictionaryItem(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            ViewBag.DictionaryID = HttpUtility.UrlEncode(DictionaryID.ToString());
            return View(dictionaryItem);
        }
        /// <summary>
        /// 编辑字典子项操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="DictionaryID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DictionaryItemEdit(int id,int DictionaryID, EF.DictionaryItem model)
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
                    model.DictionaryItemID = id;
                    model.DictionaryID = DictionaryID;
                    int result = dictionaryItemDAL.Update(model);
                    if (result > 0)
                        msg = "保存成功";
                    //return RedirectToAction("Index");
                }
                return Content(msg);
            }
            catch(Exception e)
            {
                //return View();
                return Content(msg);
            }
        }
        /// <summary>
        /// 删除字典子项操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DictionaryItemDelete(int id, FormCollection collection)
        {
            string msg = "删除失败";
            try
            {
                if (id != 0)
                {
                    var dictionaryItem = dictionaryItemDAL.GetDictionaryItem(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count = dictionaryItemDAL.Delete(dictionaryItem);
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