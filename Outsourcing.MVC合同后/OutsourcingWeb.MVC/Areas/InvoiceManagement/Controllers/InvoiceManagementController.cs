using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.InvoiceManagement.Models;
using OutsourcingWeb.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.InvoiceManagement.Controllers
{
    /// <summary>
    /// 发票管理
    /// </summary>
    public class InvoiceManagementController : Controller
    {
        #region DAL操作对象
        //CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        //PersonsEntrySetDAL personEntryDAL = new PersonsEntrySetDAL();
        //CustomerCompnayDAL customerCompnayDAL = new CustomerCompnayDAL();
        //OutsourcingCompanyDAL outCompanyDAL = new OutsourcingCompanyDAL();
        //PersonalInfoDAL personInfoDAL = new PersonalInfoDAL();
        PersonnelSettlementDAL personnelSettDAL = new PersonnelSettlementDAL();
        //LeaveDetailDAL leaveDetailDAL = new LeaveDetailDAL(); //考勤明细
        InvoiceApplicationDAL invoiceApp = new InvoiceApplicationDAL();//发票管理
        InvoiceDetailDAL invoiceDetailDAL = new InvoiceDetailDAL();//发票明细
        #endregion

        /// <summary>
        /// 发票申请列表
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [NoCache]
        // GET: InvoiceManagement/InvoiceManagement
        public ActionResult InvoiceApply(string loginid, string type, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            InvoiceApplyViewModel viewModel = new InvoiceApplyViewModel();
            if (Session["ID"] != null)
            {
                string start = Request["startD"] == null ? "" : HttpUtility.UrlDecode(Request["startD"].ToString());
                string end = Request["endD"] == null ? "" : HttpUtility.UrlDecode(Request["endD"].ToString());
                string partner = Request["partner"] == null ? "" : HttpUtility.UrlDecode(Request["partner"].ToString());
                string[] CompanyIDs = partner == "" ? new string[] { } : partner.Split(',');

                //分组统计公司结算结果
                List<SettlementGroupModel> findlist= personnelSettDAL.GetResultGroupBy(Convert.ToInt32(Session["ID"]), type);
                List<SettlementGroupModel> newList = new List<SettlementGroupModel>();//展示数据集合
                foreach (var item in findlist)
                {
                    if (!invoiceApp.IsExit(type, item.companyID, item.Years, item.Month))
                    {
                        newList.Add(item);
                    }
                }                
                viewModel.CompanySettlements = newList.ToPagedList(page, 8);
            }

            return View(viewModel);
        }

        /// <summary>
        /// 发票申请添加
        /// </summary>
        /// <param name="formColl"></param>
        /// <returns></returns>
        // POST: InvoiceManagement/InvoiceManagement/Create
        [HttpPost]
        public ActionResult Create(FormCollection formColl)
        {
            try
            {
                if (!string.IsNullOrEmpty(formColl["loginid"]))
                { ViewBag.LoginID = formColl["loginid"]; }
                else { ViewBag.LoginID = ""; }
                if (!string.IsNullOrEmpty(formColl["type"]))
                { ViewBag.Type = formColl["type"]; }
                else { ViewBag.Type = ""; }

                DateTime dt = DateTime.Parse(formColl["ApplyDate"]);
                #region 添加发票数据
                Outsourcing.EF.InvoiceApplication model = new Outsourcing.EF.InvoiceApplication();
                model.ID = Guid.NewGuid().ToString();
                model.OutrCompanyID = int.Parse(formColl["CompanyID"]);
                model.CustomerCompanyID = Convert.ToInt32(Session["ID"]);
                model.SettlementMoney = double.Parse(formColl["Moneys"]);
                model.SettlementYear = dt.Year;
                model.SettlementMonth = dt.Month;
                model.InvoiceOutState = 1;//未开具
                model.InvoiceCustomerState = 0;// 0已申请；1已开具
                model.OperationTime = DateTime.Now;

                if (invoiceApp.Insert(model) == 1)
                {
                    #region 添加发票购买方信息
                    Outsourcing.EF.InvoiceDetail detail = new Outsourcing.EF.InvoiceDetail();
                    detail.InvoiceID = model.ID;
                    detail.InvoiceMoney = (decimal)model.SettlementMoney;
                    detail.BuyerName = formColl["CompanyName"];
                    detail.BuyerAddress = formColl["Address"];
                    detail.BuyerTel = formColl["Tel"];
                    detail.BuyerTaxpayerNumber = formColl["TaxpayerNumber"];
                    detail.BuyerBankDeposit = formColl["BankDeposit"];
                    detail.BuyerBankAccount = formColl["BankAccount"];
                    invoiceDetailDAL.Insert(detail);
                    #endregion
                }
                #endregion

                return Content("loginid=" + ViewBag.LoginID + "&&type=" + ViewBag.Type);
            }
            catch
            {
                return Content("失败");
            }
        }

        /// <summary>
        /// 发票管理列表
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [NoCache]
        public ActionResult InvoiceManager(string loginid, string type, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            InvoiceManagerViewModel viewModel = new InvoiceManagerViewModel();
            if (Session["ID"] != null)
            {
                string start = Request["startD"] == null ? "" : HttpUtility.UrlDecode(Request["startD"].ToString());
                string end = Request["endD"] == null ? "" : HttpUtility.UrlDecode(Request["endD"].ToString());
                string partner = Request["partner"] == null ? "" : HttpUtility.UrlDecode(Request["partner"].ToString());
                string[] CompanyIDs = partner == "" ? new string[] { } : partner.Split(',');

                //通过查询获取当前结果列表 
                List<Outsourcing.EF.InvoiceApplication> list = invoiceApp.GetModelList(type, Convert.ToInt32(Session["ID"]), CompanyIDs, start, end);

                viewModel.InvoiceList = list.ToPagedList(page, 8);
            }

            return View(viewModel);
        }

        // GET: InvoiceManagement/InvoiceManagement/Details/5
        public ActionResult InvoiceDetails(string id)
        {
            if (Session["ID"] != null)
            {
                string result = invoiceDetailDAL.GetModelListJSON(id);
                return Content(result);
            }

            return Content("");
        }

        // POST: InvoiceManagement/InvoiceManagement/Edit/5
        [HttpPost]
        public ActionResult EditInvoiceDetail(FormCollection formColl)
        {
            try
            {
                if (!string.IsNullOrEmpty(formColl["loginid"]))
                { ViewBag.LoginID = formColl["loginid"]; }
                else { ViewBag.LoginID = ""; }
                if (!string.IsNullOrEmpty(formColl["type"]))
                { ViewBag.Type = formColl["type"]; }
                else { ViewBag.Type = ""; }


                #region 编辑销售方发票数据
                Outsourcing.EF.InvoiceApplication model = invoiceApp.GetModel(formColl["ID"]);
                model.InvoiceOutState = 0;   //1未开具   0已开具      
                if (invoiceApp.Update(model)==1)
                {
                    #region 添加发票购买方信息
                    Outsourcing.EF.InvoiceDetail detail = invoiceDetailDAL.GetModel(model.ID);
                    if (detail != null)
                    {
                        detail.InvoiceType = int.Parse(formColl["InvoiceType"]);
                        detail.InvoiceName = formColl["InvoiceName"];
                        detail.InvoiceCode = formColl["InvoiceCode"];

                        detail.SalesName = formColl["CompanyName"];
                        detail.SalesAddress = formColl["Address"];
                        detail.SalesTel = formColl["Tel"];
                        detail.SalesTaxpayerNumber = formColl["TaxpayerNumber"];
                        detail.SalesBankDeposit = formColl["BankDeposit"];
                        detail.SalesBankAccount = formColl["BankAccount"];
                        invoiceDetailDAL.Update(detail);
                    }
                    #endregion
                }
                #endregion
                return Content("loginid=" + ViewBag.LoginID + "&&type=" + ViewBag.Type);
            }
            catch(Exception ex)
            {
                return Content("");
            }
        }

        // POST: InvoiceManagement/InvoiceManagement/Delete/5
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
