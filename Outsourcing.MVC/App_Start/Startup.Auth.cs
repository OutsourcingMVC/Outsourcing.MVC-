using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YJY.EMS.Web.Common
{
    /// <summary>
    /// 表示需要用户登录才可以使用的特性
    /// 如果不需要处理用户登录，则请指定AllowAnonymousAttribute属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public AuthorizationAttribute()
        {
            String authUrl = string.Empty; //System.Configuration.ConfigurationManager.AppSettings["AuthUrl"];
            String saveKey = string.Empty;//System.Configuration.ConfigurationManager.AppSettings["AuthSaveKey"];
            String saveType = string.Empty; //System.Configuration.ConfigurationManager.AppSettings["AuthSaveType"];
            if (String.IsNullOrEmpty(authUrl))
            {
                this._AuthUrl = "~/Login.html"; //"/waste/user/login";
            }
            else
            {
                this._AuthUrl = authUrl;
            }
            if (String.IsNullOrEmpty(saveKey))
            {
                this._AuthSaveKey = "LoginedUser";
            }
            else
            {
                this._AuthSaveKey = saveKey;
            }
            if (String.IsNullOrEmpty(saveType))
            {
                this._AuthSaveType = "Session";
            }
            else
            {
                this._AuthSaveType = saveType;
            }
        }

        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="authUrl">表示没有登录跳转的登录地址</param>
        public AuthorizationAttribute(String authUrl)
            : this()
        {
            this._AuthUrl = authUrl;
        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="authUrl">表示没有登录跳转的登录地址</param>
        /// <param name="saveKey">表示登录用来保存登陆信息的键名</param>
        public AuthorizationAttribute(String authUrl, String saveKey)
            : this(authUrl)
        {
            this.AuthSaveKey = saveKey;
            this.AuthSaveType = "Session";
        }

        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="authUrl">表示没有登录跳转的登录地址</param>
        /// <param name="saveKey">表示登录用来保存登陆信息的键名</param>
        /// <param name="saveType">表示登录用来保存登陆信息的方式</param>
        public AuthorizationAttribute(String authUrl, String saveKey, String saveType)
            : this(authUrl, saveKey)
        {
            this._AuthSaveType = saveType;
        }

        /// <summary>
        /// 获取或者设置一个值，该值表示登录地址
        /// 如果web.config中末定义AuthUrl的值，则默认为：/waste/user/login
        /// </summary>
        private String _AuthUrl = String.Empty;
        public String AuthUrl
        {
            get { return _AuthUrl.Trim(); }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用于验证用户登录信息的登录地址不能为空！");
                }
                else
                {
                    _AuthUrl = value.Trim();
                }
            }
        }

        /// <summary>
        /// 获取或者设置一个值，该值表示登录用来保存登陆信息的键名
        /// 如果web.config中末定义AuthSaveKey的值，则默认为LoginedUser
        /// </summary>
        private String _AuthSaveKey = String.Empty;
        public String AuthSaveKey
        {
            get { return _AuthSaveKey.Trim(); }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用于保存登陆信息的键名不能为空！");
                }
                else
                {
                    this._AuthSaveKey = value.Trim();
                }
            }
        }

        /// <summary>
        /// 获取或者设置一个值，该值用来保存登录信息的方式
        /// 如果web.config中末定义AuthSaveType的值，则默认为Session保存
        /// </summary>
        private String _AuthSaveType = String.Empty;
        public String AuthSaveType
        {
            get { return _AuthSaveType.Trim().ToUpper(); }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用于保存登陆信息的方式不能为空，只能为【Cookie】或者【Session】！");
                }
                else
                {
                    _AuthSaveType = value.Trim();
                }
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new Exception("此特性只适合于Web应用程序使用！");
            }
            else
            {
                switch (AuthSaveType)
                {
                    case "SESSION":
                        if (filterContext.HttpContext.Session == null)
                        {
                            throw new Exception("服务器Session不可用！");
                        }
                        else if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                        {
                            if (filterContext.HttpContext.Session[_AuthSaveKey] == null)
                            {
                                //修复用户Session超时后，页面无限闪烁，死循环的bug  by yanghb 2015.9.10
                                //filterContext.Result = new RedirectResult(_AuthUrl);
                                //filterContext.RequestContext.HttpContext.Server.Transfer(_AuthUrl);
                                filterContext.HttpContext.Response.Redirect(_AuthUrl);
                            }

                        }
                        break;
                    case "COOKIE":
                        if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                        {
                            if (filterContext.HttpContext.Request.Cookies[_AuthSaveKey] == null)
                            {
                                //filterContext.Result = new RedirectResult(_AuthUrl);
                                //filterContext.RequestContext.HttpContext.Server.Transfer(_AuthUrl);
                                filterContext.HttpContext.Response.Redirect(_AuthUrl);
                            }
                        }
                        break;
                    default:
                        throw new ArgumentNullException("用于保存登陆信息的方式不能为空，只能为【Cookie】或者【Session】！");
                }
            }
        }
    }
}