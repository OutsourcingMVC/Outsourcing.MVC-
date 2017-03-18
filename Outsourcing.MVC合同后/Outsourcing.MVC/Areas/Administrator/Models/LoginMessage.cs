using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outsourcing.MVC.Areas.Administrator.Models
{
    /// <summary>
    /// 定义登陆错误消息
    /// </summary>
    public class LoginMessage
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 角色集合
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 错误数量
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }
    }
}