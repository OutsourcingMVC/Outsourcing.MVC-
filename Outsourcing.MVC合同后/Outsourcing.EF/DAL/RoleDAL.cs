using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 角色管理数据层
    /// </summary>
    public class RoleDAL : BaseDAL<Role>
    {/// <summary>
     /// 根据where条件获取列表的总条数
     /// </summary>
     /// <param name="where"></param>
     /// <returns></returns>
        public string GetRole(string where)
        {

            string count;
            using (var ct = new DB())
            {
                DbSet<Role> set = ct.Set<Role>();
                string sql = "select * from Role where " + where + "";
                List<Role> list = set.SqlQuery(sql).ToList();
                count = list.Count.ToString();
                return count;
            }

        }
        /// <summary>
        /// 根据where条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Role> GetModelList(string where)
        {

            using (var ct = new DB())
            {
                DbSet<Role> set = ct.Set<Role>();
                string sql = "select * from Role where " + where + "";
                List<Role> list = set.SqlQuery(sql).ToList();
                return list;
            }

        }
        /// <summary>
        /// 根据ID获取一个对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Role GetRoleByID(int roleid)
        {

            using (var ct = new DB())
            {
                var role=ct.Role.Where(x => x.RoleID == roleid).Include(a=>a.Account).Include(a => a.Menu).FirstOrDefault();
                return role;
            }

        }
        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Role> GetRoleList(string Rolename)
        {
            List<Role> roles = new List<Role>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(Rolename))
                {
                    roles = ct.Role.Where(c => c.RoleName.IndexOf(Rolename) != -1
                ).ToList();
                }
                else {
                    roles = ct.Role.ToList();
                }
                ////DbSet<Account> set = ct.Set<Account>();
                //string sql = "select * from Account where " + where + "";
                // List<Account> list = ct.Database.SqlQuery<Account>(sql).ToList();
                //List<Account> list = set.SqlQuery(sql).ToList();
                return roles;
            }

        }
    }
    }
