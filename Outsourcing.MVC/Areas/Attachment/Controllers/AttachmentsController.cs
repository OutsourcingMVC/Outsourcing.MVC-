using Outsourcing.EF.DAL;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.Attachment
{
    [Authorization]
    public class AttachmentsController : Controller
    {
        #region DAL操作对象
        AttachmentDAL attachmentDAL = new AttachmentDAL();
        #endregion
        // GET: Attachment/Attachments
        /// <summary>
        /// 附件管理的分页操作
        /// </summary>
        /// <param name="AttachmentName"></param>
        /// <param name="temp"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [NoCache]
        public ActionResult Index(string AttachmentName, string temp, int page = 1)
        {
            
            const int pageSize = 8;
            IEnumerable<EF.Attachment> attachments = null;
            if (!string.IsNullOrWhiteSpace(AttachmentName))
            {
                string ee = HttpUtility.UrlDecode(AttachmentName);
                attachments = attachmentDAL.GetAttachmentList(ee).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = ee;
            }
            else
                attachments = attachmentDAL.GetAttachmentList(string.Empty).AsQueryable();
            var model = attachments.OrderBy(m => m.AttachmentName).ToPagedList(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string AttachmentName, int page = 1)
        {
            const int pageSize = 8;
            IEnumerable<EF.Attachment> attachments = null;
            if (!string.IsNullOrWhiteSpace(AttachmentName))
            {
                attachments = attachmentDAL.GetAttachmentList(AttachmentName).AsQueryable();
                //departments.Where(d => d.DepartmentName.Contains(DepartmentName));
                ViewBag.CurrentFilter = AttachmentName;
            }
            else
                attachments = attachmentDAL.GetAttachmentList(string.Empty).AsQueryable();
            var model = attachments.OrderBy(m => m.AttachmentName).ToPagedList(page, pageSize);
            //HttpPost请求局部刷新时走这里
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialPageIndex", model);
            }
            return View(model);
        }
        /// <summary>
        /// 附件管理页面的查询操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="SearchString"></param>
        /// <returns></returns>
        // GET: Attachment/Attachments/Details/5
        public ActionResult Details(int id, int pageIndex, string SearchString)
        {
            EF.Attachment attachment = null;
            if (id > 0)
            {
                attachment = attachmentDAL.GetAttachment(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(attachment);
        }
        // GET: Attachment/Attachments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attachment/Attachments/Create
        [HttpPost]
        public ActionResult Create(EF.Attachment attachment)
        {
            string virtualPath = "";
            string msg = "添加失败";
            //YJY.EMS.Model.OA.Department member = new YJY.EMS.Model.OA.Department();
            try
            {
                // TODO: Add insert logic here
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase filec = Request.Files[0];
                    // HttpPostedFileBase files = Request.Files[1];
                    //// 如果没有上传文件
                    //if (file == null || string.IsNullOrWhiteSpace(filec.FileName) || file.ContentLength == 0)
                    //{
                    //        msg = "未检测到上传的文件，请重新上传";
                    //       return Content(msg);

                    //}
                    //允许的文件类型
                    string[] extension = { ".jpg", ".docx", "doc", ".pdf" };

                    if (!extension.Contains(System.IO.Path.GetExtension(filec.FileName)))
                    {
                        msg = "导入的文件必须是excel格式";
                        return Content(msg);
                    }

                    //Console.WriteLine(extension.Contains(System.IO.Path.GetExtension(file.FileName)));
                    // 保存到 {~/UploadFile/OA/AttendanceMonthStatistical/日期/文件名} 文件夹中，名称不变
                    string filename = System.IO.Path.GetFileName(filec.FileName);
                    //string filen = System.IO.Path.GetFileName(files.FileName);
                    virtualPath =
                          string.Format("/UploadFile/Areas/Attachment/ContactFile/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd"), filename);
                    //bmxyPath = string.Format("/UploadFile/Areas/Attachment/SecrecyAgreement/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd"), filen);

                    //文件系统不能使用虚拟路径
                    string path = this.Server.MapPath(virtualPath);
                    // string path1 = this.Server.MapPath(bmxyPath);
                    DirectoryInfo dirInfo = Directory.GetParent(path);
                    //DirectoryInfo dirInfo1 = Directory.GetParent(path1);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                    //if (!dirInfo1.Exists)
                    //{
                    //    dirInfo1.Create();
                    //}
                    filec.SaveAs(path);
                    // files.SaveAs(path1);
                    attachment.DocPath = virtualPath;

                }
                attachment.CreateUser = "admin";
                attachment.UpdateUser = "admin";
                attachment.UpdateTime = DateTime.Now;
                attachment.CreateTime = DateTime.Now;
                attachment.AttachmentId = attachmentDAL.Insert(attachment);
                if (attachment.AttachmentId <= 0)
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
        // GET: Attachment/Attachments/Edit/5
        [NoCache]
        public ActionResult Edit(int id, int pageIndex, string SearchString)
        {
            EF.Attachment attachment = null;
            if (id > 0)
            {
                attachment = attachmentDAL.GetAttachment(id);
            }
            //记录列表的页码以及查询条件，便于从编辑、详情等页面返回列表页时使用
            if (pageIndex > 1)
                ViewBag.pageIndex = pageIndex;
            if (!string.IsNullOrWhiteSpace(SearchString))
                ViewBag.SearchString = HttpUtility.UrlEncode(SearchString);
            return View(attachment);
        }

        // POST: Attachment/Attachments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EF.Attachment model,FormCollection collection)
        {
            string virtualPath = "";
            string bmxyPath = "";
            //客户端已处理ajax重复提交问题(ajax提交一次，表单再提交一次)
            string msg = "保存失败";
            try
            {
                // TODO: Add update logic here
                //var statistical = bllStatistical.GetModel(id);
                if (id > 0 && model != null)
                {
                    if (Request.Files.Count > 0)
                    {

                        HttpPostedFileBase filec = Request.Files[0];
                       // HttpPostedFileBase files = Request.Files[1];
                        //// 如果没有上传文件
                        //if (file == null || string.IsNullOrWhiteSpace(filec.FileName) || file.ContentLength == 0)
                        //{
                        //        msg = "未检测到上传的文件，请重新上传";
                        //       return Content(msg);

                        //}
                        //允许的文件类型
                        string[] extension = { ".jpg", ".docx", "doc", ".pdf" };

                        if (!extension.Contains(System.IO.Path.GetExtension(filec.FileName)))
                        {
                            msg = "导入的文件必须是excel格式";
                            return Content(msg);
                        }
                       
                        //Console.WriteLine(extension.Contains(System.IO.Path.GetExtension(file.FileName)));
                        // 保存到 {~/UploadFile/OA/AttendanceMonthStatistical/日期/文件名} 文件夹中，名称不变
                        string filename = System.IO.Path.GetFileName(filec.FileName);
                        //string filen = System.IO.Path.GetFileName(files.FileName);
                        virtualPath =
                              string.Format("/UploadFile/Areas/Attachment/ContactFile/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd"), filename);
                        //bmxyPath = string.Format("/UploadFile/Areas/Attachment/SecrecyAgreement/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd"), filen);

                        //文件系统不能使用虚拟路径
                        string path = this.Server.MapPath(virtualPath);
                       // string path1 = this.Server.MapPath(bmxyPath);
                        DirectoryInfo dirInfo = Directory.GetParent(path);
                        //DirectoryInfo dirInfo1 = Directory.GetParent(path1);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }
                        //if (!dirInfo1.Exists)
                        //{
                        //    dirInfo1.Create();
                        //}
                        filec.SaveAs(path);
                        // files.SaveAs(path1);
                        model.DocPath = virtualPath;
                        //return RedirectToAction("Index");
                        model.CreateUser = "admin";
                        model.UpdateUser = "admin";
                        model.UpdateTime = DateTime.Now;
                        model.CreateTime = DateTime.Now;
                        model.AttachmentId = id;
                        int result = attachmentDAL.Update(model);
                        if (result > 0)
                            msg = "保存成功";
                        //return RedirectToAction("Index");
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
        // GET: Attachment/Attachments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: Attachment/Attachments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string msg = "删除失败";
            try
            {
                if (id != 0)
                {
                    var attachment = attachmentDAL.GetAttachment(id);
                    ////先删除子表，再删除主表
                    //foreach (var role in account.Role) {
                    //    var RoleModel = roleDAL.GetRoleByID(role.RoleID);
                    //    account.Role.Remove(RoleModel);
                    //}
                    int count = attachmentDAL.Delete(attachment);
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