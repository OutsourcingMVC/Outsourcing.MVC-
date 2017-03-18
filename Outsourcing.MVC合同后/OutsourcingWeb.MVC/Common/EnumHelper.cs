using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OutsourcingWeb.MVC.Common
{
    public static class EnumHelper
    {
        /// <summary>
        /// 职位类型
        /// </summary>
        public enum JobTypes
        {
            [Description("JAVA")]
            Java=1,
            [Description("C/C++")]
            C = 2,
            [Description("C#/.NET")]
            NET = 3,
            [Description("HTML")]
            HTML = 4,
            [Description("PHP")]
            PHP = 5,
            [Description("WEB前端")]
            WebFrontEnd = 6,
            [Description("美工")]
            Artist = 7
        }
        
        /// <summary>
		/// 返回枚举项的描述信息
		/// </summary>
		/// <param name="value">要获取描述信息的枚举项</param>
		/// <returns>枚举想的描述信息</returns>
		public static string GetDescription(this Enum value, bool isTop = false)
        {
            Type enumType = value.GetType();
            DescriptionAttribute attr = null;
            if (isTop)
            {
                attr = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute));
            }
            else
            {
                // 获取枚举常数名称。
                string name = Enum.GetName(enumType, value);
                if (name != null)
                {
                    // 获取枚举字段。
                    FieldInfo fieldInfo = enumType.GetField(name);
                    if (fieldInfo != null)
                    {
                        // 获取描述的属性。
                        attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    }
                }
            }

            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;

        }

                ///<summary>  
        /// 获取枚举项+描述  
        ///</summary>  
        ///<param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>  
        ///<returns>键值对</returns>  
        public static Dictionary<string, string> GetEnumItemDesc(Type enumType)
        {  
            Dictionary<string, string> dic = new Dictionary<string, string>();  
            FieldInfo[] fieldinfos = enumType.GetFields();  
            foreach (FieldInfo field in fieldinfos)  
            {  
                if (field.FieldType.IsEnum)  
                {  
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);  
                    dic.Add(field.Name, ((DescriptionAttribute)objs[0]).Description);  
                }  
            }  
            return dic;  
        }

        /// <summary>
        /// 通过描述获取职位类型标识
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static int GetEnumValueByDesc(string desc)
        {
            desc = desc.ToUpper();
            Dictionary<string, string> dic = GetEnumItemDesc(typeof(JobTypes));
            KeyValuePair<string,string> kv= dic.FirstOrDefault(k => k.Value == desc);
            //获取枚举字符串的值，即将一个表示枚举成员的字符串，解析为对应枚举类型
            JobTypes time2 = (JobTypes)Enum.Parse(typeof(JobTypes), kv.Key, true);
            return (int)(time2);
                 
        }

    }
}