using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 外包公司数据层
    /// </summary>
    public partial class OutsourcingCompanyDAL : BaseDAL<OutsourcingCompany>
    {
        /// <summary>
        /// 根据where条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<OutsourcingCompany> GetOutsourcingCompanyList(string companyusername)
        {
            List<OutsourcingCompany> outsourcingcompanys = new List<OutsourcingCompany>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(companyusername))
                {
                    outsourcingcompanys = ct.OutsourcingCompany.Where(c => c.CompnayName.IndexOf(companyusername) != -1
                ).ToList();
                }
                else {
                    outsourcingcompanys = ct.OutsourcingCompany.ToList();
                }
                return outsourcingcompanys;
            }

        }
        /// <summary>
        /// 根据where条件获取精确列表
        /// 2016.8.26 Limuter
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<OutsourcingCompany> GetCountlList(string strWhere)
        {
            List<OutsourcingCompany> outCompany = new List<OutsourcingCompany>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    string sql = "select * from OutsourcingCompany where CompanyUserName='" + strWhere + "'";
                    outCompany = ct.OutsourcingCompany.SqlQuery(sql).ToList();
                }
                return outCompany;
            }
        }
        /// <summary>
        /// 根据id条件获OutsourcingCompany实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public OutsourcingCompany GetOutsourcingCompany(int OutsourcingCompanyid)
        {
            using (var ct = new DB())
            {
                var outsourcingcompany = ct.OutsourcingCompany.Where(c => c.CompnayId == OutsourcingCompanyid).FirstOrDefault();
                return outsourcingcompany;
            }
        }

        /// <summary>
        /// 根据登录名条件获OutsourcingCompany实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public OutsourcingCompany GetOutsourcingCompany(string CompanyUserName)
        {
            using (var ct = new DB())
            {
                var outsourcingcompany = ct.OutsourcingCompany.FirstOrDefault(m=>m.CompanyUserName== CompanyUserName);
                return outsourcingcompany;
            }
        }


        public List<OutsourcingCompany> GetlList(int id)
        {
            List<OutsourcingCompany> outCompany = new List<OutsourcingCompany>();
            using (var ct = new DB())
            {
                string sql = "select * from OutsourcingCompany where CompnayId='" + id + "'";
                outCompany = ct.OutsourcingCompany.SqlQuery(sql).ToList();
                return outCompany;
            }
        }
        //根据ID获取外包表数据
        public List<OutsourcingCompany> GetList(int id)
        {
            List<OutsourcingCompany> outCompany = new List<OutsourcingCompany>();
            using (var ct = new DB())
            {
                string sql = "select * from OutsourcingCompany where CompnayId='" + id + "'";
                outCompany = ct.OutsourcingCompany.SqlQuery(sql).ToList<OutsourcingCompany>();
                return outCompany;
            }
        }

        //根据ID获取外包表数据
        public string GetListJSON(int id)
        {
            string resStr = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //List<OutsourcingCompany> outCompany = new List<OutsourcingCompany>();
            using (var ct = new DB())
            {
                var result = from p in ct.OutsourcingCompany
                             where p.CompnayId == id
                             select new
                             {
                                 CompnayName = p.CompnayName,
                                 EnglishName = p.EnglishName,
                                 LegalRepresentative = p.LegalRepresentative,
                                 LegalTelephone = p.LegalTelephone,
                                 UnitResponsible = p.UnitResponsible,
                                 ResponsibleTelephone = p.ResponsibleTelephone,
                                 Address = p.Address,
                                 BusinessScope = p.BusinessScope,
                                 EnterpriseNum2 = p.EnterpriseNum2,
                                 RegistrationAuthority = p.RegistrationAuthority,
                                 Nature = p.Nature
                             };
                //string sql = "select * from OutsourcingCompany where CompnayId='" + id + "'";
                //outCompany = ct.OutsourcingCompany.SqlQuery(sql).ToList<OutsourcingCompany>();
                //return outCompany;
                resStr = jss.Serialize(result);
            }
            return resStr;
        }
    }
}
