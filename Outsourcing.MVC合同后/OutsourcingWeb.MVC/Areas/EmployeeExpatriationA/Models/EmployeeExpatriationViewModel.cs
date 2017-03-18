using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.EmployeeExpatriationA.Models
{
    /// <summary>
    /// 人员派遣视图模型
    /// </summary>
    public class EmployeeExpatriationViewModel
    {
        /// <summary>
        /// 人员派遣单列表 
        /// </summary>
        public IPagedList<Outsourcing.EF.EmployeeExpatriation> List;
    }
}