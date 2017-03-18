using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.EmployeeExpatriationA.Models;
using System.Web.Script.Serialization;
using Outsourcing.EF;
using OutsourcingWeb.MVC.Areas.CooperativeContractA.Controllers;
using System.IO;

namespace OutsourcingWeb.MVC.Areas.EmployeeExpatriationA.Controllers
{
    public class EmployeeExpatriationCController : Controller
    {
        #region dal操作对象
        //人员派遣数据操作类
        EmployeeExpatriationDAL employeeDAL = new EmployeeExpatriationDAL();
        //合同内容数据操作类
        CooperativeContractDAL cooperativeContractDAL = new CooperativeContractDAL();
        #endregion

        /// <summary>
        /// 派遣人员列表
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="oldPage">全同当前页</param>
        /// <param name="FirstID">甲方标识</param>
        /// <param name="SecondID">乙方标识</param>
        /// <param name="page">人员派遣单当前页</param>
        /// <returns></returns>
        [Common.NoCache]
        public ActionResult EmployeeExpatriationIndex(string loginid, string type,int oldPage,int FirstID,int SecondID, int page=1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            ViewBag.OldPage = oldPage;
            ViewBag.FirstID = FirstID;
            ViewBag.SecondID = SecondID;

            //人员派遣视图模型
            EmployeeExpatriationViewModel viewModel = new EmployeeExpatriationViewModel();

            viewModel.List = employeeDAL
                            .GetModelList(FirstID, SecondID)
                            .ToPagedList(page, 8);

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
        public ActionResult GetContract(string loginid, string type, string ID)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            string contentStr = string.Empty;
            EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(ID);           
            
            if (employeeModel != null)
            {
                ContractReview cReview;
                string firstContent = employeeModel.FirstContent.Replace("{甲方}", employeeModel.CustomerCompnay.CompnayName);
                firstContent = firstContent.Replace("{乙方}", employeeModel.OutsourcingCompany.CompnayName);
                cReview.ContractContent = firstContent + employeeModel.PersonList+employeeModel.SecondContent;
                cReview.ReviewContent = employeeModel.ReviewContract == null ? "" : HttpUtility.UrlDecode(employeeModel.ReviewContract);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                contentStr = jss.Serialize(cReview);
            }

            return Content(contentStr);
        }


        /// <summary>
        /// 同意合同
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult AgreenContract(string loginid, string type, int oldPage, int FirstID, int SecondID, int page,string ID)
        {
            try
            {
                EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(ID);
                if (employeeModel != null)
                {
                    // 0：同意 1：审阅中
                    switch (type)
                    {
                        case "1":
                            employeeModel.SecondPartyStatus = 0;
                            break;
                        case "2":
                            employeeModel.FirstPartyStatus = 0;
                            break;
                    }
                    employeeDAL.Update(employeeModel);
                    
                }
            }
            catch
            {
                
            }

            string str = string.Format("loginid={0}&&type={1}&&page={2}&&", loginid, type,page);
            str += string.Format("oldPage={0}&&FirstID={1}&&SecondID={2}", oldPage, FirstID, SecondID);
            //return RedirectToAction("CooperativeContractIndex", new { loginid = loginid, type = type, page = page });
            return Content(str);
        }

        /// <summary>
        /// 上传文件合同
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData, string ContractNum, string IDf)
        {
            if (fileData != null)
            {
                try
                {
                    #region 更新当前上传人员派遣合同
                    EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(IDf);
                    if (employeeModel != null)
                    {

                       CooperativeContract cooperativeModel= cooperativeContractDAL.GetModel(employeeModel.FirstPartyID.Value, employeeModel.SecondPartyID.Value);
                        // 文件上传后的保存路径
                        string filePath = Server.MapPath("~/UploadFile/") + cooperativeModel == null ? "" : cooperativeModel.ContractCode;
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                        string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                        string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                        fileData.SaveAs(filePath + "/" + saveName);

                        employeeModel.UploadFilePath = "~/UploadFile/" + cooperativeModel == null ? "" : cooperativeModel.ContractCode;
                        employeeModel.ContactFileFul = saveName;
                        employeeDAL.Update(employeeModel);

                        return Json(new { Success = true, FileName = fileName, SaveName = saveName });
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            //else
            //{
            //    return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            //}
            return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
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
                EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(ID);
                if (employeeModel != null)
                {
                    switch (type)
                    {
                        //甲方先上传盖章还是乙方先上传 0甲方；1乙方                        
                        case "1":
                            employeeModel.FirstOrSecondDownload = 1;
                            break;
                        case "2":
                            employeeModel.FirstOrSecondDownload = 0;
                            break;
                    }
                    employeeDAL.Update(employeeModel);
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
                EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(ID);
                
                if (employeeModel != null)
                {
                    // 文件上传后的保存路径
                    string filePath = employeeModel.UploadFilePath + "/" + employeeModel.ContactFileFul;
                    string filename = employeeModel.ContactFileFul;
                    string absoluFilePath = Server.MapPath(filePath);
                    return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(filename));
                }
                #endregion
            }
            catch(Exception ex) { }

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
        public ActionResult EffectiveStatusUpdate(string loginid, string type, int oldPage, int FirstID, int SecondID, int page, string ID)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(ID);
           
            if (employeeModel != null)
            {
                #region 修改内容
                //盖章合同是否生效 0生效；1未生效
                switch (type)
                {
                    case "1":
                        employeeModel.SecondPartyEffectiveStatus = 1;
                        break;
                    case "2":
                        employeeModel.FirstPartEffectiveStatus = 1;
                        break;
                }
                //如果 甲方、乙方合同状态生效，则整个合同状态生效；
                if (employeeModel.FirstPartEffectiveStatus == 1 && employeeModel.SecondPartyEffectiveStatus == 1)
                {
                    employeeModel.ContractStatus = 1;
                    employeeModel.EffectiveTime = DateTime.Now;
                }
                employeeDAL.Update(employeeModel);
                #endregion
            }
            string str = string.Format("loginid={0}&&type={1}&&page={2}&&", loginid, type, page);
            str += string.Format("oldPage={0}&&FirstID={1}&&SecondID={2}", oldPage, FirstID, SecondID);
            //return RedirectToAction("CooperativeContractIndex", new { loginid = loginid, type = type, page = page });

            return Content(HttpUtility.UrlEncode(str));
        }

        /// <summary>
        /// 更新审阅内容
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>        
        public ActionResult UpdateContractReview(string loginid, string ID, string ReviewContent)
        {
            string str = "审阅失败";
            try
            {
                EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(ID);
                
                if (employeeModel != null)
                {
                    employeeModel.ReviewContract = ReviewContent; //存放审阅备注内容
                    employeeModel.ReviewTime = DateTime.Now;
                    employeeModel.ReviewUserID = int.Parse(loginid);
                    employeeDAL.Update(employeeModel);
                    str = "审阅成功";
                }
            }
            catch
            {
                str = "审阅失败";
            }
            return Content(str);
        }


        // POST: EmployeeExpatriationA/EmployeeExpatriationC/Delete/5
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
}
