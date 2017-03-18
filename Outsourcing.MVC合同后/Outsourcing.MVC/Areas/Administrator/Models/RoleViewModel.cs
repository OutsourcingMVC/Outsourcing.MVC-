using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;


namespace Outsourcing.MVC.Areas.Administrator.Models
{
    /// <summary>
    /// 定义角色模块需要的viewModel
    /// </summary>
    public class RoleViewModel
    {
        /// <summary>
        /// 系统角色集合
        /// </summary>
        public IPagedList<EF.Role> Roles { get; set; }

        /// <summary>
        /// 系统角色关联的菜单集合
        /// </summary>
        public IEnumerable<EF.Menu> Menus { get; set; }

        ///// <summary>
        ///// 系统角色与菜单的关系集合
        ///// </summary>
        //public IEnumerable<RoleMenu_R> RoleMenu_Rs { get; set; }

        /// <summary>
        /// 系统角色
        /// </summary>
        public EF.Role Role { get; set; }
    }
}