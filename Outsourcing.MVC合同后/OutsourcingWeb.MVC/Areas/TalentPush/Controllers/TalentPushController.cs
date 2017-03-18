using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Common;
//using OutsourcingWeb.MVC.Areas.Requirement.Models;
using OutsourcingWeb.MVC.Areas.TalentPush.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.EF;

namespace OutsourcingWeb.MVC.Areas.TalentPush.Controllers
{
    public class TalentPushController : Controller
    {
        #region  DAL操作对象
        RequirementDAL requirementDAL = new RequirementDAL();
        CustomerCompnayDAL customercompnayDAL = new CustomerCompnayDAL();
        PushInterViewTableDAL pushviewDAL = new PushInterViewTableDAL();
        PersonalInfoDAL personalDAL = new PersonalInfoDAL();
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        #endregion
        // GET: Requirement/Requirement
        [NoCache]
        public ActionResult ShowTalentRequirement(string loginid,string JobName, string pid, int page = 1)
        {
            ValidateloginID(loginid);
            const int pageSize = 8;

            ViewBag.JobName = JobName;
            ViewBag.Pid = pid;

            PushInverviewViewModel viewModel = new PushInverviewViewModel();
            string s = Request["pid"].ToString();
            IEnumerable<Outsourcing.EF.Requirement> Requirements = null;
            int personid = int.Parse(HttpUtility.UrlDecode(pid));
            if (!string.IsNullOrWhiteSpace(JobName))
            {
                //
                string ee = HttpUtility.UrlDecode(JobName);
                int TecLanguage = Common.EnumHelper.GetEnumValueByDesc(JobName);
                Requirements = requirementDAL.GetRequirementListByJobName(TecLanguage).AsQueryable();
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
        public ActionResult Details(string rid, int pageIndex, int pid)
        {
            rid = HttpUtility.UrlDecode(rid);
            PushInverviewViewModel viewModel = new PushInverviewViewModel();

            if (!string.IsNullOrEmpty(rid))
            {
                viewModel.Requirement = requirementDAL.GetRequirement(int.Parse(rid));
                viewModel.Personal = personalDAL.GetPersonalInfo(pid);
            }
            else
            {
                viewModel.Requirement = new Outsourcing.EF.Requirement();
                viewModel.Personal = new Outsourcing.EF.PersonalInfo();
            }
            ViewBag.pageIndex = pageIndex;
            ViewBag.JobName = HttpUtility.UrlEncode(viewModel.Personal.Publications);
            ViewBag.pid = pid;
            return View(viewModel);
        }

        // GET: TalentPush/TalentPush/Edit/5
        public ActionResult Edit(string loginid, string id, string viewS, string plushS)
        {
            ValidateloginID(loginid);

            id = HttpUtility.UrlDecode(id);
            viewS = HttpUtility.UrlDecode(viewS);
            plushS = HttpUtility.UrlDecode(plushS);
            string pid = HttpUtility.UrlDecode(Request["pid"]);
            string rid = HttpUtility.UrlDecode(Request["rid"]);
            string job = HttpUtility.UrlDecode(Request["job"]);
            Outsourcing.EF.PushInterViewTable model = null;
            if (id == "-1")
            {
                model = new Outsourcing.EF.PushInterViewTable();
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
            PersonalInfo pModel = personalDAL.GetPersonalInfo(int.Parse(pid));
            if (pModel != null)
            {
                //如果当前人才被推送，则更新人员信息中的人员状态
                pModel.PeopleStatue = 2;
                personalDAL.Update(pModel);
            }

            return RedirectToAction("ShowTalentRequirement", new { pid = pid, JobName = job ,loginid= loginid });
        }
        [NoCache]
        public ActionResult MuchEdit(string loginid,string rids, string job, int pid)
        {
            try
            {
                ValidateloginID(loginid);
                foreach (var reqid in HttpUtility.UrlDecode(rids).Split(','))
                {
                    if (reqid == "") continue;
                    Outsourcing.EF.PushInterViewTable pushView = pushviewDAL.GetPushInterViewByWhere(int.Parse(reqid), pid);
                    //判断当前实例是否存在
                    if (pushView == null)
                    {
                        #region  实例不存在
                        pushView = new Outsourcing.EF.PushInterViewTable();
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

            return RedirectToAction("ShowTalentRequirement", new { pid = pid, JobName = job, loginid = loginid });

        }

        // GET: TalentPush/TalentPush/Delete/5
        [NoCache]
        public ActionResult Delete(string loginid,string rids, string job, int pid)
        {
            try
            {
                ValidateloginID(loginid);
                foreach (var reqid in HttpUtility.UrlDecode(rids).Split(','))
                {
                    if (reqid == "") continue;
                    Outsourcing.EF.PushInterViewTable pushView = pushviewDAL.GetPushInterViewByWhere(int.Parse(reqid), pid);
                    if (pushView != null)
                    {
                        //结果存在，则重置(删除)
                        pushviewDAL.Delete(pushView);
                    }
                }
            }
            catch { }
            return RedirectToAction("ShowTalentRequirement", new { pid = pid, JobName = job, loginid = loginid });


        }

        public void ValidateloginID(string loginid)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }

           
            List<CustomerOutsourc> custOut = custOutDAL.GetlList("CustomerOutID=" + loginid);
            if (custOut.Count > 0)
            {
                string types = custOut[0].Type.ToString();
                ViewBag.Type = types;
            }
        }
    }
}