using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 合同管理数据层
    /// </summary>
    public class ContractManagementDAL: BaseDAL<ContractManagement>
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
                DbSet<ContractManagement> set = ct.Set<ContractManagement>();
                string sql = "select * from ContractManagement where " + where + "";
                List<ContractManagement> list = set.SqlQuery(sql).ToList();
                count = list.Count.ToString();
                return count;
            }
        }

        /// <summary>
        /// 根据where条件获取合同管理列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<ContractManagement> GetModelList(string where)
        {
            List<ContractManagement> contracts = new List<ContractManagement>();
            using (var ct = new DB())
            {
                DbSet<ContractManagement> set = ct.Set<ContractManagement>();
                string sql = "select * from ContractManagement ";
                if (!string.IsNullOrEmpty(where))
                {
                    sql += " where " + where;
                }
                contracts = set.SqlQuery(sql).ToList();
                return contracts;
            }
        }

        /// <summary>
        /// 根据条件获取一个实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public ContractManagement GetContractByWhere(string id)
        {
            using (var ct = new DB())
            {
                var contract = ct.ContractManagement.Where(c => c.ID == id ).FirstOrDefault();

                return contract;
            }
        }

        /// <summary>
        /// 根据客户标识获取一个实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public ContractManagement GetContractByPartner(int id)
        {
            using (var ct = new DB())
            {
                var contract = ct.ContractManagement.Where(c => c.RegisterId == id).FirstOrDefault();

                return contract;
            }
        }

    }
}
