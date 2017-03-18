using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 个人结算和考勤的数据层
    /// </summary>
    public class LeaveDetailDAL: BaseDAL<LeaveDetail>
    {
        /// <summary>
        /// 根据个人结算标识获取考勤明细
        /// </summary>
        /// <param name="PersonSettlementID"></param>
        /// <returns></returns>
        public List<LeaveDetail> GetModelList(string PersonSettlementID, int Month)
        {
            List<LeaveDetail> list = new List<LeaveDetail>();
            using (var ct = new DB())
            {
                var result = ct.LeaveDetail.Include(v=>v.PersonSettlement)                    
                                           .Where(m => m.PersonSettlementID == PersonSettlementID
                                             && m.LeaveStartDate.Month == Month)
                                           .ToList();

                list = result;
            }
            return list;
        }

        /// <summary>
        /// 根据个人结算标识获取考勤明细
        /// </summary>
        /// <param name="PersonSettlementID"></param>
        /// <param name="dt"></param>
        /// <returns>返回JSON格式数据 </returns>
        public string GetModelListJSON(string PersonSettlementID, int Month)
        {
           
            string resStr = string.Empty;
            using (var ct = new DB())
            {
                var res = from s in ct.LeaveDetail
                          where s.PersonSettlementID == PersonSettlementID
                                && s.LeaveStartDate.Month == Month
                          orderby s.LeaveStartDate descending
                          select new
                          {
                              LeaveStartDate=s.LeaveStartDate,
                              LeaveEndDate= s.LeaveEndDate,
                              LeaveHours=s.LeaveHours,
                              LeaveMoney=s.LeaveMoney,
                              LeaveReason=s.LeaveReason,
                              LeaveType=s.LeaveType
                          };
               JavaScriptSerializer jss = new JavaScriptSerializer();
                resStr = jss.Serialize(res);
            }
            return resStr;
        }

        /// <summary>
        ///  通过ID查询实例
        /// </summary>
        /// <param name="ID">标识</param>
        /// <returns></returns>
        public LeaveDetail GetModelByID(string ID)
        {
            using (var ct = new DB())
            {
                return ct.LeaveDetail.FirstOrDefault(m => m.ID == ID);
            } 
        }
    }
}
