using Outsourcing.EF.Model.Recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.Model.Index
{
    public class IndexShow
    {
        public List<RequiredModel> requirementList { get; set; }

        public List<PersonalInfoModel> persionalInfoList { get; set; }

        public List<PtrcInfoModel> ptrcinfoList { get; set; }

        public List<WaitEmployer> waitEmployerList { get; set; }
        //public List<NewsInforModel> NewsInforModel { get; set; }
    }

    public class PersonalInfoModel:PersonalInfo
    {
        public string CompnayName { get; set; }
    }

    public class PtrcInfoModel
    {
        public string Name { get; set; }
    }

    public class WaitEmployer
    {
        public string Name { get; set; }
        public double Money { get; set; }
    }
}
