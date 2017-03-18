using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Common
{
    /// <summary>
    /// 所有需要进行登录控制的控制器基类（此类未启动，用户Session管理工作暂由过滤器承担） by 杨虎斌 2015/07/24
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前登录的用户属性
        /// </summary>
        public Outsourcing.EF.Account CurrentAccountInfo
        {
            get
            {
                if (Session["LoginedUser"] != null)
                    return Session["LoginedUser"] as Outsourcing.EF.Account;
                else
                    return new Outsourcing.EF.Account();
            }

        }

        /// <summary>
        /// 重新基类在Action执行之前的事情
        /// </summary>
        /// <param name="filterContext">重写方法的参数</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //得到用户登录的信息
            //CurrentAccountInfo = Session["LoginedUser"] as Model.Administrator.Account;

            //判断用户是否为空
            //if (CurrentAccountInfo == null)
            //{
            //    Response.Redirect("/Administrator/Account/Login");
            //}
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //记录错误日志
            //WHC.Framework.Commons.LogTextHelper.Error(filterContext.Exception);

            // 当自定义显示错误 mode = On，显示友好错误页面
            //if (filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    filterContext.ExceptionHandled = true;
            //    this.View("Error").ExecuteResult(this.ControllerContext);
            //}
        }

    }
}