using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;

namespace Outsourcing.Framework.Utility
{
    /// <summary>
    /// Author      :  杨虎斌
    /// Create date : 2015.5.26
    /// Description : ExcelHelper.提供导出数据到Excel,和将Excel中的数据读取到DataTable
    /// </summary>
    public sealed class ExcelHelper
    {
        public static string ExcelConnectionString = ConfigurationManager.ConnectionStrings["ExcelConnection"].ConnectionString;

        /// <summary>
        /// 查询Excel中的数据
        /// </summary>
        /// <param name="path">Excel文件的路径</param>
        /// <param name="commandText">SQL Command</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string path, string commandText)
        {
            string connectionString = string.Format(ExcelConnectionString, path);
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand selectCommand = new OleDbCommand(commandText, connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();
                return dataTable;
            }
        }

        /// <summary>
        /// 将数据源中的数据导出到Excel,导出的数据表头按照数据库字段设置
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="downloadFilefolder">导出的文件存放文件夹</param>
        /// <returns>生成的Excel文件路径</returns>
        public static string CreateExcel(string fileName, DataTable dt, string downloadFilefolder, string sheetName)
        {
            if (!downloadFilefolder.EndsWith("\\"))
            {
                downloadFilefolder += "\\";
            }
            string newExcelPath = string.Format("{0}{1}.xls", downloadFilefolder, fileName);
            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            StringBuilder createCommandText = new StringBuilder();
            if (rows == 0 || cols == 0)
            {
                throw new ArgumentNullException("DataTable中必须有数据");
            }


            //生成连接字符串
            string connectionString = string.Format(ExcelConnectionString, newExcelPath);

            //生成创建表的脚本
            createCommandText.Append("CREATE TABLE ");
            createCommandText.Append(sheetName + " ( ");
            for (int i = 0; i < cols; i++)
            {
                //为修复bugfree中id为70的bug，更改了下面的数据类型；从varchar改为text   by yhb 2011.12.05
                String typeName = "text";
                switch (dt.Columns[i].DataType.Name)
                {
                    case "Single":
                    case "Double":
                    case "Decimal": typeName = "Decimal"; break;
                }
                if (i < cols - 1)
                {
                    createCommandText.Append(string.Format("[{0}] {1},", dt.Columns[i], typeName));
                }
                else
                {
                    createCommandText.Append(string.Format("[{0}] {1})", dt.Columns[i], typeName));
                }
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand createCommand = new OleDbCommand(createCommandText.ToString(), connection);
                createCommand.ExecuteNonQuery();
                createCommand.Dispose();

                #region 生成插入数据脚本
                StringBuilder insertCommandText = new StringBuilder();
                insertCommandText.Append("INSERT INTO ");
                insertCommandText.Append(sheetName + " ( ");
                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                    {
                        insertCommandText.Append(string.Format("[{0}],", dt.Columns[i]));
                    }
                    else
                    {
                        insertCommandText.Append("[" + dt.Columns[i] + "]) values (");
                    }
                }
                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                    {
                        insertCommandText.Append("@clm" + i + ",");
                    }
                    else
                    {
                        insertCommandText.Append("@clm" + i + ")");
                    }
                }
                #endregion

                //建立插入动作的Command
                OleDbCommand insertCommand = new OleDbCommand(insertCommandText.ToString(), connection);
                OleDbParameterCollection param = insertCommand.Parameters;
                for (int i = 0; i < cols; i++)
                {
                    switch (dt.Columns[i].DataType.Name)
                    {
                        case "Byte": param.Add(new OleDbParameter("@clm" + i, OleDbType.TinyInt)); break;
                        case "Int16": param.Add(new OleDbParameter("@clm" + i, OleDbType.SmallInt)); break;
                        case "Int32": param.Add(new OleDbParameter("@clm" + i, OleDbType.Integer)); break;
                        case "Int64": param.Add(new OleDbParameter("@clm" + i, OleDbType.BigInt)); break;
                        case "UInt16": param.Add(new OleDbParameter("@clm" + i, OleDbType.UnsignedSmallInt)); break;
                        case "UInt32": param.Add(new OleDbParameter("@clm" + i, OleDbType.UnsignedInt)); break;
                        case "UInt64": param.Add(new OleDbParameter("@clm" + i, OleDbType.UnsignedBigInt)); break;
                        case "Single": param.Add(new OleDbParameter("@clm" + i, OleDbType.Single)); break;
                        case "Double": param.Add(new OleDbParameter("@clm" + i, OleDbType.Double)); break;
                        case "Decimal": param.Add(new OleDbParameter("@clm" + i, OleDbType.Decimal)); break;
                        default: param.Add(new OleDbParameter("@clm" + i, OleDbType.LongVarChar)); break;
                    }
                }

