using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outsourcing.MVC.Areas.FinancialManagement.Models
{
    public class FinancialManagementViewModel
    {
        /// <summary>
        /// 财务管理集合
        /// </summary>
        public IPagedList<EF.FinancialManagement> FinancialManagements { get; set; }


        /// <summary>
        /// 财务管理实例
        /// </summary>
        public EF.FinancialManagement FinancialManagement { get; set; }
    }
}