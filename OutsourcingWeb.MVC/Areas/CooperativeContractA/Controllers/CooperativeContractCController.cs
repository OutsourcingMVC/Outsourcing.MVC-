using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OutsourcingWeb.MVC.Areas.CooperativeContractA.Controllers
{
    public class CooperativeContractCController : Controller
    {
        #region dal操作对象
        CooperativeContractDAL cooperativeContractDAL = new CooperativeContractDAL();
        #endregion

        /// <summary>
        /// 合作合同列表
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        // GET: CooperativeContractA/CooperativeContractC
        [NoCache]
        public ActionResult CooperativeContractIndex(string loginid, string type, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            Models.CooperativeContractViewModel viewModel = new Models.CooperativeContractViewModel();

            if (Session["ID"] != null)
            {
                string start = Request["startD"] == null ? "" : HttpUtility.UrlDecode(Request["startD"].ToString());
                string end = Request["endD"] == null ? "" : HttpUtility.UrlDecode(Request["endD"].ToString());
                string ContractCode = Request["ContractCode"] == null ? "" : HttpUtility.UrlDecode(Request["ContractCode"].ToString());
                
                viewModel.ViewList = cooperativeContractDAL.GetModelList(type,Convert.ToInt32(Session["ID"]), ContractCode, start, end)
                                     .ToPagedList(page, 8);
            }            
            return View(viewModel);
        }

        /// <summary>
        /// 获取合作合同内容
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        // GET: CooperativeContractA/CooperativeContractC/Details/5
        public ActionResult GetContract(string loginid, string type,string ID)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            string contentStr = string.Empty;
            CooperativeContract model = cooperativeContractDAL.GetModelByID(ID);
            ContractReview cr;
            if (model != null)
            {
                //contentStr ="{"+ string.Format(" \"result1\":{0},\"result2\":{1}", model.ContractContent, model.back1)+"}";
                //contentStr = model.ContractContent;
                cr.ContractContent = model.ContractContent;
                cr.ReviewContent = HttpUtility.UrlDecode(model.ContractBack);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                contentStr = jss.Serialize(cr);
            }

            return Content(contentStr);
        }

        /// <summary>
        /// 更新审阅内容
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>        
        public ActionResult UpdateContractReview(string loginid, string ID,string ReviewContent)
        {
            string str = "审阅失败";
            try
            {
                CooperativeContract model = cooperativeContractDAL.GetModelByID(ID);
                if (model != null)
                {
                    model.ContractBack = ReviewContent; //存放审阅备注内容
                    model.ReviewTime = DateTime.Now;
                    model.ReviewUserID = int.Parse(loginid);
                    cooperativeContractDAL.Update(model);
                    str = "审阅成功";
                }
            }
            catch
            {
                str = "审阅失败";
            }
            return Content(str);
        }
        /// <summary>
        /// 修改合同
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult AgreenContract(string loginid, string type,string ID,int page)
        {
            string str = "修改失败";
            try
            {
                CooperativeContract model = cooperativeContractDAL.GetModelByID(ID);
                if (model != null)
                {
                    // 0：同意 1：审阅中
                    switch (type)
                    {
                        case "1":
                            model.SecondPartyStatus = 0;
                            break;
                        case "2":
                            model.FirstPartyStatus = 0;
                            break;
                    }
                    cooperativeContractDAL.Update(model);
                    str = "修改成功";
                }
            }
            catch
            {
                str = "修改失败";
            }
            return RedirectToAction("CooperativeContractIndex", new { loginid = loginid, type = type, page = page });
            //return Content(str);
        }

        /// <summary>
        /// 上传文件合同
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData,string ContractNum,string IDf)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/UploadFile/")+ ContractNum;
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath +"/"+ saveName);

                    #region 更新当前上传合同信息
                    CooperativeContract model = cooperativeContractDAL.GetModelByID(IDf);
                    if (model != null)
                    {
                        model.UploadFilePath = "~/UploadFile/"+ContractNum; 
                        model.ContactFileFul = saveName;
                        cooperativeContractDAL.Update(model);
                    }
                    #endregion

                        return Json(new { Success = true, FileName = fileName, SaveName = saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 更新合同是由甲方或者乙方先下载
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult UpdateDownByFirstOrSecond(string ID, string type)
        {
            string str = "失败";
            try
            {
                CooperativeContract model = cooperativeContractDAL.GetModelByID(ID);
                if (model != null)
                {
                    switch (type)
                    {
                        //甲方先上传盖章还是乙方先上传 0甲方；1乙方                        
                        case "1":
                            model.FirstOrSecondDownload = 1;
                            break;
                        case "2":
                            model.FirstOrSecondDownload = 0;
                            break;
                    }
                    cooperativeContractDAL.Update(model);
                    str = "成功";
                }
            }
            catch { }
            return Content(str);
        }

        /// <summary>
        /// 下载盖章合同 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStreamResult DownFile(string ID)
        {
            try
            {
                #region 更新当前上传合同信息
                CooperativeContract model = cooperativeContractDAL.GetModelByID(ID);
                if (model != null)
                {
                    // 文件上传后的保存路径
                    
                    string filePath =model.UploadFilePath+"/"+model.ContactFileFul;
                    string filename = model.ContactFileFul;
                    string absoluFilePath = Server.MapPath(filePath);
                    return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(filename));
                }
                #endregion
            }
            catch { }

            return null;
        }

        /// <summary>
        /// 更改合同内容生效
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult EffectiveStatusUpdate(string loginid, string type, string ID, int page)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            CooperativeContract model = cooperativeContractDAL.GetModelByID(ID);
            if (model != null)
            {
                #region 修改内容
                //盖章合同是否生效 0生效；1未生效
                switch (type)
                {
                    case "1":
                        model.SecondPartyEffectiveStatus = 1;
                        break;
                    case "2":
                        model.FirstPartEffectiveStatus = 1;
                        break;
                }
                //如果 甲方、乙方合同状态生效，则整个合同状态生效；
                if (model.FirstPartEffectiveStatus == 1 && model.SecondPartyEffectiveStatus == 1)
                {
                    model.ContractStatus = 1;
                    model.EffectiveTime = DateTime.Now;
                }
                cooperativeContractDAL.Update(model);
                #endregion
            }
            string str = string.Format("?loginid={0}&&type={1}&&page={2}", loginid, type, page);
            return Content(str);
        }

        // GET: CooperativeContractA/CooperativeContractC/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CooperativeContractA/CooperativeContractC/Edit/5
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

        // GET: CooperativeContractA/CooperativeContractC/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CooperativeContractA/CooperativeContractC/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

    public struct ContractReview
    {
        public string ContractContent;
        public string ReviewContent;
    }
}
