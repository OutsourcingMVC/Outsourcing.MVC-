using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.Model.Recruit
{
    public class RecruitModel
    {
        /// <summary>
        /// 简历ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 登录Id
        /// </summary>
        public int? LoginId { get; set; }
        /// <summary>
        /// 简历名称
        /// </summary>
        public string RecruitName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreteTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 简历公开程度
        /// </summary>
        public string Private { get; set; }
    }
}
