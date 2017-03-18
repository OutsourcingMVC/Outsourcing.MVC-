using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Framework.Utility
{
    /// <summary>
    /// 对象转换
    /// </summary>
    public class DataTableToObjects
    {
        /// <summary>
        /// 转换DataTable对象到Json
        /// </summary>
        /// <param name="sEcho"></param>
        /// <param name="totalRow"></param>
        /// <param name="dt"></param>
        /// <param name="dt_dispose"></param>
        /// <returns></returns>
        public static string DataTableToJson(int sEcho, int totalRow, DataTable dt, bool dt_dispose)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{\"sEcho\":" + sEcho.ToString() + ",");
            json.Append("\"iTotalRecords\":" + totalRow.ToString() + ",");
            json.Append("\"iTotalDisplayRecords\":" + totalRow.ToString() + ",");
            json.Append("\"aaData\":[");
            //json.AppendFormat("{\"sEcho\":{0},\n \"iTotalRecords\":{1},\n \"iTotalDisplayRecords\": {2},\n \"aaData\":[", sEcho, totalRow, totalRow);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    json.Append("\"");
                    json.Append(dt.Columns[j].ColumnName);
                    json.Append("\":\"");
                    json.Append(dt.Rows[i][j].ToString());
                    json.Append("\",");
                }
                json.Remove(json.Length - 1, 1);
                json.Append("},");
            }
            json.Remove(json.Length - 1, 1);
            json.Append("]}");

            return json.ToString();
        }

    }
}
