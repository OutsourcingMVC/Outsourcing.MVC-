using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outsourcing.MVC.Areas.Requirement.Models
{
    public class RequirementModel
    {
        /// <summary>
        ///需求集合
        /// </summary>
        public IPagedList<EF.Requirement> Requirements { get; set; }
        /// <summary>
        /// 需求关联的客户公司集合
        /// </summary>
        public IEnumerable<EF.CustomerCompnay> CustomerCompnays { get; set; }
        /// <summary>
        ///单个需求
        /// </summary>
        public EF.Requirement Requirement { get; set; }
        /// <summary>
        /// 单个需求关联的客户公司
        /// </summary>
        public EF.CustomerCompnay CustomerCompnay { get; set; }
    }

}