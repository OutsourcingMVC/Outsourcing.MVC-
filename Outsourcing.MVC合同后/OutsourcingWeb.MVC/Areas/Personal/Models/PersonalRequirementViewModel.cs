using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.EF;

namespace OutsourcingWeb.MVC.Areas.Personal.Models
{
    public class PersonalRequirementViewModel
    {
        /// <summary>
        /// 被推送人员的集合
        /// </summary>
        public IPagedList<Outsourcing.EF.PersonalInfo> Personals { get; set; }

        /// <summary>
        /// 当前需求
        /// </summary>
        public Outsourcing.EF.Requirement Requirement { get; set; }
    }
}