using Outsourcing.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace OutsourcingWeb.MVC.Areas.Ectocyst.Models
{
    public class PersonalInfoViewModel
    {
        //public List<PersonalInfo> personalInfo { get; set; }
        public IPagedList<PersonalInfo> PersonalInfo { get; set; }
        public List<PersonalInfo> personalInfo { get; set; }
    }
}