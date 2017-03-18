using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.Model.Recruit
{
    public class MySelfModel
    {
        /// <summary>
        /// 简历ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 简历名称
        /// </summary>
        [Required(ErrorMessage ="简历名称不能为空")]
        public string RecruitName { get; set; }
        /// <summary>
        /// 参加工作年份
        /// </summary>
        public string AttendYear { get; set; }
        /// <summary>
        /// 户口所在地
        /// </summary>
        public string RegisteredPerson { get; set; }
        /// <summary>
        ///现居地
        /// </summary>
        public string NowAdress { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string IfMarried { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// 证件形式
        /// </summary>
        public int? CredentialsStatus { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CredentialsNumber { get; set; }
        /// <summary>
        /// 是否有海外工作经验
        /// </summary>
        public string OverseasStudent { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticsStatus { get; set; }
        /// <summary>
        /// 隐私设置
        /// </summary>
        public string Private { get; set; }
        /// <summary>
        /// 简历更新时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 简历创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 简历人名称
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string UserName { get; set; }
        /// <summary>
        /// 简历人性别
        /// </summary>
        public string UserSex { get; set; }
        /// <summary>
        /// 简历人出生日期
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string TelPhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 登录ID
        /// </summary>
        public int? LoginId { get; set; }
    }
}
