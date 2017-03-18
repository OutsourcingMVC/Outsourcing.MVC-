using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 外包公司数据层
    /// </summary>
    public partial class CustomerOutsourcDAL : BaseDAL<CustomerOutsourc>
    {
        /// <summary>
        /// 根据where条件获取精确列表
        /// 2016.8.24 Limuter
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<CustomerOutsourc> GetCountlList(string strWhere)
        {
            List<CustomerOutsourc> customerOutsourc = new List<CustomerOutsourc>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    string sql = "select * from CustomerOutsourc where CompanyUserName='" + strWhere + "'";
                    customerOutsourc = ct.CustomerOutsourc.SqlQuery(sql).ToList();
                }
                return customerOutsourc;
            }
        }
        public List<CustomerOutsourc> GetlList(string strWhere)
        {
            List<CustomerOutsourc> customerOutsourc = new List<CustomerOutsourc>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    string sql = "select * from CustomerOutsourc where "+strWhere+"";
                    customerOutsourc = ct.CustomerOutsourc.SqlQuery(sql).ToList();
                }
                return customerOutsourc;
            }
        }

        /// <summary>
        /// 通过标识查询结果
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CustomerOutsourc GetModelByID(int ID)
        {
            using (var ct = new DB())
            {
                return ct.CustomerOutsourc.FirstOrDefault(m => m.CustomerOutID == ID);
            }
        }
    }
}
