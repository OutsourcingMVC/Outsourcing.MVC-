using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Outsourcing.EF.DAL
{ 
    /// <summary>
    /// 人员派遣表数据层
    /// </summary>
    public class EmployeeExpatriationDAL : BaseDAL<EmployeeExpatriation>
    {
        /// <summary>
        /// 根据参数返回实例数据 
        /// </summary>
        /// <param name="FirstPartyID">甲方标识</param>
        /// <param name="SecondPartyID">乙方标识</param>
        /// <param name="ExpaDate">派遣日期</param>
        /// <returns></returns>
        public EmployeeExpatriation GetModelByParam(int FirstPartyID,int SecondPartyID,DateTime ExpaDate)
        {
            using (var ct = new DB())
            {
                var model = ct.EmployeeExpatriation
                            .Include(x => x.CustomerCompnay)
                            .Include(x => x.OutsourcingCompany)
                            .Where(m => m.FirstPartyID == FirstPartyID
                                   && m.SecondPartyID == SecondPartyID
                                   && m.EntryDate.Value == ExpaDate)
                            .FirstOrDefault<EmployeeExpatriation>();
                return model;
            }
            //return null;
        }


        /// <summary>
        /// 根据参数返回实例数据 
        /// </summary>
        /// <param name="ID">当前数据标识标识</param>
        /// <returns></returns>
        public EmployeeExpatriation GetModelByParam(string ID)
        {
            using (var ct = new DB())
            {
                var model = ct.EmployeeExpatriation
                            .Include(x => x.CustomerCompnay)
                            .Include(x => x.OutsourcingCompany)
                            .Where(m => m.ID == ID)
                            .FirstOrDefault<EmployeeExpatriation>();
                return model;
            }
            //return null;
        }

        /// <summary>
        /// 根据甲方、乙方标识查询所属数据 
        /// </summary>
        /// <param name="FirstPartyID">甲方标识</param>
        /// <param name="SecondPartyID">乙方标识</param>
        /// <returns></returns>
        public List<EmployeeExpatriation> GetModelList(int FirstPartyID, int SecondPartyID)
        {
            using (var ct = new DB())
            {
                var list = ct.EmployeeExpatriation
                            .Include(x => x.CustomerCompnay)
                            .Include(x => x.OutsourcingCompany)
                            .Where(m => m.FirstPartyID == FirstPartyID
                                   && m.SecondPartyID == SecondPartyID)
                            .ToList<EmployeeExpatriation>();
                return list;
            }
            //return null;
        }
    }
}
