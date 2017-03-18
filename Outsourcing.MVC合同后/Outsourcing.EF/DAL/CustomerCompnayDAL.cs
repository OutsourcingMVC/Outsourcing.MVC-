using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 客户公司数据层
    /// </summary>
    public partial class CustomerCompnayDAL : BaseDAL<CustomerCompnay>
    {/// <summary>
     /// 根据where条件获取列表
     /// </summary>
     /// <param name="where"></param>
     /// <returns></returns>
        public List<CustomerCompnay> GetCustomerCompnayList(string companyname)
        {
            List<CustomerCompnay> customercompnays = new List<CustomerCompnay>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(companyname))
                {
                    customercompnays = ct.CustomerCompnay.Where(c => c.CompnayName.IndexOf(companyname) != -1
                ).ToList();
                }
                else {
                    customercompnays = ct.CustomerCompnay.ToList();
                }
                return customercompnays;
            }

        }
        /// <summary>
        /// 根据where条件获取精确列表
        /// 2016.8.26 Limuter
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<CustomerCompnay> GetCountlList(string strWhere)
        {
            List<CustomerCompnay> custCompany = new List<CustomerCompnay>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    string sql = "select * from CustomerCompnay where CompanyUserName='" + strWhere + "'";
                    custCompany = ct.CustomerCompnay.SqlQuery(sql).ToList();
                }
                return custCompany;
            }
        }
        //  /// <summary>
        //  /// 根据ID获取公司名称
        //  /// </summary>
        //  /// <param name="CompnayId"></param>
        //  /// <returns></returns>
        //public CustomerCompnay getCustomercompany (int CompnayId)
        //{
        //    string sql="select * from "
        //}



        /// <summary>
        /// 根据id条件获CustomerCompnay实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public CustomerCompnay GetCustomerCompnay(int customercompnayid)
        {
            using (var ct = new DB())
            {
                var customercompnay = ct.CustomerCompnay.Where(c => c.CompnayId == customercompnayid).FirstOrDefault();
                return customercompnay;
            }
        }

        /// <summary>
        /// //根据ID获取客户表数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List集合</returns>
        public List<CustomerCompnay> GetList(int id)
        {
            List<CustomerCompnay> custCompany = new List<CustomerCompnay>();
            using (var ct = new DB())
            {
                string sql = "select * from CustomerCompnay where CompnayId='" + id + "'";
                custCompany = ct.CustomerCompnay.SqlQuery(sql).ToList<CustomerCompnay>();
                return custCompany;
            }
        }

        /// <summary>
        /// //根据ID获取客户表数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json</returns>
        public string GetListJSON(int id)
        {
            string resStr = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //List<CustomerCompnay> custCompany = new List<CustomerCompnay>();
            using (var ct = new DB())
            {
                var result = from p in ct.CustomerCompnay
                             where p.CompnayId == id
                             select new {
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

                //string sql = "select * from CustomerCompnay where CompnayId='" + id + "'";
                //custCompany = ct.CustomerCompnay.SqlQuery(sql).ToList<CustomerCompnay>();
                resStr = jss.Serialize(result);                
            }
            return resStr;
        }

        /// <summary>
        /// 根据登录名获取实例
        /// </summary>
        /// <param name="CompanyUserName"></param>
        /// <returns></returns>
        public CustomerCompnay GetModel(string CompanyUserName)
        {
            using (var ct = new DB())
            {
                return ct.CustomerCompnay.FirstOrDefault(m => m.CompanyUserName == CompanyUserName);
            }
        }
    }
}
