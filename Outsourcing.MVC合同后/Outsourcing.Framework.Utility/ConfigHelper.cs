//=====================================================================================
// All Rights Reserved , Copyright © YJY 2015
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using System.IO;
using System.Configuration;
using Aspose.Words;
namespace Outsourcing.Framework.Utility
{
    /// <summary>
    ///  Config 公共帮助类
    /// 版本：2.0
    /// <author>
    ///		<name>YJY</name>
    ///		<date>2013.09.27</date>
    /// </author>
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }

        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(string key, string value)
        {
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("/XmlConfig/Config.xml"));
            System.Xml.XmlNode xNode;
            System.Xml.XmlElement xElem1;
            System.Xml.XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");

            xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", value);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(HttpContext.Current.Server.MapPath("/XmlConfig/Config.xml"));
        }
    }
}
