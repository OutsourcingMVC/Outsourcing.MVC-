using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.Model.Recruit
{
    public class PersonalShowModel
    {
        public int PersonalInfoId { get; set; }
        public string OutsourcingCompanyId { get; set; }
        public string PersonName { get; set; }
        public string Sex { get; set; }
        public string Remark { get; set; }
        public string Resume { get; set; } 
        public string Educations { get; set;}
        public string Email { get; set; }
        public string Telphone { get; set; }
        public string Birthday { get; set; }
        public string Education { get; set; }
        public string GraduationSchool { get; set; }
        public string GraduationDate { get; set; }
        public string Publications { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string CompnayName { get; set; }
        public string EnglishName { get; set; }
        public string Honor { get; set; }
        public string InSchoolLife{ get; set; }
        public string Language { get; set; }
        public string ProjectPower { get; set; }
        public string Books { get; set; }
        public string WorkDay { get; set; }
        public string LegalRepresentative { get; set; }
        public string LegalTelephone { get; set; }
        public string UnitResponsible { get; set; }
        public string ResponsibleTelephone { get; set; }
        public string Address { get; set; }
        public string EnterpriseNum { get; set; }
        public string Descriptions { get; set; }
        public string Nature { get; set; }
        public string BusinessScope { get; set; }
    }
}
