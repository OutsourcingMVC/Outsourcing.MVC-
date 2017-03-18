using Outsourcing.EF.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.InvoiceManagement.Models
{
    /// <summary>
    /// 发票申请视图模型
    /// </summary>
    public class InvoiceApplyViewModel
    {
        /// <summary>
        /// 结算人员的集合(分组后)
        /// </summary>
        public IPagedList<SettlementGroupModel> CompanySettlements { get; set; }
    }

    public class InvoiceManagerViewModel
    {
        /// <summary>
        /// 结算人员的集合
        /// </summary>
        public IPagedList<Outsourcing.EF.InvoiceApplication> InvoiceList { get; set; }

      
    }
}