using Outsourcing.EF;
using Outsourcing.EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region  DAL操作对象
        public AttachmentDAL AttachmentDAL = new AttachmentDAL();
        #endregion
        public ActionResult Index()
        {

            //Attachment a = new Attachment
            //{
            //    AttachmentId = Guid.NewGuid().ToString(),
            //    AttachmentName = "111"
            //};

            //AttachmentDAL.Insert(a);
            //var dd = new Attachment();
            using (var ct = new DB())
            {
                ct.Attachment.ToList();
            }
            //AttachmentDAL.Update(dd);
            return View();
        }
        
    }
}