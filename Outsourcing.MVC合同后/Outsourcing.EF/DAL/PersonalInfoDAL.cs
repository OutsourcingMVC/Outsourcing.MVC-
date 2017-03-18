using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    public partial class PersonalInfoDAL : BaseDAL<PersonalInfo>
    { /// <summary>
      /// 根据where条件获取列表
      /// </summary>
      /// <param name="where"></param>
      /// <returns></returns>
        public List<PersonalInfo> GetPersonalInfoList(string personname)
        {
            List<PersonalInfo> personalinfos = new List<PersonalInfo>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(personname))
                {
                    personalinfos = ct.PersonalInfo.Where(c => c.PersonName.IndexOf(personname) != -1
                ).ToList();
                }
                else
                {
                    personalinfos = ct.PersonalInfo.ToList();
                }
                return personalinfos;
            }

        }
        //搜索功能
        //public List<PersonalInfo> Search(string person)
        //{
        //    using (var ct = new DB())
        //    {
        //        if (!string.IsNullOrEmpty(Personnames))
        //        {
        //            PersonalInfos =ct.PersonalInfo.Where(c=>c.PersonName).IndexOf(personname)!= -1).tolist;
        //        }
        //    }
        /// <summary>
        /// 根据id条件获PersonalInfo实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public PersonalInfo GetPersonalInfo(int personalinfoid)
        {
            using (var ct = new DB())
            {
                var personalinfo = ct.PersonalInfo.Where(c => c.PersonalInfoId == personalinfoid).FirstOrDefault();
                return personalinfo;
            }
        }

        /// <summary>
        /// 根据id条件获未入职PersonalInfo实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public PersonalInfo GetNoEntryPersonalInfo(int personalinfoid)
        {
            using (var ct = new DB())
            {
                var personalinfo = ct.PersonalInfo.Where(c => c.PersonalInfoId == personalinfoid && c.PeopleStatue != 0).FirstOrDefault();
                return personalinfo;
            }
        }

        //根据ID和姓名获取外包公司人才列表
        public List<PersonalInfo> GetPersonList(string strWhere)
        {
            List<PersonalInfo> person = new List<PersonalInfo>();
            using (var ct = new DB())
            {
                //string sql = "select * from PersonalInfo where OutsourcingCompanyId='" + id + "' and IsDelete=1";
                string sql = "select * from PersonalInfo where ";
                if (!string.IsNullOrEmpty(strWhere))
                {
                    sql += strWhere;
                }
                person = ct.PersonalInfo.SqlQuery(sql).ToList();
                return person;
            }
        }
        /// <summary>
        /// 根据id条件获PersonalInfo实体
        /// </summary>
        public List<PersonalInfo> GetPersonalInfoList(int personalinfoid)
        {
            List<PersonalInfo> person = new List<PersonalInfo>();
            using (var ct = new DB())
            {
                //string sql = "select * from PersonalInfo where OutsourcingCompanyId='" + id + "' and IsDelete=1";
                string sql = "select * from PersonalInfo where PersonalInfoId='" + personalinfoid + "'";
                person = ct.PersonalInfo.SqlQuery(sql).ToList();
                return person;
            }
        }
        ////加载人才列表
        //public List<PersonalInfo> GetModelList(string strWhere)
        //{
        //    //StringBuilder strSql = new StringBuilder();
        //    //strSql.Append("select StaffMemberID,StaffName,Email,Registry,PoliticalLandscape,SerialNumber,StaffMemberType,Sex,Age,Nation,AccountID,DepartmentID,BirthDate,LanguageBranch,LanguageBranchRead,SpokenLanguage,BloodType,HighestEducationQualification,Professionally,WorkYear,RegistryHabitude,SocialInsuranceInBJ,Telephone,MarriageStatus,Children,ChildrenAge,ReachDate,Occupancy,EmergencyContact,EmergencyTel,RelationsWithHimSelf,CompetitionProtocol,CompetitionProtocolName,HopeWage,LowestHopeWage,OtherWelfare,ComprehensiveMedical,ComprehensiveMedicalDescription,InfectDisease,InfectDiseaseDescription,OtherDisease,OtherDiseaseDescription,IdentityCard,StaffState,Contract,ContractWage1,FiveSocialInsuranceOneHousingFund,BankCardName,CMBCardNumber,IsJob,ContactFile,DepartureTime,SecrecyAgreement");
        //    //strSql.Append(" FROM StaffMember ");
        //    //if (strWhere.Trim() != "")
        //    //{
        //    //    strSql.Append(" where " + strWhere);
        //    //}
        //    //return DbHelperSQL.Query(strSql.ToString());
        //}
    }
}
