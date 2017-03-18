using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 发表申请数据层
    /// </summary>
    public class InvoiceApplicationDAL:BaseDAL<InvoiceApplication>
    {
        /// <summary>
        /// 判断当前是否存在相同数据
        /// <param name="type">当前登录用户的类型</param>
        /// <param name="typeID">当前登录用户的标识</param>
        /// <param name="year">查询年</param>
        /// <param name="month">查询月</param>
        /// <returns>实例存在返回true,否则false</return
        public bool IsExit(string type,int typeID,int year,int month)
        {
            InvoiceApplication model = null;
            using (var dt = new DB())
            {
                switch (type)
                {
                    case "1"://外包公司  未测试
                        model = dt.InvoiceApplication.FirstOrDefault(
                            m => m.CustomerCompanyID == typeID
                            && m.SettlementYear == year
                            && m.SettlementMonth == month
                            );
                        break;
                    case "2"://客户公司
                        model = dt.InvoiceApplication.FirstOrDefault(
                            m=>m.OutrCompanyID==typeID
                            && m.SettlementYear==year
                            && m.SettlementMonth==month
                            );

                        break;
                }
            }
            return model == null ? false : true; 
        }

        /// <summary>
        /// 获取已申请的发票数据信息
        /// </summary>
        /// <param name="type">当前登录用户的类型</param>
        /// <param name="typeID">当前用户标识</param>
        /// <param name="companyIDS">要查询公司的集合</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public List<InvoiceApplication> GetModelList(string type, int typeID,string[] companyIDS,string startDate,string endDate)
        {
            List<InvoiceApplication> list = new List<InvoiceApplication>();
            using (var dt = new DB())
            {
                var result = dt.InvoiceApplication
                               .Include(x => x.OutsourcingCompany)
                               .Include(x => x.CustomerCompnay)
                               .AsQueryable<InvoiceApplication>();
                switch (type)
                {
                    case "1"://外包公司
                        result= result.Where(m => m.OutrCompanyID == typeID);
                        #region  查询所合作的公司
                        if (companyIDS.Length > 0)
                        {
                            if (companyIDS.Length == 1)
                            {
                                string id = companyIDS[0];
                                result = result.Where(m => m.CustomerCompanyID.ToString() == id);
                            }
                            else
                            {
                                result = result.Where(m => companyIDS.Contains(m.CustomerCompanyID.ToString()));
                            }
                        }
                        #endregion
                        break;
                    case "2"://客户公司
                        result = result.Where(m => m.CustomerCompanyID == typeID);
                        #region 查询所合作的公司
                        if (companyIDS.Length > 0)
                        {
                            if (companyIDS.Length == 1)
                            {
                                string id = companyIDS[0];
                                result = result.Where(m => m.OutrCompanyID.ToString() == id);
                            }
                            else
                            {
                                result = result.Where(m => companyIDS.Contains(m.OutrCompanyID.ToString()));
                            }
                        }
                        #endregion
                        break;
                }

                //查询输入的日期段数据
                //如果没有传入查询日期将默认查询当月数据
                if (startDate != "" && endDate != "")
                {
                    DateTime endConvert = DateTime.Parse(endDate);

                    DateTime start = DateTime.Parse(string.Format("{0}-01 00:00:00", startDate));
                    DateTime end = DateTime.Parse(string.Format("{0}-{1} 23:59:59", endDate, endConvert.AddMonths(1).AddDays(-1).Day));

                    result = result.Where(m => m.OperationTime >= start && m.OperationTime <= end);

                }
                list = result.ToList<InvoiceApplication>();
            }
            return list;
        }

        public InvoiceApplication GetModel(string ID)
        {
            using (var dt = new DB())
            {
                return dt.InvoiceApplication.FirstOrDefault(m => m.ID == ID);
            }
        }
    }
}

