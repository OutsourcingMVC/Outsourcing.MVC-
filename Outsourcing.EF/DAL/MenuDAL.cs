using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 菜单管理的数据层
    /// </summary>
    public class MenuDAL : BaseDAL<Menu>
    { /// <summary>
      /// 根据where条件获取列表
      /// </summary>
      /// <param name="where"></param>
      /// <returns></returns>
        public List<Menu> GetModelList(string where)
        {

            using (var ct = new DB())
            {
                DbSet<Menu> set = ct.Set<Menu>();
                string sql = "select * from Menu where " + where + "";
                List<Menu> list = set.SqlQuery(sql).ToList();
                return list;
            }
        }
        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Menu> GetMenuList(string Menuname)
        {
            List<Menu> Menus = new List<Menu>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(Menuname))
                {
                    int[] a = { 1, 2, 3 };
                    Menus = ct.Menu.Where(c => c.MenuName.IndexOf(Menuname) != -1
                ).ToList();
                }
                else {
                    Menus = ct.Menu.ToList();
                }
                return Menus;
            }

        }
        /// <summary>
        /// 根据id条件获Menu实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Menu GetMenu(int Menuid)
        {
            using (var ct = new DB())
            {
                var menu = ct.Menu.Where(c => c.MenuID == Menuid).FirstOrDefault();
                return menu;
            }
        }

    }
}
