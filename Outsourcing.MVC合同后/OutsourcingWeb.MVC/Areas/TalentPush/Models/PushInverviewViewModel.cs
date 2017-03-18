using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.TalentPush.Models
{
    public class PushInverviewViewModel
    {
        /// <summary>
        /// 客户需求集合
        /// </summary>
        public IPagedList<Outsourcing.EF.Requirement> Requirements { get; set; }

        /// <summary>
        /// 客户需求
        /// </summary>
        public Outsourcing.EF.Requirement Requirement { get; set; }

        /// <summary>
        /// 需求关联的面试状态与反馈状态结合集合
        /// </summary>
        public IEnumerable<Outsourcing.EF.PushInterViewTable> PushInterViewTables { get; set; }

        /// <summary>
        /// 需求关联的客户公司集合
        /// </summary>
        public IEnumerable<Outsourcing.EF.CustomerCompnay> CustomerCompnays { get; set; }

        public Outsourcing.EF.PersonalInfo Personal { get; set; }
    }
}