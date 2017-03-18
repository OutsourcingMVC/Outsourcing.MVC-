using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.PersonEntry.Models
{
    public class PersonSettlViewModel
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 工作天数
        /// </summary>
        public long workDays { get; set; }

        /// <summary>
        /// 实际工作天数
        /// </summary>
        public long RealworkDays { get; set; }

        public double LeaveDays { get; set; }

        public double OverTimeDay { get; set; }

        public double Money { get; set; }
        /// <summary>
        /// 请假与加班详细
        /// </summary>
        public IPagedList<Outsourcing.EF.LeaveDetail> LeaveDetail { get; set; }
    }
}