using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Outsourcing.EF.DAL
{
    /// <summary>
    /// 职位需求数据层
    /// </summary>

    
    public partial class RequirementDAL : BaseDAL<Requirement>
    {
        /// <summary>
        /// 通过职位标识获取数据内容
        /// </summary>
        /// <param name="JobCategory">职位标识</param>
        /// <returns></returns>
        public List<Requirement> GetRequirementListByJobName(int TecLanguage = 0)
        {
            List<Requirement> Requirements = new List<Requirement>();
            using (var ct = new DB())
            {
                if (TecLanguage != 0)
                {
                    
                    Requirements = ct.Requirement.Where(c => c.TecLanguage == TecLanguage)
                                                   .ToList();
                    //Requirements = ct.Requirement.Where(c => c.JobName.Contains(jobName)).ToList();
                    //int k = 0;
                    //foreach (var model in ct.Requirement)
                    //{
                    //    if (model.JobName.Contains(jobName))
                    //    {
                    //        k++;
                    //    } 
                    //}
                }
                else
                {
                    Requirements = ct.Requirement.ToList();
                }
                return Requirements;
            }
        }

        /// <summary>
        /// 根据where条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Requirement> GetRequirementList(string ProjectName)
        {
            List<Requirement> Requirements = new List<Requirement>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(ProjectName))
                {
                    Requirements = ct.Requirement.Where(c => c.ProjectName.IndexOf(ProjectName) != -1
                ).ToList();
                }
                else {
                    Requirements = ct.Requirement.ToList();
                }
                return Requirements;
            }

        }
        //根据职位类别JobCategory获取招聘信息列表
        public List<Requirement> GetPersonList(string strWhere)
        {
            List<Requirement> requirement = new List<Requirement>();
            using (var ct = new DB())
            {
                string sql = "select a.CompnayName AS Name,b.* from CustomerCompnay AS a JOIN Requirement AS b ON a.CompnayId=b.CompnayId WHERE  ";
                if (!string.IsNullOrEmpty(strWhere))
                {
                    sql += strWhere;
                }
                requirement = ct.Requirement.SqlQuery(sql).ToList();
                return requirement;
            }
        }
        /// <summary>
        /// 根据id条件获Requirement实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Requirement GetRequirement(int Requirementid)
        {
            using (var ct = new DB())
            {
                var Requirement = ct.Requirement.Where(c => c.RequirementId == Requirementid).FirstOrDefault();
                return Requirement;
            }
        }
        //根据id条件获Requirement实体
        public List<Requirement> GetrequirementList(int RequirementId)
        {
            List<Requirement> requirement = new List<Requirement>();
            using (var ct = new DB())
            {
                string sql = "select * from Requirement WHERE RequirementId = '" + RequirementId + "'";
                requirement = ct.Requirement.Include(x => x.PushInterViewTable).Where(m => m.RequirementId == RequirementId).ToList();
                return requirement;
            }
        }
    }
}
