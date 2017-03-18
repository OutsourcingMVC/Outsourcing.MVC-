using Outsourcing.EF.DAL;
using Outsourcing.MVC.Areas.Requirement.Models;
using Outsourcing.MVC.Areas.TalentPush.Models;
using Outsourcing.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.TalentPush.Controllers
{
    /// <summary>
    /// 人才推送
    /// </summary>
    public class TalentPushController : Controller
    {
        #region DAL操作对象
        RequirementDAL requirementDAL = new RequirementDAL();
        CustomerCompnayDAL customercompnayDAL = new CustomerCompnayDAL();
        PushInterViewTableDAL pushviewDAL = new PushInterViewTableDAL();
        PersonalInfoDAL personalDAL = new PersonalInfoDAL();
        #endregion
        // GET: Requirement/Requirement
        [NoCache]
        public ActionResult Index(string JobName, string pid, int page = 1)
        {
            const int pageSize = 8;

            PushInverviewViewModel viewModel = new PushInverviewViewModel();
            string s = Request["pid"].ToString();
            IEnumerable<EF.Requirement> Requirements = null;
            int personid = int.Parse(HttpUtility.UrlDecode(pid));
            if (!string.IsNullOrWhiteSpace(JobName))
            {
                //
                int ee =Convert.ToInt32( HttpUtility.UrlDecode(JobName));
                Requirements = requirementDAL.GetRequirementListByJobName(ee).AsQueryable();                
                ViewBag.CurrentFilter = ee;
            }
            else
            {
                Requirements = requirementDAL.GetRequirementList(string.Empty).AsQueryable();
            }
            viewModel.Requirements = Requirements.OrderBy(m => m.ArrivalTime).ToPagedList(page, pageSize);
            viewModel.CustomerCompnays = customercompnayDAL.GetCustomerCompnayList(string.Empty);
            viewModel.PushInterViewTables = pushviewDAL.GetModelList(string.Empty);
            viewModel.Personal = personalDAL.GetPersonalInfo(personid);
            return View(viewModel);

        }

        // GET: TalentPush/TalentPush/Details/5
        public ActionResult Details(string rid, int pageIndex,int pid)
        {
            rid = HttpUtility.UrlDecode(rid);
            PushInverviewViewModel viewModel = new PushInverviewViewModel();
            
            if (!string.IsNullOrEmpty(rid))
            {
                viewModel.Requirement = requirementDAL.GetRequirement(int.Parse(rid));
                viewModel.Personal=personalDAL.GetPersonalInfo(pid);
            }
            else
            {
                viewModel.Requirement = new EF.Requirement();
                viewModel.Personal = new EF.PersonalInfo();
            }
            ViewBag.pageIndex = pageIndex;
            ViewBag.JobName = HttpUtility.UrlEncode(viewModel.Personal.Publications);
            ViewBag.pid = pid;
            return View(viewModel);
        }

        // GET: TalentPush/TalentPush/Edit/5
        public ActionResult Edit(string id, string viewS, string plushS)
        {
            id = HttpUtility.UrlDecode(id);
            viewS = HttpUtility.UrlDecode(viewS);
            plushS = HttpUtility.UrlDecode(plushS);
            string pid = HttpUtility.UrlDecode(Request["pid"]);
            string rid = HttpUtility.UrlDecode(Request["rid"]);
            string job = HttpUtility.UrlDecode(Request["job"]);
            EF.PushInterViewTable model = null;
            if (id == "-1")
            {
                model = new EF.PushInterViewTable();
                model.ID = Guid.NewGuid().ToString();
                model.InterviewStatus = viewS;
                model.PlushStatute = plushS;
                model.RequirementId = int.Parse(rid);
                model.PersonalInfoId = int.Parse(pid);
                pushviewDAL.Insert(model);
            }
            else
            {
                model = pushviewDAL.GetPushInterViewByWhere(id);
                model.InterviewStatus = viewS;
                model.PlushStatute = plushS;

                pushviewDAL.Update(model);

            }
            return RedirectToAction("Index",new { pid = pid , JobName = job });            
        }
        [NoCache]
        public ActionResult MuchEdit(string rids, string job,int pid)
        {
            try
            {
                foreach (var reqid in HttpUtility.UrlDecode(rids).Split(','))
                {
                    if (reqid == "") continue;
                    EF.PushInterViewTable pushView = pushviewDAL.GetPushInterViewByWhere(int.Parse(reqid), pid);
                    //判断当前实例是否存在
                    if (pushView == null)
                    {
                        #region  实例不存在
                        pushView = new EF.PushInterViewTable();
                        pushView.ID = Guid.NewGuid().ToString();
                        pushView.InterviewStatus = "0";
                        pushView.PlushStatute = "0";
                        pushView.RequirementId = int.Parse(reqid);
                        pushView.PersonalInfoId = pid;
                        pushviewDAL.Insert(pushView);
                        #endregion
                    }
                    else
                    {
                        #region 实例存在
                        pushView = pushviewDAL.GetPushInterViewByWhere(pushView.ID);
                        pushView.InterviewStatus = "0";
                        pushView.PlushStatute = "0";

                        pushviewDAL.Update(pushView);
                        #endregion
                    }
                }
            }
            catch { }

            return RedirectToAction("Index", new { pid = pid, JobName = job });
        }

        // GET: TalentPush/TalentPush/Delete/5
        [NoCache]
        public ActionResult Delete(string rids, string job, int pid)
        {
            try
            {
                foreach (var reqid in HttpUtility.UrlDecode(rids).Split(','))
                {
                    if (reqid == "") continue;
                    EF.PushInterViewTable pushView = pushviewDAL.GetPushInterViewByWhere(int.Parse(reqid), pid);
                    if (pushView != null)
                    {
                        //结果存在，则重置(删除)
                        pushviewDAL.Delete(pushView);
                    }
                }
            }
            catch { }
            return RedirectToAction("Index", new { pid = pid, JobName = job });
        
        }
    }
}
