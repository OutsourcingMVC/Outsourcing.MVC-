using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    public partial class AccountDAL : BaseDAL<Account>
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
                DbSet<Account> set = ct.Set<Account>();
                string sql = "select * from Account where " + where + "";
                List<Account> list = set.SqlQuery(sql).ToList();
                count = list.Count.ToString();
                return count;
            }

        }

        /// <summary>
        /// 根据where条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Account> GetModelList(string username)
        {
            List<Account> accounts = new List<Account>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(username))
                {
                    accounts = ct.Account.Where(c => c.AccountName.IndexOf(username) != -1
                ).Include(a => a.Role).ToList();
                }
                else {
                    accounts = ct.Account.Include(a => a.Role).ToList();
                }
                ////DbSet<Account> set = ct.Set<Account>();
                //string sql = "select * from Account where " + where + "";
                // List<Account> list = ct.Database.SqlQuery<Account>(sql).ToList();
                //List<Account> list = set.SqlQuery(sql).ToList();
                return accounts;
            }

        }
        /// <summary>
        /// 根据条件获取一个实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Account GetAccountByWhere(string username, string password)
        {
            using (var ct = new DB())
            {
                var account = ct.Account.Where(c => c.AccountName == username && c.Password == password
                ).FirstOrDefault();

                return account;
            }
        }
        /// <summary>
        /// 根据id条件获取用户对应的角色
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Account GetAccoutRole(int userid)
        {


            using (var ct = new DB())
            {
                var account = ct.Account.Where(c => c.AccountID == userid).Include(a => a.Role).FirstOrDefault();
                ////DbSet<Account> set = ct.Set<Account>();
                //string sql = "select * from Account where " + where + "";
                // List<Account> list = ct.Database.SqlQuery<Account>(sql).ToList();
                //List<Account> list = set.SqlQuery(sql).ToList();
                return account;
            }
        }
    }
}
