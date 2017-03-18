using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Personal.Controllers
{
    public class PersonalController : Controller
    {
        #region dal操作对象
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();        
        PersonalInfoDAL personalDAL = new PersonalInfoDAL();
        PushInterViewTableDAL pushViewDAL = new PushInterViewTableDAL();
        RequirementDAL requirementDAL = new RequirementDAL();
        PersonsEntrySetDAL personEntryDAL = new PersonsEntrySetDAL();
        //人员派遣数据操作类
        EmployeeExpatriationDAL employeeDAL = new EmployeeExpatriationDAL();
        //合作合同数据操作类
        CooperativeContractDAL cooperativeContractDAL = new CooperativeContractDAL();
        #endregion
        // GET: Personal/Personal
        [NoCache]
        public ActionResult Index(string loginid, string rid, int page = 1)
        {

            Models.PersonalRequirementViewModel viewModel = new Models.PersonalRequirementViewModel();
            try
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
                ViewBag.oldpage = Request["oldpage"] == null ? 1 : Convert.ToInt32(Request["oldpage"]);
                const int pageSize = 8;
                List<PushInterViewTable> pushList = pushViewDAL.QueryModelList(string.Format("RequirementId={0}", HttpUtility.UrlDecode(rid)));
                List<PersonalInfo> personallist = new List<PersonalInfo>();
                Requirement requireModel = requirementDAL.GetRequirement(int.Parse(rid));
                foreach (var model in pushList)
                {
                    //获取未入职人员的信息
                    PersonalInfo person = personalDAL.GetNoEntryPersonalInfo(model.PersonalInfoId.Value);
                    if (person != null)
                        personallist.Add(person);
                }
                viewModel.Requirement = requireModel;
                viewModel.Personals = personallist.OrderBy(a => a.PersonalInfoId).ToPagedList(page, pageSize); 

            }
            catch(Exception ex) {

            }
            return View(viewModel);
        }

        // GET: TalentPush/TalentPush/Edit/5
        [NoCache]
        public ActionResult Edit(string loginid,string rid,string pid)
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

            pid = HttpUtility.UrlDecode(pid);
            rid = HttpUtility.UrlDecode(rid);
            Outsourcing.EF.PushInterViewTable model = pushViewDAL.GetPushInterViewByWhere(int.Parse(rid), int.Parse(pid));
            if (model == null)
            {
                model.ID = Guid.NewGuid().ToString();
                model.InterviewStatus = "0";
                model.PlushStatute = "0";
                model.RequirementId = int.Parse(rid);
                model.PersonalInfoId = int.Parse(pid);
                model.FeedbackState = "0";
                pushViewDAL.Insert(model);
            }
            else
            {
                model.InterviewStatus = "0";
                model.PlushStatute = "0";
                model.FeedbackState = "0";
                pushViewDAL.Update(model);

            }

            ////暂时未找到跳转不到指定Action原因，据说是因为域的问题
            string url = string.Format("/Personal/Personal/Index?loginid={0}&rid={1}&pid={2}", loginid, rid, pid);
            //return Content(url);           
            return RedirectToAction("Index", "Personal", new { area = "Personal", loginid = loginid, rid = rid });
        }

        /// <summary>
        /// 设置面试时间
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="rid"></param>
        /// <param name="pid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        // GET: TalentPush/TalentPush/Edit/5
        [NoCache]
        public ActionResult SetInterViewTime(string loginid, string rid, string pid,string time)
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

            pid = HttpUtility.UrlDecode(pid);
            rid = HttpUtility.UrlDecode(rid);
            time = HttpUtility.UrlDecode(time);
            Outsourcing.EF.PushInterViewTable model = pushViewDAL.GetPushInterViewByWhere(int.Parse(rid), int.Parse(pid));
            if (model == null)
            {
                model.ID = Guid.NewGuid().ToString();
                model.InterviewStatus = "0";
                model.PlushStatute = "0";
                model.RequirementId = int.Parse(rid);
                model.PersonalInfoId = int.Parse(pid);
                model.FeedbackState = "0";
                model.InterviewTime = Convert.ToDateTime(time);
                pushViewDAL.Insert(model);
            }
            else
            {
                model.InterviewTime = Convert.ToDateTime(time);
                pushViewDAL.Update(model);

            }
            //暂时未找到跳转不到指定Action原因，据说是因为域的问题
            string url = string.Format("loginid={0}&rid={1}&pid={2}", loginid, rid, pid);
            //Redirect(url);            
            return Content(url);
        }

        [NoCache]
        [HttpPost]
        public ActionResult SetInterStatus(string loginid, string rid, string pid, string status)
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

            pid = HttpUtility.UrlDecode(pid);
            rid = HttpUtility.UrlDecode(rid);
            status = HttpUtility.UrlDecode(status);//1成功；2失败
            Outsourcing.EF.PushInterViewTable model = pushViewDAL.GetPushInterViewByWhere(int.Parse(rid), int.Parse(pid));
            if (model == null)
            {
                model.ID = Guid.NewGuid().ToString();
                model.InterviewStatus = "0";
                model.PlushStatute = "0";
                model.RequirementId = int.Parse(rid);
                model.PersonalInfoId = int.Parse(pid);
                model.FeedbackState = "0";
                model.InterviewStatus = status;
                if (status == "2")
                {
                    model.InterviewResult = HttpUtility.UrlDecode(Request.Form["InterViewResult"]);
                }
                pushViewDAL.Insert(model);
            }
            else
            {
                model.InterviewStatus = status;
                if (status == "2")
                {
                    model.InterviewResult = HttpUtility.UrlDecode(Request.Form["InterViewResult"]);
                }
                pushViewDAL.Update(model);

            }
            //暂时未找到跳转不到指定Action原因，据说是因为域的问题
            string url = string.Format("loginid={0}&rid={1}&pid={2}", loginid, rid, pid);
            //Redirect(url);            
            return Content(url);
        }

        [NoCache]
        [HttpGet]
        public ActionResult GetInterResult(string rid, string pid)
        {
            pid = HttpUtility.UrlDecode(pid);
            rid = HttpUtility.UrlDecode(rid);
            Outsourcing.EF.PushInterViewTable model = pushViewDAL.GetPushInterViewByWhere(int.Parse(rid), int.Parse(pid));
            string message = "无";
            if (model != null)
            {
                message = model.InterviewResult;

            }          
            return Content(message);
        }

        /// <summary>
        /// 人员入职
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="comid"></param>
        /// <param name="pid"></param>
        /// <param name="outcid"></param>
        /// <param name="rid"></param>
        /// <param name="time"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PersonEntry(string loginid, int comid, int pid, int outcid,int rid, DateTime time,int money,string PersonLevel)
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

            #region 添加人员入职信息
            PersonsEntrySet model = new PersonsEntrySet();
            model.EntryDate = time;
            model.EntryStatus = 0; //0在职，1离职    
            model.PersonalInfoId = pid;
            model.CustomerCompnayCompnayId = comid;
            model.OutsourcingCompanyCompnayId = outcid;
            model.EntryMoney = money;
            personEntryDAL.Insert(model);


            // 更新人员表中的个人状态
            PersonalInfo person= personalDAL.GetPersonalInfo(pid);
            if (person != null)
            {
                person.PeopleStatue = 0;
                personalDAL.Update(person);
            }
            #endregion

            #region 人员派遣单指定
            EmployeeExpatriation employeeModel = employeeDAL.GetModelByParam(comid, outcid, model.EntryDate.Value);
            if (employeeModel == null)
            {
                employeeModel = new EmployeeExpatriation();
                #region 如果不存在当前人员派遣单，则添加新的
                employeeModel.ID = Guid.NewGuid().ToString();
                employeeModel.FirstPartyID = comid;
                employeeModel.SecondPartyID = outcid;
                employeeModel.EntryDate = model.EntryDate;
                employeeModel.FirstContent = ContractOpration.GetEmployeeExpatriationTop();
                employeeModel.SecondContent = ContractOpration.GetEmployeeExpatriationBottom();
                employeeModel.PersonList = ContractOpration.GetEmployeeExpatriationCenter(
                                                            person.PersonName,
                                                            person.Publications,
                                                            PersonLevel, //人员评定级别
                                                            model.EntryDate.Value.ToString("yyyy-MM-dd"),
                                                            model.EntryMoney.ToString());
                //是否同意 0：同意 1：审阅中
                employeeModel.FirstPartyStatus = 1;
                employeeModel.SecondPartyStatus = 1;
                //合同是否生效 0 未生效；1生效
                employeeModel.FirstPartEffectiveStatus = 0;
                employeeModel.SecondPartyEffectiveStatus = 0;
                //派遣协议状态合同状态 0：未生效 1：已生效
                employeeModel.ContractStatus = 0;

                employeeDAL.Insert(employeeModel);
                #endregion

            }
            else
            {
                //如果存在则更新到内容中
                string  str= ContractOpration.GetEmployeeExpatriationCenter(
                                            person.PersonName,
                                            person.Publications,
                                            "初级",
                                            model.EntryDate.Value.ToString("yyyy-MM-dd"),
                                            model.EntryMoney.ToString());
                employeeModel.PersonList += str;
                employeeDAL.Update(employeeModel);
            }
            #endregion

            #region 添加合作合同
            if (!cooperativeContractDAL.IsExist(comid, outcid))//判断当前合作合同是否存在；
            {
                DateTime dt = DateTime.Now;
                CooperativeContract cooperModel = new CooperativeContract();
                cooperModel.ID = Guid.NewGuid().ToString();
                cooperModel.ContractCode = string.Format("YJY-{0}-{1}-{2}", dt.Year, dt.Month, cooperativeContractDAL.GetContractNumberByDate());
                cooperModel.ContractFirstParty = comid;
                cooperModel.ContractSecondParty = outcid;
                cooperModel.ContractStatus = 0;//合同状态 0：未生效 1：已生效
                cooperModel.FirstPartEffectiveStatus = 0;
                cooperModel.SecondPartyEffectiveStatus = 0;

                cooperModel.SigningTime = dt;//签约时间
                cooperModel.FirstPartyStatus = 1; // 0：同意 1：审阅中
                cooperModel.SecondPartyStatus = 1;// 0：同意 1：审阅中

                //获取派遣协议模板
                cooperModel.ContractContent = ContractOpration.GetDispatchingAgreement();
                cooperativeContractDAL.Insert(cooperModel);
            }
            #endregion

            //暂时未找到跳转不到指定Action原因，据说是因为域的问题
            string url = string.Format("loginid={0}&rid={1}&pid={2}", loginid, rid, pid);                      
            return Content(url);
        }
    }
}