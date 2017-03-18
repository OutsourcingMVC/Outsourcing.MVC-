using Outsourcing.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.Ectocyst.Models
{
    public class MemberCenterViewModel
    {
        public List<OutsourcingCompany> outConmpany { get; set; }
        public List<CustomerCompnay> custConmpany { get; set; }
    }
}