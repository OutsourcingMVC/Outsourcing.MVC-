using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 财务管理数据层
    /// </summary>
    public class FinancialDAL : BaseDAL<FinancialManagement>
    {
        /// <summary>
        /// 根据where条件获取列表的总条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public string GetRecordCount(string where)
        {

            string count;
            using (var ct = new DB())
            {
                DbSet<FinancialManagement> set = ct.Set<FinancialManagement>();
                string sql = "select * from FinancialManagement where " + where + "";
                List<FinancialManagement> list = set.SqlQuery(sql).ToList();
                count = list.Count.ToString();
                return count;
            }

        }

        /// <summary>
        /// 根据where条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<FinancialManagement> GetModelList(string code)
        {
            List<FinancialManagement> financials = null; 
            using (var ct = new DB())
            {
                DbSet<FinancialManagement> dbSet = ct.Set<FinancialManagement>();
                string sql = "select * from FinancialManagement";
                if (!string.IsNullOrEmpty(code))
                {
                    sql += string.Format(" where code like '%{0}%'", code);
                }
                financials = dbSet.SqlQuery(sql).ToList();
                return financials;
            }

        }

        /// <summary>
        /// 通过设置查询条件查询结果
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<FinancialManagement> QueryModelList(string where)
        {
            List<FinancialManagement> financials = null;
            using (var ct = new DB())
            {
                DbSet<FinancialManagement> dbSet = ct.Set<FinancialManagement>();
                string sql = "select * from FinancialManagement";
                if (!string.IsNullOrEmpty(where))
                {
                    //sql += string.Format(" where code like '%{0}%'", code);
                    sql += where ;
                }
                financials = dbSet.SqlQuery(sql).ToList();
                return financials;
            }

        }

        /// <summary>
        /// 根据条件获取一个实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public FinancialManagement GetFinancialByWhere(string id)
        {
            using (var ct = new DB())
            {
                var financial = ct.FinancialManagement.Where(c => c.ID == id).FirstOrDefault();

                return financial;
            }
        }
    }
}
