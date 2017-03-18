using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.CompanySettlement.Models
{
    public class CompanySettlementViewModel
    {
        /// <summary>
        /// 结算人员的集合
        /// </summary>
        public IPagedList<Outsourcing.EF.PersonSettlement> PersonSettlements { get; set; }

        /// <summary>
        /// 结算金额总计
        /// </summary>
        public double TotalMoney { get; set; }

        /// <summary>
        /// 合作公司
        /// </summary>
        public Dictionary<int, string> PatternList { get; set; }

    }
}