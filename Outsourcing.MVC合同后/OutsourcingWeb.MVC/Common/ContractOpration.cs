using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace OutsourcingWeb.MVC.Common
{
    /// <summary>
    /// 通过合同操作项
    /// </summary>
    public class ContractOpration
    {
       /// <summary>
       /// 获取派遣协议模板内容
       /// </summary>
       /// <returns></returns>
        public static string GetDispatchingAgreement()
        {
            string TemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"Common\DispatchingAgreement.txt";
            string strContent = string.Empty;
            if (System.IO.File.Exists(TemplatePath))
            {
                using (TextReader tr = new StreamReader(TemplatePath, Encoding.Default))
                {
                    strContent = tr.ReadToEnd();
                }
            }
            return strContent;

        }

        /// <summary>
        /// 获取人员派遣协议模板上部分内容
        /// </summary>
        /// <returns></returns>
        public static string GetEmployeeExpatriationTop()
        {
            string TemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"Common\EmployeeExpatriationTop.txt";
            string strContent = string.Empty;
            if (System.IO.File.Exists(TemplatePath))
            {
                using (TextReader tr = new StreamReader(TemplatePath, Encoding.Default))
                {
                    strContent = tr.ReadToEnd();
                }
            }
            return strContent;
        }

        /// <summary>
        /// 获取人员派遣协议模板下部分内容
        /// </summary>
        /// <returns></returns>
        public static string GetEmployeeExpatriationBottom()
        {
            string TemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"Common\EmployeeExpatriationBottom.txt";
            string strContent = string.Empty;
            if (System.IO.File.Exists(TemplatePath))
            {
                using (TextReader tr = new StreamReader(TemplatePath, Encoding.Default))
                {
                    strContent = tr.ReadToEnd();
                }
            }
            return strContent;
        }

        /// <summary>
        /// 获取人员派遣单行数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Job"></param>
        /// <param name="Level"></param>
        /// <param name="ArrivalTime"></param>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static string GetEmployeeExpatriationCenter(string name, string Job, string Level, string ArrivalTime, string Money)
        {
            StringBuilder strContent = new StringBuilder();
            //替换姓名
            strContent.Append("<tr style = \"height: 26px;\" >");
            strContent.Append("<td width = \"64\" style = \"border-width: 0px 1px 2px 2px; border-style: none solid double double; border-color: rgb(0, 0, 0) windowtext windowtext; padding: 0px 7px; width: 64px; height: 26px; background-color: transparent;\" >");
            strContent.Append("<p style = \"line -height: 150%;\" >");
            strContent.AppendFormat("<span><span style = \"undefinedfont -family:宋体\" >{0}</span></span>", name);
            strContent.Append("</p></td>");
            //替换工作岗位
            strContent.Append("<td width = \"102\" valign = \"top\" style = \"border-width: 0px 1px 2px 0px; border-style: none solid double none; border-color: rgb(0, 0, 0) windowtext windowtext rgb(0, 0, 0); padding: 0px 7px; width: 102px; height: 26px; background-color: transparent;\">");
            strContent.Append("<p style = \"text -align: center; line-height: 150%;\" >");
            strContent.AppendFormat("<span style = \"line-height: 150%; font-size: 16px;font-family:宋体\" >{0}</span>", Job);
            strContent.Append("</p></td>");
            //替换级别
            strContent.Append("<td width = \"134\" valign = \"top\" style = \"border-width: 0px 1px 2px 0px; border-style: none solid double none; border-color: rgb(0, 0, 0) windowtext windowtext rgb(0, 0, 0); padding: 0px 7px; width: 134px; height: 26px; background-color: transparent;\" >");
            strContent.Append("<p style = \"text-align: center; line-height: 150%;\" >");
            strContent.AppendFormat("<span style = \"undefinedfont-family:宋体\" >{0}</span>", Level);
            strContent.Append("</p></td>");
            //替换到岗时间
            strContent.Append("<td width = \"139\" style = \"border-width: 0px 1px 2px 0px; border-style: none solid double none; border-color: rgb(0, 0, 0) windowtext windowtext rgb(0, 0, 0); padding: 0px 7px; width: 139px; height: 26px; background-color: transparent;\" >");
            strContent.Append("<p style = \"text-align: center; line-height: 150%;\" >");
            strContent.AppendFormat("<span style = \"undefinedfont-family:宋体\" >{0}</span>", ArrivalTime);
            strContent.Append("</p></td>");
            //替换人员结算单价
            strContent.Append("<td width = \"165\" style = \"border-width: 0px 2px 2px 0px; border-style: none double double none; border-color: rgb(0, 0, 0) windowtext windowtext rgb(0, 0, 0); padding: 0px 7px; width: 165px; height: 26px; background-color: transparent;\" >");
            strContent.Append("<p style = \"text -align: center; line-height: 150%;\" >");
            strContent.AppendFormat("<span style = \"undefinedfont -family:宋体\" >{0}</span>", Money);
            strContent.Append("</p></td></tr>");

            return strContent.ToString();
        }
    }
}