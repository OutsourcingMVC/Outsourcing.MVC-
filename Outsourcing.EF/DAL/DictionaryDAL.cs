using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    public class DictionaryDAL : BaseDAL<Dictionary>
    {/// <summary>
     /// 根据条件获取列表
     /// </summary>
     /// <param name="where"></param>
     /// <returns></returns>
        public List<Dictionary> GetDictionaryList(string Dictionaryname)
        {
            List<Dictionary> Dictionarys = new List<Dictionary>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(Dictionaryname))
                {
                    int[] a = { 1, 2, 3 };
                    Dictionarys = ct.Dictionary.Where(c => c.ClassName.IndexOf(Dictionaryname) != -1
                ).ToList();
                }
                else {
                    Dictionarys = ct.Dictionary.ToList();
                }
                return Dictionarys;
            }

        }
        /// <summary>
        /// 根据id条件获Dictionary实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Dictionary GetDictionary(int Dictionaryid)
        {
            using (var ct = new DB())
            {
                var Dictionary = ct.Dictionary.Where(c => c.DictionaryID == Dictionaryid).FirstOrDefault();
                return Dictionary;
            }
        }
    }
}
