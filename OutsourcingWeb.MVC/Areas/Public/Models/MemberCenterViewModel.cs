using Outsourcing.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.Public.Models
{
    public class MemberCenterViewModel
    {
        public List<OutsourcingCompany> outConmpany { get; set; }
        public List<CustomerCompnay> custConmpany { get; set; }
    }
    public class CompanyData
    {
        public string CompnayName { get; set; }
        public string EnglishName { get; set; }
        public string LegalRepresentative { get; set; }
        public string LegalTelephone { get; set; }
        public string UnitResponsible { get; set; }
        public string ResponsibleTelephone { get; set; }
        public string Address { get; set; }
        public string BusinessScope { get; set; }
        public string EnterpriseNum2 { get; set; }
        public string RegistrationAuthority { get; set; }
        public string Nature { get; set; }
    }
}