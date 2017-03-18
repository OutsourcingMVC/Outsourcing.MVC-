using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outsourcing.MVC.Areas.Administrator.Models
{
    /// <summary>
    /// 自定义model，字段要和ztree demo的字段一样
    /// </summary>
    public class JsonMenuModel
    {
        public string id;
        public string pId;
        public string name;
        public bool open;
        public string url;
        public string icon;
        public string title;
        public bool ischecked;
    }
}