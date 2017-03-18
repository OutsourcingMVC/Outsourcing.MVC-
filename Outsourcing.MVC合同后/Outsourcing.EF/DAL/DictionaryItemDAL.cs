
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 字典项数据层
    /// </summary>
    public class DictionaryItemDAL : BaseDAL<DictionaryItem>
    {
        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<DictionaryItem> GetDictionaryItemList(string DictionaryItemname)
        {
            List<DictionaryItem> DictionaryItems = new List<DictionaryItem>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(DictionaryItemname))
                {
                    int[] a = { 1, 2, 3 };
                    DictionaryItems = ct.DictionaryItem.Where(c => c.ItemName.IndexOf(DictionaryItemname) != -1
                ).ToList();
                }
                else {
                    DictionaryItems = ct.DictionaryItem.ToList();
                }
                return DictionaryItems;
            }

        }
        /// <summary>
        /// 根据id条件获DictionaryItem实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DictionaryItem GetDictionaryItem(int DictionaryItemid)
        {
            using (var ct = new DB())
            {
                var DictionaryItem = ct.DictionaryItem.Where(c => c.DictionaryItemID == DictionaryItemid).FirstOrDefault();
                return DictionaryItem;
            }
        }
        /// <summary>
        /// 根据where条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<DictionaryItem> GetDictionaryItemModelList(string where)
        {

            
            using (var ct = new DB())
            {
                DbSet<DictionaryItem> set = ct.Set<DictionaryItem>();
                string sql = "select * from DictionaryItem where " + where + "";
                List<DictionaryItem> list = set.SqlQuery(sql).ToList();
                return list;
            }

        }
    }
}
