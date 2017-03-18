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
    /// 申请详情的数据层
    /// </summary>
    public class InvoiceDetailDAL : BaseDAL<InvoiceDetail>
    {
        /// <summary>
        /// 返回数据根据标识
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>JSON格式数据</returns>
        public string GetModelListJSON(string ID)
        {
            string resultStr = string.Empty;
            using (var dt = new DB())
            {
                var model = dt.InvoiceDetail.FirstOrDefault(m => m.InvoiceID == ID);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                resultStr = jss.Serialize(model);
            }

            return resultStr;
        }

        /// <summary>
        /// 返回数据根据标识
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>返回实例</returns>
        public InvoiceDetail GetModel(string ID)
        {
            using (var dt = new DB())
            {
                return  dt.InvoiceDetail.FirstOrDefault(m => m.InvoiceID == ID);
            }

            
        }
    }
}
