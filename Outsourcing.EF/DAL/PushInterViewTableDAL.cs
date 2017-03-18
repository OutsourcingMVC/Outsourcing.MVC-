using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{   
    /// <summary>
    /// 推送中心数据层
    /// </summary>
    public class PushInterViewTableDAL:BaseDAL<PushInterViewTable>
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
                DbSet<PushInterViewTable> set = ct.Set<PushInterViewTable>();
                string sql = "select * from PushInterViewTable where " + where + "";
                List<PushInterViewTable> list = set.SqlQuery(sql).ToList();
                count = list.Count.ToString();
                return count;
            }

        }

        /// <summary>
        /// 根据where条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<PushInterViewTable> GetModelList(string code)
        {
            List<PushInterViewTable> financials = null;
            using (var ct = new DB())
            {
                DbSet<PushInterViewTable> dbSet = ct.Set<PushInterViewTable>();
                string sql = "select * from PushInterViewTable";
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
        public List<PushInterViewTable> QueryModelList(string where)
        {
            List<PushInterViewTable> financials = null;
            using (var ct = new DB())
            {
                DbSet<PushInterViewTable> dbSet = ct.Set<PushInterViewTable>();
                string sql = "select * from PushInterViewTable";
                if (!string.IsNullOrEmpty(where))
                {
                    sql += string.Format(" where {0}", where);
                    //sql += where;
                }
                financials = dbSet.SqlQuery(sql).ToList();
                return financials;
            }

        }

        /// <summary>
        /// 通过条件获取对象
        /// </summary>
        /// <param name="rid">需求标识</param>
        /// <param name="pid">人员标识</param>
        /// <returns></returns>
        public PushInterViewTable GetPushInterViewByWhere(int rid,int pid)
        {
            using (var ct = new DB())
            {
                var financial = ct.PushInterViewTable.Where(c => c.RequirementId == rid && c.PersonalInfoId==pid).FirstOrDefault();

                return financial;
            }
        }

        /// <summary>
        /// 通过条件获取对象
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="rid">需求标识</param>
        /// <param name="pid">人员标识</param>
        /// <returns></returns>
        public PushInterViewTable GetPushInterViewByWhere(string id)
        {
            using (var ct = new DB())
            {
                var financial = ct.PushInterViewTable.Where(c => c.ID==id).FirstOrDefault();

                return financial;
            }
        }
    }
}
