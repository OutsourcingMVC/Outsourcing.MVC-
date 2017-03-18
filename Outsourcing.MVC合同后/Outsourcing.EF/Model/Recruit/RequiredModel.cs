using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.Model.Recruit
{
    public class RequiredModel
    {
        public int RequirementId { get; set; }
        public string ProjectName { get; set; }
        public string ArrivalTime { get; set; }
        public string JobName { get; set; }
        public int CompnayId { get; set; }
        public string CompanyName { get; set; }
        public int? Salary { get; set; }
        public string SalaryName { get; set; }
        public string PublishTime { get; set; }
        public string ProjectAddress { get; set; }
        public int? Property { get; set; }
        public string PropertyName { get; set; }
        public int? Experience { get; set; }
        public string ExperienceName { get; set; }
        public int? Education { get; set; }
        public string EducationName { get; set; }

        public int? RecNumber { get; set; }
        public string JobDescription { get; set; }
        public string ComProfile { get; set; }
        public int? JobCategory { get; set; }
        public string JobCateGoryName { get; set; }
        public string ComAddress { get; set; }
        public int? TecLanguage { get; set; }
        public string TecLanguageName { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
