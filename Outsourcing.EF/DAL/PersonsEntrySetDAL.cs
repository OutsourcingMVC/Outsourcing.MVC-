using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
  
    public class PersonsEntrySetDAL:BaseDAL<PersonsEntrySet>
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
                DbSet<PersonsEntrySet> set = ct.Set<PersonsEntrySet>();
                string sql = "select * from PersonsEntrySet where " + where + "";
                List<PersonsEntrySet> list = set.SqlQuery(sql).ToList();
                count = list.Count.ToString();
                return count;
            }

        }

        /// <summary>
        /// 通过设置查询条件查询结果(无须添加关键字where)
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<PersonsEntrySet> QueryModelList(string where)
        {
            List<PersonsEntrySet> financials = null;
            using (var ct = new DB())
            {
                DbSet<PersonsEntrySet> dbSet = ct.Set<PersonsEntrySet>();
                string sql = "select * from PersonsEntrySet";
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
        /// 获取单个对象
        /// </summary>
        /// <param name="personID">人员标识</param>
        /// <param name="cID">客户公司标识</param>
        /// <param name="oID">外包公司标识</param>
        /// <returns></returns>
        public PersonsEntrySet GetModelBy(int personID, int cID, int oID)
        {
            PersonsEntrySet personEntry = null;
            using (var ct = new DB())
            {
                personEntry = ct.PersonsEntrySet.FirstOrDefault(m => m.PersonalInfoId == personID &&
                                             m.CustomerCompnayCompnayId == cID &&
                                             m.OutsourcingCompanyCompnayId == oID);
            }
            return personEntry;
        }

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public PersonsEntrySet GetModelBy(int ID)
        {
            PersonsEntrySet personEntry = null;
            using (var ct = new DB())
            {
                personEntry = ct.PersonsEntrySet.FirstOrDefault(m => m.Id==ID);
            }
            return personEntry;
        }

        /// <summary>
        /// 通过类型获取相关数据 
        /// </summary>
        /// <param name="cID">客户公司标识</param>
        /// <param name="type">默认为2客户公司</param>
        /// <returns></returns>
        public List<PersonsEntrySet> GetModelList(int cID,int type=2)
        {
            List<PersonsEntrySet> list = new List<PersonsEntrySet>();
            using (var ct = new DB())
            {
                var result = ct.PersonsEntrySet
                              .Include(x=>x.CustomerCompnay)
                              .Include(x=>x.OutsourcingCompany)
                              .Include(x=>x.PersonalInfo)
                              .Where(m => m.CustomerCompnayCompnayId == cID)                              
                              .ToList<PersonsEntrySet>();

                list = result;                
            }
            return list;
        }
       

        /// <summary>
        /// 通过类型获取相关数据 
        /// </summary>
        /// <param name="personID">人员标识</param>
        /// <param name="oID">外包公司标识</param>
        /// <returns></returns>
        public List<PersonsEntrySet> GetModelList(int oID)
        {
            List<PersonsEntrySet> list = new List<PersonsEntrySet>();
            using (var ct = new DB())
            {
                var result= ct.PersonsEntrySet
                              .Include(x => x.CustomerCompnay)
                              .Include(x => x.OutsourcingCompany)
                              .Include(x => x.PersonalInfo)
                              .Where(m => m.OutsourcingCompanyCompnayId == oID)
                              .ToList<PersonsEntrySet>();
                list = result;
            }
            return list;
        }

        
        /// <summary>
        /// 根据用户类型返回入职人员所属的公司或合作公司的标识集合
        /// </summary>
        /// <param name="type">用户类型1：外包公司；2：客户公司</param>
        /// <returns></returns>
        public Dictionary<int, string> GetCompanyIDByUserType(string type)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            using (var ct = new DB())
            {
                //根据当前用户类型获取合作公司
                //1：获取当前用户所有的合作公司
                //2：获取当前用户所有的合作外包公司
                switch (type)
                {
                    case "1":
                        var expr1 = from p in ct.PersonsEntrySet
                                   group p by p.CustomerCompnayCompnayId into g
                                   select g;

                        foreach (var item in expr1)
                        {
                            Console.WriteLine(item.Key);

                            foreach (var p in item)
                            {
                                if (!list.ContainsKey(p.CustomerCompnayCompnayId))
                                {
                                    list.Add(p.CustomerCompnayCompnayId, p.CustomerCompnay.CompnayName);
                                }

                            }
                        }
                        break;
                    case "2":

                        var expr2 = from p in ct.PersonsEntrySet
                                   group p by p.OutsourcingCompanyCompnayId into g
                                   select g;

                        foreach (var item in expr2)
                        {
                            Console.WriteLine(item.Key);

                            foreach (var p in item)
                            {
                                if(!list.ContainsKey(p.OutsourcingCompanyCompnayId))
                                {
                                    list.Add(p.OutsourcingCompanyCompnayId, p.OutsourcingCompany.CompnayName);
                                }
                                
                            }
                        }
                        break;
                }
            }
            return list;
        }
    }
}
