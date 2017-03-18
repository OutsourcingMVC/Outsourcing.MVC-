using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outsourcing.MVC.Areas.ContractManagement.Models
{
    /// <summary>
    /// 合同管理视图模型
    /// </summary>
    public class ContractManagementViewModel
    {
        /// <summary>
        /// 合同管理集合
        /// </summary>
        public IPagedList<EF.ContractManagement> ContractManagements { get; set; }


        /// <summary>
        /// 合同管理实例
        /// </summary>
        public EF.ContractManagement ContractManagement { get; set; }
    }
}