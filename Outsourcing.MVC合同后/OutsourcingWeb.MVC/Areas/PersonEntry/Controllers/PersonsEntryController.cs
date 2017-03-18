using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.PersonEntry.Controllers
{
    public class PersonsEntryController : Controller
    {
        #region DAL操作对象
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        PersonsEntrySetDAL personEntryDAL = new PersonsEntrySetDAL();
        CustomerCompnayDAL customerCompnayDAL = new CustomerCompnayDAL();
        OutsourcingCompanyDAL outCompanyDAL = new OutsourcingCompanyDAL();
        PersonalInfoDAL personInfoDAL = new PersonalInfoDAL();
        PersonnelSettlementDAL personnelSettDAL = new PersonnelSettlementDAL();//人员考勤结算
        LeaveDetailDAL leaveDetailDAL = new LeaveDetailDAL(); //请假加班明细
        #endregion

        // GET: PersonEntry/PersonsEntry
        [NoCache]
        public ActionResult PersonsEntryList(string loginid, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            CustomerOutsourc custOut = custOutDAL.GetModelByID(Convert.ToInt32(loginid));
            int? types = null;
            if (custOut != null)
            {
                types = custOut.Type;
                ViewBag.Type = types;
            }

            List<PersonsEntrySet> list = null;
            if (types != null)
            {
                switch (types)
                {
                    case 1:
                        #region  外包公司
                        OutsourcingCompany outsourcing = outCompanyDAL.GetOutsourcingCompany(custOut.CompanyUserName);
                        if (outsourcing != null)
                            list = personEntryDAL.GetModelList(outsourcing.CompnayId);
                        else
                            list = new List<PersonsEntrySet>();
                        #endregion
                        break;
                    case 2:
                        #region  客户公司
                        Outsourcing.EF.CustomerCompnay customer = customerCompnayDAL.GetModel(custOut.CompanyUserName);
                        if (customer != null)
                        {
                            list = personEntryDAL.GetModelList(customer.CompnayId, 2);
                        }
                        else
                        {
                            list = new List<PersonsEntrySet>();
                        }
                        #endregion
                        break;
                }
            }
            Models.PersonsEntrySetViewModel viewModel = new Models.PersonsEntrySetViewModel();
            viewModel.PersonsEntrySets = list.Where(m => m.ResignedDate == null).ToPagedList(page, 8);
            return View(viewModel);
        }

        /// <summary>
        /// 所属的公司的查询
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="StaffName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult RecManagements(string loginid, string type, string StaffName, int page = 1)
        {

            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type)) { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            int outID = Convert.ToInt32(Session["ID"]);
            Models.PersonsEntrySetViewModel viewModel = new Models.PersonsEntrySetViewModel();
            List< PersonsEntrySet> requirement = null;
            if (!string.IsNullOrWhiteSpace(StaffName))
            {
                StaffName = HttpUtility.UrlDecode(StaffName);
                requirement = personEntryDAL.QueryModelList(string.Format("  b. like '%{0}%'",  StaffName));
                ViewBag.CurrentFilter = StaffName;
            }
            else
            {
                requirement = personEntryDAL.QueryModelList(string.Format("   b.IsDelete=1", outID));
            }
            viewModel.PersonsEntrySets = requirement.ToPagedList(page, 6);
            return View(viewModel);
        }

        /// <summary>
        /// 设置离职时间
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="ID"></param>
        /// <param name="ResignedDate"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [NoCache]
        [HttpPost]
        public ActionResult SetResignedDate(string loginid, int ID, DateTime ResignedDate, int pageIndex)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            CustomerOutsourc custOut = custOutDAL.GetModelByID(Convert.ToInt32(loginid));
            if (custOut != null)
            {
                ViewBag.Type = custOut.Type;
            }

            PersonsEntrySet personEntry = personEntryDAL.GetModelBy(ID);
            if (personEntry != null)
            {
                personEntry.ResignedDate = ResignedDate;
                personEntry.EntryStatus = 1;
                personEntryDAL.Update(personEntry);

                //离职后更改个人信息中个人状态
                PersonalInfo person = personInfoDAL.GetPersonalInfo(personEntry.PersonalInfoId);
                if (person != null)
                {
                    person.PeopleStatue = 1;
                    personInfoDAL.Update(person);
                }
                //离职后直接结算个人账务
                DateTime d1 = new DateTime(ResignedDate.Year, ResignedDate.Month, 1); //当月第一天
                DateTime d2 = d1.AddMonths(1).AddDays(-1); //当月最后一天

                long workDays = dateDiff(d1, d2);//本月工作日
                long RealworkDays = dateDiff(d1, ResignedDate);//本月实际工作日


                //通过参数获取个人结算表中当月是否有记录
                PersonSettlement model = personnelSettDAL.GetModelByParam(personEntry.PersonalInfoId, personEntry.OutsourcingCompanyCompnayId, personEntry.CustomerCompnayCompnayId, ResignedDate);
                if (model == null)//没有就添加
                {
                    model = new PersonSettlement();
                    model.ID = Guid.NewGuid().ToString();
                    model.PersonID = personEntry.PersonalInfoId;
                    model.CustomerID = personEntry.CustomerCompnayCompnayId;
                    model.OutCompanyID = personEntry.OutsourcingCompanyCompnayId;
                }
                model.WorkDays = (int)workDays;
                model.SettlementDate = ResignedDate;
                model.Wages = (double)personEntryDAL.GetModelBy(personEntry.PersonalInfoId, personEntry.CustomerCompnayCompnayId, personEntry.OutsourcingCompanyCompnayId).EntryMoney;

                if (model == null)
                    personnelSettDAL.Insert(model);

                #region 计算考勤工资
                const double HourByDay = 0.125; //8小时按1天算

                //工作日-实际出勤日=请假日或未上班的天数
                double leaveDays = (workDays - RealworkDays) * 8;
                //事假小时与事假被扣总金额；
                double leaveTotalMoney = ((model.Wages / workDays) / 8 * leaveDays) * -1;

                float overTimesPremium = 0f, overTimePremiumTotalMoney = 0f;//加班补助金额和加班时间；
                float LeaveOffDay = 0f, LeaveOffDayTotalMoney = 0f;//事假(调休)的时长和金额
                float overTimes = 0f, overTimesTotalMoney = 0f; // 加班时长和加班金额;
                #region 统计考勤中的加班与请假天数
                foreach (var item in leaveDetailDAL.GetModelList(model.ID, model.SettlementDate.Month))
                {
                    switch (item.LeaveType)
                    {
                        case 1: //事假
                            leaveDays += (float)item.LeaveHours;
                            leaveTotalMoney += (float)item.LeaveMoney;
                            break;
                        case 2: //加班补助
                            overTimesPremium += (float)item.LeaveHours;
                            overTimePremiumTotalMoney += (float)item.LeaveMoney;
                            break;
                        case 3://事假调休
                            LeaveOffDay += (float)item.LeaveHours;
                            LeaveOffDayTotalMoney += (float)item.LeaveMoney;
                            break;
                        case 4://加班
                            overTimes += (float)item.LeaveHours;
                            overTimesTotalMoney += (float)item.LeaveMoney;
                            break;
                    }
                }
                #endregion

                //工资结算中只包含加班补助和事假工资计算；
                // 计算请假天数=请假天数+调休天数
                model.LeaveDays = leaveDays == 0 ? 0 : Math.Round((leaveDays + LeaveOffDay) * HourByDay, 2);
                //计算加班天数
                model.OvertimeDays = overTimes == 0 ? 0 : Math.Round(overTimes * HourByDay, 2);
                //计算实际工作日=本月工作日-（请假时间+调休时间）*时间基数
                model.RealWorkDays = Math.Round(model.WorkDays - (leaveDays + LeaveOffDay) * HourByDay, 2);
                //到手工资=应得工资+加班补助+事假工资
                model.RealWages = Math.Round(model.Wages + overTimePremiumTotalMoney + leaveTotalMoney, 2);//计算请假后的工资；
                personnelSettDAL.Update(model);
                #endregion
            }

            string url = string.Format("loginid={0}&page={1}", loginid, pageIndex);
            return Content(url);
        }

        [NoCache]
        public ActionResult PersonSettl(string loginid, int PersonID, int CustomerID, int OutCompanyID, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            CustomerOutsourc custOut = custOutDAL.GetModelByID(Convert.ToInt32(loginid));
            if (custOut != null)
            {
                ViewBag.Type = custOut.Type;
            }
            ViewBag.PersonID = PersonID;
            ViewBag.CustomerID = CustomerID;
            ViewBag.OutCompanyID = OutCompanyID;

            Models.PersonSettlViewModel viewModel = new Models.PersonSettlViewModel();

            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1); //当月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1); //当月最后一天

            long workDays = dateDiff(d1, d2);//获取当前月工作天数
            viewModel.StartDate = d1.ToString("yyyy-MM-dd");
            viewModel.EndDate = d2.ToString("yyyy-MM-dd");
            viewModel.workDays = workDays;

            PersonSettlement model = personnelSettDAL.GetModelByParam(PersonID, OutCompanyID, CustomerID, d1);
            if (model == null)
            {
                model = new PersonSettlement();
                model.ID = Guid.NewGuid().ToString();
                model.PersonID = PersonID;
                model.CustomerID = CustomerID;
                model.OutCompanyID = OutCompanyID;
                model.WorkDays = (int)workDays;
                model.SettlementDate = DateTime.Now;
                model.Wages = (double)personEntryDAL.GetModelBy(PersonID, CustomerID, OutCompanyID).EntryMoney;
                personnelSettDAL.Insert(model);
            }
            //else
            //{

            //}
            viewModel.LeaveDetail = model.LeaveDetail.OrderBy(m => m.LeaveStartDate).ToPagedList<LeaveDetail>(page, 8);
            viewModel.Money = model.Wages;
            ViewBag.PersonSettlementID = model.ID;
            return View(viewModel);

        }

        [NoCache]
        [HttpPost]
        public ActionResult AddPersonLeaveDetail(string loginid)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            CustomerOutsourc custOut = custOutDAL.GetModelByID(Convert.ToInt32(loginid));
            if (custOut != null)
            {
                ViewBag.Type = custOut.Type;
            }
            ViewBag.PersonID = Request["PersonID"];
            ViewBag.CustomerID = Request["CustomerID"];
            ViewBag.OutCompanyID = Request["OutCompanyID"];

            #region 初始化数据
            LeaveDetail leaveDetail = new LeaveDetail();
            leaveDetail.ID = Guid.NewGuid().ToString();
            leaveDetail.PersonSettlementID = Request["ID"].ToString();
            leaveDetail.LeaveStartDate = Convert.ToDateTime(Request["stime"]);
            leaveDetail.LeaveEndDate = Convert.ToDateTime(Request["etime"]);
            leaveDetail.LeaveHours = (decimal)(leaveDetail.LeaveEndDate - leaveDetail.LeaveStartDate).TotalHours;
            leaveDetail.LeaveReason = Request["LeaveReason"].ToString();

            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1); //当月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1); //当月最后一天
            long workDays = dateDiff(d1, d2);//获取当前月工作天数
            decimal money = Convert.ToDecimal(Request["Moneys"]);
            switch (Convert.ToInt32(Request["leaveType"]))
            {
                case 1: //请假
                    leaveDetail.LeaveType = 1;
                    //（工资÷工作日总数）÷工作8小时×请假时间
                    leaveDetail.LeaveMoney = ((money / workDays) / 8 * leaveDetail.LeaveHours) * -1;
                    break;
                case 2://加班补助                    
                    //加班补助直接录入
                    leaveDetail.LeaveMoney = money;
                    leaveDetail.LeaveType = 2;
                    break;
                case 3://事假调休                    
                    //事假调休不计入工资计算
                    leaveDetail.LeaveMoney = 0;
                    leaveDetail.LeaveType = 3;
                    break;
                case 4: //加班
                    //加班不计算加班费算法 
                    leaveDetail.LeaveMoney = 0;
                    leaveDetail.LeaveType = 4;
                    break;

            }
            leaveDetailDAL.Insert(leaveDetail);
            #endregion

            string url = string.Format("loginid={0}&&PersonID={1}&&CustomerID={2}&&OutCompanyID={3}&&page ={4}", loginid, ViewBag.PersonID, ViewBag.CustomerID, ViewBag.OutCompanyID, 1);
            return Content(url);
            //return RedirectToAction("PersonSettl", new { loginid=loginid, PersonID= ViewBag.PersonID, CustomerID= ViewBag.CustomerID, OutCompanyID= ViewBag.OutCompanyID, page = 1 });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="PersonID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="OutCompanyID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult DelPersonLeaveDetail(string loginid, string type, string ID, int PersonID, int CustomerID, int OutCompanyID, int page)
        {
            string msg = "删除失败";
            #region 保存登录信息
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }

            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            #endregion  

            LeaveDetail model = leaveDetailDAL.GetModelByID(HttpUtility.HtmlDecode(ID));
            //如果存在则删除当前对象
            if (model != null)
            {
                leaveDetailDAL.Delete(model);
                msg = "删除成功";
            }
            StringBuilder urlStr = new StringBuilder();
            urlStr.AppendFormat("?loginid={0}&&type={1}&&PersonID={2}", loginid, type, PersonID);
            urlStr.AppendFormat("&&CustomerID={0}&&OutCompanyID={1}&&page={2}", CustomerID, OutCompanyID, page);

            return Content(urlStr.ToString());
        }

        /// 获取日期段里的工作日【除去 周六、日】  
        /// </summary>  
        /// <param name="startDate"></param>  
        /// <param name="endDate"></param>  
        /// <returns></returns>  
        long dateDiff(DateTime startDate, DateTime endDate)
        {
            DateTime fromTime = startDate;
            DateTime toTime = endDate;
            TimeSpan ts = toTime.Subtract(fromTime);//TimeSpan得到fromTime和toTime的时间间隔  
            long countday = ts.Days;//获取两个日期间的总天数  
            long weekday = 0;//工作日  
            //循环用来扣除总天数中的双休日  
            for (int i = 0; i <= countday; i++)
            {
                DateTime tempdt = fromTime.Date.AddDays(i);
                if (tempdt.DayOfWeek != System.DayOfWeek.Saturday && tempdt.DayOfWeek != System.DayOfWeek.Sunday)
                {
                    weekday++;
                }
            }

            return weekday;
        }
    }
}