using Outsourcing.EF;
using Outsourcing.EF.DAL;
using OutsourcingWeb.MVC.Areas.PersonnelSettlement.Models;
using OutsourcingWeb.MVC.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OutsourcingWeb.MVC.Areas.PersonnelSettlement.Controllers
{
    /// <summary>
    /// 人员结算
    /// </summary>
    public class PersonnelSettlementController : Controller
    {
        bool QueryMonth = false;
        #region DAL操作对象
        
        CustomerOutsourcDAL custOutDAL = new CustomerOutsourcDAL();
        PersonsEntrySetDAL personEntryDAL = new PersonsEntrySetDAL();
        CustomerCompnayDAL customerCompnayDAL = new CustomerCompnayDAL();
        OutsourcingCompanyDAL outCompanyDAL = new OutsourcingCompanyDAL();
        PersonalInfoDAL personInfoDAL = new PersonalInfoDAL();
        PersonnelSettlementDAL personnelSettDAL = new PersonnelSettlementDAL();
        LeaveDetailDAL leaveDetailDAL = new LeaveDetailDAL(); //考勤明细
        #endregion


        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="isQueryCurrentMonth"></param>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        [NoCache]
        // GET: PersonnelSettlement/PersonnelSettlement
        public ActionResult Index(string loginid,string type, int page = 1)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }
            //ViewBag.QueryMonth = isQueryCurrentMonth;  

            PersonSettlementsViewModel viewModel = new PersonSettlementsViewModel();
            List<Outsourcing.EF.PersonSettlement> list = new List<PersonSettlement>();
            if (Session["ID"] != null)
            {
                //是否查询当月的 0不查，1查当月
                //if (isQueryCurrentMonth == 0)
                //{

                string start = Request["startD"] == null ? "" : Request["startD"].ToString();
                string end = Request["endD"] == null ? "" : Request["endD"].ToString();
                int personID = int.Parse(Request["PersonID"]);
                ViewBag.personID = personID;
                list = personnelSettDAL.GetPersonModelList(Convert.ToInt32(Session["ID"]), type, personID, start, end);                                                
                //}
                //else
                //{
                //    string start = Request["startD"]==null?"":Request["startD"].ToString();
                //    string end = Request["endD"] == null ? "" : Request["endD"].ToString();
                //    string partner = Request["partner"] == null ? "" : HttpUtility.UrlDecode(Request["partner"].ToString());
                //    string[] CompanyIDs = partner == "" ? new string[] { } : partner.Split(',');
                //    list = personnelSettDAL.GetModelList(Convert.ToInt32(Session["ID"]), DateTime.Now.Month, type, CompanyIDs, start, end);                          
                //}
            }
            //viewModel.PatternList =personEntryDAL.GetCompanyIDByUserType(type);
            #region 统计当前总支付金额
            double sum = list.Sum(m => m.RealWages);
            //foreach (var item in list)
            //{
            //    sum += item.RealWages;
            //}
            #endregion

            viewModel.PersonSettlements=list.ToPagedList(page, 7);
            viewModel.TotalMoney = sum;

            return View(viewModel);
        }


        /// <summary>
        /// 个人结算
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        [NoCache]
        public ActionResult SettlementPerson(string loginid,string type,string PersonSettlementID,int page)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(type))
            { ViewBag.Type = type; }
            else { ViewBag.Type = ""; }

            int personID = int.Parse(Request["PersonID"]);
            ViewBag.personID = personID;
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
                model.LeaveDays = leaveDays == 0 ? 0 : Math.Round((leaveDays+ LeaveOffDay) * HourByDay, 2);
                //计算加班天数
                model.OvertimeDays = overTimes == 0 ? 0 : Math.Round(overTimes * HourByDay, 2); 
                //计算实际工作日=本月工作日-（请假时间+调休时间）*时间基数
                model.RealWorkDays = Math.Round(model.WorkDays - (leaveDays + LeaveOffDay) * HourByDay, 2);
                //到手工资=应得工资+加班补助+事假工资
                model.RealWages = Math.Round(model.Wages + overTimePremiumTotalMoney+ leaveTotalMoney, 2);//计算请假后的工资；
                personnelSettDAL.Update(model);
                #endregion
            }

            return RedirectToAction("Index",new { loginid= loginid, type = type, page = page, personID= personID, isQueryCurrentMonth = ViewBag.QueryMonth });
            //return View();
        }

        // GET: PersonnelSettlement/PersonnelSettlement/Details/5
        public JsonResult Details(string loginid, string types, string PersonSettlementID)
        {
            if (!string.IsNullOrEmpty(loginid))
            { ViewBag.LoginID = loginid; }
            else { ViewBag.LoginID = ""; }
            if (!string.IsNullOrEmpty(types))
            { ViewBag.Type = types; }
            else { ViewBag.Type = ""; }

            string JsonStr = string.Empty;
            //只查询当前日期中月的记录
            PersonSettlement model = personnelSettDAL.GetModelByParam(PersonSettlementID);
            if (model != null)
            {
                JsonStr = leaveDetailDAL.GetModelListJSON(PersonSettlementID, model.SettlementDate.Month);
            }           

            return Json(JsonStr);
        }

    }
}
