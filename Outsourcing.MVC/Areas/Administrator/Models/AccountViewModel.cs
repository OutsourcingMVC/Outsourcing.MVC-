using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Outsourcing.MVC.Areas.Administrator.Models
{
    /// <summary>
    /// 定义用户模块需要的viewModel
    /// </summary>
    public class AccountViewModel
    {
        /// <summary>
        /// 系统用户集合
        /// </summary>
        public IPagedList<EF.Account> Accounts { get; set; }

        /// <summary>
        /// 系统用户关联的角色集合
        /// </summary>
        public IEnumerable<EF.Role> Roles { get; set; }

        ///// <summary>
        ///// 系统用户与角色的关系集合
        ///// </summary>
        //public IEnumerable<AccountRole_R> AccountRole_Rs { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public EF.Account Account { get; set; }

        ///// <summary>
        ///// 系统用户关联的角色
        ///// </summary>
        //public Role Role { get; set; }

        ///// <summary>
        ///// 系统用户与角色的关系
        ///// </summary>
        //public AccountRole_R AccountRole_R { get; set; }

    }
}