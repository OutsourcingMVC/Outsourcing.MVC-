using Outsourcing.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.Customer.Models
{
    public class MemberCenterViewModel
    {
        public IPagedList<PersonalInfo> PersonalInfo { get; set; }
        public IPagedList<PushedHistory> PushedHistory { get; set; }
        public IPagedList<Requirement> Requirement { get; set; }
    }
}