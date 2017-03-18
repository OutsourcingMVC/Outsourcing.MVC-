using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outsourcing.MVC.Areas.Professional.Models
{
    public class ProfessionalsExtenViewModel:EF.PersonalInfo
    {
        /// <summary>
        /// 所属公司
        /// </summary>
        public string OutsourcingCompany { get; set; }
    }
}