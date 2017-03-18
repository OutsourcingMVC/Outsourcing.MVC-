using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace Outsourcing.MVC.Common
{
    /// <summary>
    /// 定义json格式转换器 by yanghb 2016/1/26 add
    /// </summary>
    public static class CommonJsonConverter
    {
        /// <summary>
        /// 将json字符串转换为List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(this string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }

        /// <summary>
        ///  将json字符串转换为T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        //序列化的方法很多，下面是参考之一。另外如果在MVC项目中，还可以通过返回Json()类型的ActionResult实现Json序列化
        //        public string GetJsonString()
        //{    
        //   List<Product> products = new List<Product>(){
        //   new Product(){Name="苹果",Price=5.5},
        //   new Product(){Name="橘子",Price=2.5},
        //   new Product(){Name="干柿子",Price=16.00}
        //   };    
        //   ProductList productlist = new ProductList();    
        //   productlist.GetProducts = products;    
        //    return new JavaScriptSerializer().Serialize(productlist));    
        //}

    }
}
