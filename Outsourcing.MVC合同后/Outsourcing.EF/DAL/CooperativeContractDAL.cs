using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 合作合同数据层
    /// </summary>
    public class CooperativeContractDAL:BaseDAL<CooperativeContract>
    {
        /// <summary>
        /// 判断当前是否存在合作合同
        /// </summary>
        /// <param name="FirstParty">甲方标识</param>
        /// <param name="SeconedParty">乙方标识</param>
        /// <returns></returns>
        public bool IsExist(int FirstParty, int SeconedParty)
        {
            using (var ct = new DB())
            {
                var result = ct.CooperativeContract
                             .Where(m => m.ContractFirstParty == FirstParty
                             && m.ContractSecondParty == SeconedParty);
                if (result.ToList().Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断当前是否存在合作合同
        /// </summary>
        /// <param name="FirstParty">甲方标识</param>
        /// <param name="SeconedParty">乙方标识</param>
        /// <returns></returns>
        public CooperativeContract GetModel(int FirstParty, int SeconedParty)
        {
            using (var ct = new DB())
            {
                var result = ct.CooperativeContract
                             .Where(m => m.ContractFirstParty == FirstParty
                             && m.ContractSecondParty == SeconedParty)
                             .FirstOrDefault();
                    return result;                
            }
            
        }

        /// <summary>
        /// 根据时间获取生成合同编号
        /// </summary>
        /// <returns></returns>
        public int GetContractNumberByDate()
        {
            DateTime dt = DateTime.Now;
            using (var ct = new DB())
            {
                var result = ct.CooperativeContract
                             .Where(m => m.SigningTime.Value.Year==dt.Year
                                    && m.SigningTime.Value.Month==dt.Month);
                return result.ToList().Count + 1;
            }
            return 0;
        }

        /// <summary>
        /// 根据参数获取数据列表 
        /// </summary>
        /// <param name="type">当前登录用户的类型</param>
        /// <param name="typeID">当前用户标识</param>
        /// <param name="ContractCode">合同编号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public List<CooperativeContract> GetModelList(string type, int typeID, string ContractCode, string startDate, string endDate)
        {
            List<CooperativeContract> list = new List<CooperativeContract>();
            using (var dt = new DB())
            {
                var result = dt.CooperativeContract
                               .Include(x => x.OutsourcingCompany)
                               .Include(x => x.CustomerCompnay)
                               .AsQueryable<CooperativeContract>();
                switch (type)
                {
                    case "1"://外包公司
                        result = result.Where(m => m.ContractSecondParty == typeID);  
                        break;
                    case "2"://客户公司
                        result = result.Where(m => m.ContractFirstParty == typeID);                        
                        break;
                }
                //查询合同编码
                if (ContractCode != "")
                {
                    result = result.Where(m => m.ContractCode.Contains(ContractCode));
                }

                //查询输入的日期段数据
                //如果没有传入查询日期将默认查询当月数据
                if (startDate != "" && endDate != "")
                {
                    DateTime endConvert = DateTime.Parse(endDate);

                    DateTime start = DateTime.Parse(string.Format("{0}-01 00:00:00", startDate));
                    DateTime end = DateTime.Parse(string.Format("{0}-{1} 23:59:59", endDate, endConvert.AddMonths(1).AddDays(-1).Day));

                    result = result.Where(m => m.SigningTime >= start && m.SigningTime <= end);

                }
                list = result.ToList<CooperativeContract>();
            }
            return list;
        }

        /// <summary>
        /// 根据标识获取数据 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CooperativeContract GetModelByID(string ID)
        {
            using (var ct = new DB())
            {
                return ct.CooperativeContract.FirstOrDefault(m => m.ID == ID);
            }
           
        }
    }
}
