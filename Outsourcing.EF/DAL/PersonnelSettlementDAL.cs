using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    public class PersonnelSettlementDAL:BaseDAL<PersonSettlement>
    {

        /// <summary>
        /// 查询列表结果(针对人员)
        /// </summary>
        /// <param name="personid">人员标识</param>
        /// <param name="outCid">外包公司标识</param>
        /// <param name="CustomerID">客户公司标识</param>
        /// <returns></returns>
        public List<PersonSettlement> GetModelList(int personid,int outCid,int CustomerID)
        {
            List<PersonSettlement> resultList = new List<PersonSettlement>();
            using (var ct = new DB())
            {
                //string sql = "select * from PersonSettlement";
                //if (!string.IsNullOrEmpty(where))
                //{
                //    sql += where;
                //}
                resultList = ct.PersonSettlement
                              .Include(x => x.CustomerCompnay)
                              .Include(x => x.OutsourcingCompany)
                              .Include(x => x.PersonalInfo)
                              .Include(x => x.LeaveDetail)
                              .Where(m=>m.CustomerID==CustomerID && m.PersonID==personid && m.OutCompanyID==outCid)
                              .ToList<PersonSettlement>();
            }

            return resultList;
        }

        /// <summary>
        /// 根据当前用户类型和标识查询结果
        /// </summary>
        /// <param name="typeID">用户标识</param>
        /// <param name="Month">当前月份</param>
        /// <param name="type">用户类型 1：外包公司 2：客户公司</param>
        /// <returns></returns>
        public List<PersonSettlement> GetModelList(int typeID, string type, string[] CompanyIds, string StartDate, string EndDate)
        {
            List<PersonSettlement> resultList = new List<PersonSettlement>();
            using (var ct = new DB())
            {
                switch(type)
                {
                    case "1":
                        #region 外包公司
                        var outResult = ct.PersonSettlement
                                       .Include(x => x.CustomerCompnay)
                                       .Include(x => x.OutsourcingCompany)
                                       .Include(x => x.PersonalInfo)
                                       .Include(x => x.LeaveDetail)
                                       .Where(m => m.OutCompanyID == typeID);                                       
                        //查询所合作的公司
                        if (CompanyIds.Length > 0)
                        {
                            if (CompanyIds.Length == 1)
                            {
                                string id = CompanyIds[0];
                                outResult = outResult.Where(m => m.CustomerID.ToString() == id);
                            }
                            else
                            {
                                outResult = outResult.Where(m => CompanyIds.Contains(m.CustomerID.ToString()));
                            }
                        }

                        //查询输入的日期段数据
                        //如果没有传入查询日期将默认查询当月数据
                        if (StartDate != "" && EndDate != "")
                        {
                            DateTime endConvert = DateTime.Parse(EndDate);

                            DateTime start = DateTime.Parse(string.Format("{0}-01 00:00:00", StartDate));
                            DateTime end = DateTime.Parse(string.Format("{0}-{1} 23:59:59", EndDate, endConvert.AddMonths(1).AddDays(-1).Day));

                            outResult = outResult.Where(m => m.SettlementDate >= start && m.SettlementDate <= end);

                        }
                        else
                        {
                            outResult.Where(m => m.SettlementDate.Month == DateTime.Now.Month);
                        }
                        resultList = outResult.ToList<PersonSettlement>();
                        #endregion
                        break;
                    case "2":
                        #region 客户公司
                        var result = ct.PersonSettlement
                                      .Include(x => x.CustomerCompnay)
                                      .Include(x => x.OutsourcingCompany)
                                      .Include(x => x.PersonalInfo)
                                      .Include(x => x.LeaveDetail)
                                      .Where(m => m.CustomerID == typeID);                        

                        //查询所合作的公司
                        if (CompanyIds.Length > 0)
                        {
                            if (CompanyIds.Length == 1)
                            {
                                string id = CompanyIds[0];
                                result = result.Where(m => m.OutCompanyID.ToString()== id);
                            }
                            else
                            {
                                result = result.Where(m => CompanyIds.Contains(m.OutCompanyID.ToString()));
                            }
                        }
                       
                        //查询输入的日期段数据
                        //如果没有传入查询日期将默认查询当月数据
                        if (StartDate != "" && EndDate != "")
                        {
                            DateTime endConvert = DateTime.Parse(EndDate);

                            DateTime start = DateTime.Parse(string.Format("{0}-01 00:00:00", StartDate));
                            DateTime end = DateTime.Parse(string.Format("{0}-{1} 23:59:59", EndDate, endConvert.AddMonths(1).AddDays(-1).Day));

                            result = result.Where(m => m.SettlementDate >= start && m.SettlementDate <= end);
                          
                        }
                        else  
                        {
                            result.Where(m=>m.SettlementDate.Month==DateTime.Now.Month);
                        }
                        resultList =result.ToList<PersonSettlement>();
                        break;
                        #endregion
                }
            }

            return resultList;
        }

        /// <summary>
        ///个人结算明细根据当前用户类型和标识查询结果
        /// </summary>
        /// <param name="typeID">用户标识</param>
        /// <param name="type">用户类型 1：外包公司 2：客户公司</param>
        /// <returns></returns>
        public List<PersonSettlement> GetPersonModelList(int typeID,  string type,int personID,string StartDate,string EndDate)
        {
            List<PersonSettlement> resultList = new List<PersonSettlement>();
            using (var ct = new DB())
            {
                switch (type)
                {
                    case "1":
                        #region 外包公司查询
                        var OutResult = ct.PersonSettlement
                                       .Include(x => x.CustomerCompnay)
                                       .Include(x => x.OutsourcingCompany)
                                       .Include(x => x.PersonalInfo)
                                       .Include(x => x.LeaveDetail)
                                       .Where(m => m.OutCompanyID == typeID && m.PersonID == personID)
                                       .OrderByDescending(m => m.SettlementDate);
                                       //.ToList<PersonSettlement>();

                        if (StartDate != "" && EndDate != "")
                        {
                            DateTime start = DateTime.Parse(StartDate);
                            DateTime end = DateTime.Parse(EndDate);
                            resultList = OutResult.Where(m => m.SettlementDate.Month >= start.Month
                                             && m.SettlementDate.Month <= end.Month).ToList<PersonSettlement>();
                        }
                        else
                        {
                            resultList = OutResult.ToList<PersonSettlement>();
                        }
                        #endregion
                        break;
                    case "2":
                        #region 客户公司
                        var result = ct.PersonSettlement
                                      .Include(x => x.CustomerCompnay)
                                      .Include(x => x.OutsourcingCompany)
                                      .Include(x => x.PersonalInfo)
                                      .Include(x => x.LeaveDetail)
                                      .Where(m => m.CustomerID == typeID && m.PersonID == personID)
                                      .OrderByDescending(m => m.SettlementDate);


                        if (StartDate != "" && EndDate != "")
                        {
                            DateTime start = DateTime.Parse(StartDate);
                            DateTime end = DateTime.Parse(EndDate);
                            resultList = result.Where(m => m.SettlementDate.Month >= start.Month
                                             && m.SettlementDate.Month <= end.Month).ToList<PersonSettlement>();
                        }
                        else
                        {
                            resultList = result.ToList<PersonSettlement>();
                        }
                        #endregion
                        break;
                }
            }

            return resultList;
        }

        /// <summary>
        /// 根据参数查询结果
        /// </summary>
        /// <param name="personid"></param>
        /// <param name="outID"></param>
        /// <param name="customerid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public PersonSettlement GetModelByParam(int personid, int outID, int customerid, DateTime dt)
        {
            PersonSettlement model = null;
          
            using (var ct = new DB())
            {
                model = ct.PersonSettlement.Include(m=>m.LeaveDetail).Where(w => (w.PersonID == personid && w.OutCompanyID == outID)
                                                ).FirstOrDefault(m => m.CustomerID == customerid
                                                && m.SettlementDate.Year==dt.Year && m.SettlementDate.Month==dt.Month
                                                );
            }
            return model;
        }

        /// <summary>
        /// 根据参数查询结果
        /// </summary>
        /// <param name="ID">当前数据标识</param>
        /// <returns></returns>
        public PersonSettlement GetModelByParam(string ID)
        {
            PersonSettlement model = null;

            using (var ct = new DB())
            {
                model = ct.PersonSettlement.FirstOrDefault(m => m.ID == ID);
            }
            return model;
        }

        public List<SettlementGroupModel> GetResultGroupBy(int typeID, string type)
        {
            List<SettlementGroupModel> resultList = new List<SettlementGroupModel>();
            using (var ct = new DB())
            {
                switch (type)
                {
                    case "1":
                        #region 外包公司查询
                        var OutResult = ct.PersonSettlement
                                       .Include(x => x.CustomerCompnay)
                                       .Include(x => x.OutsourcingCompany)
                                       .Include(x => x.PersonalInfo)
                                       //.Include(x => x.LeaveDetail)
                                       .Where(m => m.OutCompanyID == typeID)
                                       .OrderByDescending(m => m.SettlementDate);
                        #endregion
                        break;
                    case "2":
                        #region 客户公司
                        //查询出所以数据
                        var result = ct.PersonSettlement
                                      .Include(x => x.CustomerCompnay)
                                      .Include(x => x.OutsourcingCompany)
                                      .Include(x => x.PersonalInfo)
                                      .Where(m => m.CustomerID == typeID);
                        var list = from k in result
                                   group k by new
                                   {
                                       k.SettlementDate.Month,
                                       k.SettlementDate.Year,
                                       k.OutCompanyID,
                                       k.OutsourcingCompany,
                                       k.CustomerCompnay
                                   }
                                 into g
                                   select new SettlementGroupModel
                                   {
                                       companyName = g.Key.OutsourcingCompany.CompnayName,
                                       companyID = g.Key.OutCompanyID,
                                       Years = g.Key.Year,
                                       Month = g.Key.Month,
                                       TotalMoney = g.Sum(c => c.RealWages),
                                       CustomName = g.Key.CustomerCompnay.CompnayName
                                      
                                   };

                        #endregion
                        resultList = list.ToList<SettlementGroupModel>();
                        break;
                }
            }

            return resultList;
        }
    }

    public class SettlementGroupModel {
        public string companyName { get; set; }

        public string CustomName { get; set; }
        public int companyID { get; set; }
        public int Years { get; set; }
        public int Month { get; set; }

         public double TotalMoney { get; set; }
    }
}
