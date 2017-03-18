//=====================================================================================
// All Rights Reserved , Copyright © YJY 2015
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Framework.Utility
{
    public static class StringExtension
    {
        /// <summary>
        /// string转换为float
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string s, float defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrWhiteSpace(s))
            {
                return defValue;
            }

            try
            {
                return Convert.ToSingle(s);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// string转换为int
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int ToInt(this string s, int defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrWhiteSpace(s))
            {
                return defValue;
            }

            try
            {
                return Convert.ToInt32(s);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// string转换为decimal
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s, decimal defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrWhiteSpace(s))
            {
                return defValue;
            }
            try
            {
                return Convert.ToDecimal(s);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// string转换为DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, DateTime defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrWhiteSpace(s))
            {
                return defValue;
            }
            try
            {
                return Convert.ToDateTime(s);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// string转换为DateTime类型的string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static string ToDateTime(this string s, string defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrWhiteSpace(s))
            {
                return defValue;
            }
            try
            {
                return Convert.ToDateTime(s).ToString("yyyy-MM-dd");
            }
            catch
            {
                return defValue;
            }
        }
    }
}
