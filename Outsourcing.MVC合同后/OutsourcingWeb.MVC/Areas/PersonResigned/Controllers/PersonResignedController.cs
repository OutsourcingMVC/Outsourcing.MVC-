using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.PersonResigned.Models;
using OutsourcingWeb.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.PersonResigned.Controllers
{
    public class PersonResignedController : Controller
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

        // GET: PersonResigned/PersonResigned
        [NoCache]
        public ActionResult PersonsResignedList(string loginid,string type, int page = 1)
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
            Models.PersonsResignedViewModel viewModel = new Models.PersonsResignedViewModel();
            //判断离职日期不为空的人员
            viewModel.PersonsEntrySets = list.Where(m=>m.ResignedDate!=null).ToPagedList(page, 8);
            return View(viewModel);
        }

        /// <summary>
        /// 离职人员结算明细
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="isQueryCurrentMonth"></param>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        [NoCache]
        // GET: PersonResigned/PersonResigned
        public ActionResult PersonResignedSettlement(string loginid, string type, int page = 1, int isQueryCurrentMonth = 0, string searchStr = "")
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            //ViewBag.QueryMonth = isQueryCurrentMonth;

            PersonResignedSettlementsViewModel viewModel = new PersonResignedSettlementsViewModel();
            List<Outsourcing.EF.PersonSettlement> list = new List<PersonSettlement>();
            if (Session["ID"] != null)
            {
                string start = Request["startD"] == null ? "" : Request["startD"].ToString();
                string end = Request["endD"] == null ? "" : Request["endD"].ToString();
                int personID = int.Parse(Request["PersonID"]);
                ViewBag.personID = personID;
                list = personnelSettDAL.GetPersonModelList(Convert.ToInt32(Session["ID"]), type, personID, start, end);
            }
            //viewModel.PatternList = personEntryDAL.GetCompanyIDByUserType(type);
            #region 统计当前总支付金额
            double sum = list.Sum(m => m.RealWages);
            //foreach (var item in list)
            //{
            //    sum += item.RealWages;
            //}
            #endregion

            viewModel.PersonSettlements = list.ToPagedList(page, 7);
            viewModel.TotalMoney = sum;

            return View(viewModel);
        }


        /// <summary>
        /// 离职个人结算
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        [NoCache]
        public ActionResult SettlementPerson(string loginid, string type, string PersonSettlementID, int page)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            ViewBag.QueryMonth = Request["isQueryCurrentMonth"];

            //只查询当前日期中月的记录
            PersonSettlement model = personnelSettDAL.GetModelByParam(PersonSettlementID);

            if (model != null)
            {
                #region 计算考勤工资
                const double HourByDay = 0.125; //8小时按1天算
                float leaveDays = 0f, leaveTotalMoney = 0f; //事假小时与事假被扣总金额；
                float overTimesPremium = 0f, overTimePremiumTotalMoney = 0f;//加班补助金额和加班时间；
                float LeaveOffDay = 0f, LeaveOffDayTotalMoney = 0f;//事假(调休)的时长和金额
                float overTimes = 0f, overTimesTotalMoney = 0f; // 加班时长和加班金额;
                //统计考勤中的加班与请假天数
                foreach (var item in leaveDetailDAL.GetModelList(HttpUtility.HtmlEncode(PersonSettlementID), model.SettlementDate.Month))
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

            return RedirectToAction("PersonResignedSettlement", new { loginid = loginid, type = type, page = page, isQueryCurrentMonth = ViewBag.QueryMonth });
            //return View();
        }

        // GET: PersonResigned/PersonResigned/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
            
        }
        
        
    }
}
