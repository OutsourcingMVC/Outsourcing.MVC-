using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.Public.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Public.Controllers
{
    public class MessageListController : Controller
    {
        #region  DAL操作对象
        RequirementDAL requirementDAL = new RequirementDAL();
        #endregion
        // GET: Public/MessageList
        public ActionResult MessageList(int type, int page = 1)
        {
            int compnayId = Convert.ToInt32(Session["ID"]);
            if (compnayId!=0)
            { ViewBag.LoginID = compnayId; }
            else { ViewBag.LoginID = ""; }
            Models.MessageListViewModel viewModel = new MessageListViewModel();
            IEnumerable<Requirement> requirement = null;
            requirement = requirementDAL.GetPersonList(string.Format("b.JobCategory='{0}'  AND b.IsDelete = 1 ORDER BY b.PublishTime DESC", type)).AsQueryable();
            viewModel.Requirement = requirement.ToPagedList(page, 15);
            return View(viewModel);
        }
    }
}