                //遍历DataTable将数据插入新建的Excel文件中
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        param[i].Value = row[i];
                    }
                    insertCommand.ExecuteNonQuery();
                }
                insertCommand.Parameters.Clear();
                insertCommand.Dispose();
                connection.Close();
            }//end using
            return newExcelPath;
        }


        public static System.IO.StringWriter CreateExcel(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                System.IO.StringWriter sw = new StringWriter();
                StringBuilder cols = new StringBuilder();
                //添加列头
                foreach (DataColumn col in dt.Columns)
                {
                    cols.Append(col.ColumnName + '\t');
                }
                sw.WriteLine(cols);
                StringBuilder row = new StringBuilder();
                foreach (DataRow dataRow in dt.Rows)
                {
                    row = new StringBuilder();
                    foreach (object val in dataRow.ItemArray)
                    {

                        //row.Append(dataRow[0] + "\t" + dataRow[1] + "\t" + dataRow[2] + "\t" + dataRow[3] + "\t" + dataRow[4] + "\t" + dataRow[5] + "\t" + dataRow[6] + "\t" + dataRow[7] + "\t" + dataRow[8] + "\t" + dataRow[9] + "\t" + dataRow[10] + "\t" + dataRow[11] + "\t");
                        //if (val is String)
                        //    row.Append(val + "\t");
                        if (val is DBNull)
                            row.Append("" + "\t");
                        else if (val is DateTime)
                            row.Append(((DateTime)val).ToString("yyyy/MM/dd").Trim() + "\t");
                        else
                            row.Append(val.ToString().Trim().Replace("\r", "").Replace("\n", "") + "\t");  //'\'' +
                    }
                    //row.Append("\t");
                    //row.ToString().Remove(row.ToString().Length - 2, 2);
                    sw.WriteLine(row);
                }
                return sw;
            }
            return null;
        }

        //test
        //public static System.IO.StringWriter CreateExcel()
        //{
        //    System.IO.StringWriter sw = new StringWriter();
        //    sw.WriteLine("中国建设银行");
        //    sw.WriteLine("账　　号：");
        //    sw.WriteLine("开户机构：\t	北京市\t");
        //    sw.WriteLine("发生日期\t交易地点                         \t支出\t收入\t账户余额\t对方账号\t对方户名\t币种\t摘要\t");
        //    sw.WriteLine("20120301\t110665400静安庄支行营业部\t100.00\t0.00\t5,732.03\t人民币\tATM取款\t");
        //    return sw;
        //}

        /// <summary>
        /// 将数据源中的数据导出到Excel,导出的数据表头按照数据库字段设置
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="downloadFilefolder">导出的文件存放文件夹</param>
        /// <returns>生成的Excel文件路径</returns>
        public static string CreateExcel(DataTable dt, string downloadFilefolder, string sheetName)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("dt");
            }
            if (!downloadFilefolder.EndsWith("\\"))
            {
                downloadFilefolder += "\\";
            }
            string newExcelPath = Guid.NewGuid().ToString();
            return CreateExcel(newExcelPath, dt, downloadFilefolder, sheetName);
        }

        /// <summary>
        /// 将数据源中的数据导出到Excel,导出的数据表头按照数据库字段设置
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="downloadFilefolder">导出的文件存放文件夹</param>
        /// <returns>生成的Excel文件路径</returns>
        public static System.IO.StringWriter CreateExcelForWriter(DataTable dt, string downloadFilefolder, string sheetName)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("dt");
            }
            if (!downloadFilefolder.EndsWith("\\"))
            {
                downloadFilefolder += "\\";
            }
            string newExcelPath = Guid.NewGuid().ToString();
            return CreateExcel(dt);
        }

        /// <summary>
        /// 将数据源中的数据导出到Excel,导出的数据表头按照数据库字段设置
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="downloadFilefolder">导出的文件存放文件夹</param>
        /// <returns>生成的Excel文件路径</returns>
        public static System.IO.StringWriter CreateExcelForWriter(DataTable dt)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("dt");
            }
            string newExcelPath = Guid.NewGuid().ToString();
            //return CreateExcel(newExcelPath, dt, downloadFilefolder, sheetName);
            return CreateExcel(dt);
        }

        /// <summary>
        /// 拆分Datatable
        /// </summary>
        /// <param name="table">要拆分的DataTable</param>
        /// <param name="count">拆分的行数</param>
        /// <returns></returns>
        public static DataSet SplitDataTable(DataTable table, UInt16 count)
        {
            if (0 == count)
            {
                throw new ArgumentException("count 不能为0");
            }
            DataSet ds = new DataSet();
            Int32 tmpCount = count; //拆分行数
            Int32 surplusCount = table.Rows.Count;  //剩余行数
            Int32 index = 0;// 行索引
            do
            {
                DataTable cloneTable = new DataTable();
                cloneTable = table.Clone();
                if (surplusCount < count)
                {
                    tmpCount = table.Rows.Count;
                }
                for (; index < tmpCount; index++)
                {
                    DataRow row = table.Rows[index];
                    cloneTable.ImportRow(row);
                }
                tmpCount = tmpCount + count;
                surplusCount = surplusCount - count;
                cloneTable.TableName = Guid.NewGuid().ToString();
                ds.Tables.Add(cloneTable);
            } while (tmpCount < (table.Rows.Count + count));
            table.Dispose();
            GC.SuppressFinalize(table);
            return ds;
        }
    }
}