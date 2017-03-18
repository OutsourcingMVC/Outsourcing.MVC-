using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.CooperativeContractA.Models
{
    /// <summary>
    /// 合作合同视图模型
    /// </summary>
    public class CooperativeContractViewModel
    {
        /// <summary>
        /// 数据展示集
        /// </summary>
        public IPagedList<Outsourcing.EF.CooperativeContract> ViewList;
               
    }
}