using Outsourcing.EF.Model.Recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL.Recruit
{
    /// <summary>
    /// 
    /// </summary>
    public class RecuritHomeDAL
    {
        public PersonalShowModel GetPersonalModel(int id)
        {
            PersonalShowModel model = new PersonalShowModel();
            try
            {
                using (var dbcontext = new DB())
                {
                    var result = dbcontext.PersonalInfo.FirstOrDefault(m => m.PersonalInfoId == id);
                    if (result != null)
                    {
                        model.PersonalInfoId = result.PersonalInfoId;
                        model.OutsourcingCompanyId = result.OutsourcingCompanyId;
                        model.PersonName = result.PersonName;
                        model.Remark = result.Remark;
                        model.Resume = result.Resume;
                        model.Educations = result.Attachment;
                        model.Sex = result.Sex;
                       
                        model.Birthday = result.Birthday;
                        model.Education = result.Education;
                        model.Email = result.Email;
                        model.Telphone = result.Telephone;
                        model.GraduationSchool = result.GraduationSchool;
                        model.GraduationDate = result.GraduationDate;
                        model.Publications = result.Publications;

                        model.CreateTime = result.CreateTime;
                        model.UpdateTime = result.UpdateTime;

                        if (!string.IsNullOrWhiteSpace(model.OutsourcingCompanyId ))
                        {
                            int companyID = Convert.ToInt32(model.OutsourcingCompanyId);
                            var resultModle = dbcontext.OutsourcingCompany.FirstOrDefault(m => m.CompnayId == companyID);
                            if (resultModle != null)
                            {
                                model.CompnayName = resultModle.CompnayName;
                                model.EnglishName = resultModle.EnglishName;
                                model.LegalRepresentative = resultModle.LegalRepresentative;

                                model.LegalTelephone = resultModle.LegalTelephone;
                                model.UnitResponsible = resultModle.UnitResponsible;
                                model.ResponsibleTelephone = resultModle.ResponsibleTelephone;

                                model.Address = resultModle.Address;
                                model.EnterpriseNum = resultModle.EnterpriseNum ;
                                model.Descriptions = resultModle.Descriptions;

                                model.Nature = resultModle.Nature;
                                model.BusinessScope = resultModle.BusinessScope;
                            }
                        }

                    }
                }
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequiredModel GetRequiredModel(int id)
        {
            RequiredModel model = new RequiredModel();
            try
            {
                using (var dbcontext = new DB())
                {
                    var item = dbcontext.Requirement.FirstOrDefault(m => m.RequirementId == id);
                    if (item != null)
                    {
                        model.CompnayId = item.CompnayId;
                        var compnay = dbcontext.CustomerCompnay.FirstOrDefault(m => m.CompnayId == model.CompnayId);
                        if (compnay != null)
                        {
                            model.CompanyName = compnay.CompnayName;
                        }
                        model.RequirementId = item.RequirementId;
                        model.ProjectName = item.ProjectName;

                        if (item.ArrivalTime.ToString() != "" && item.ArrivalTime.ToString() != null)
                            model.ArrivalTime = DateTime.Parse(item.ArrivalTime.ToString()).ToString("yyyy-MM-dd");
                        else
                            model.ArrivalTime = "";

                        model.JobName = item.JobName;

                        model.Salary = item.Salary;
                        if (item.Salary > 0 && item.Salary.ToString() != "")
                        {
                            switch (item.Salary)
                            {
                                case 1:
                                    model.SalaryName = "4000";
                                    break;
                                case 2:
                                    model.SalaryName = "4000元-6000元";
                                    break;
                                case 3:
                                    model.SalaryName = "6000元-8000元";
                                    break;
                                case 4:
                                    model.SalaryName = "8000元-10000元";
                                    break;
                                case 5:
                                    model.SalaryName = "10000元以上";
                                    break;
                                case 6:
                                    model.SalaryName = "面议";
                                    break;
                            }
                        }
                        else
                        {
                            model.SalaryName = "面议";
                        }

                        if (item.PublishTime.ToString() != "" && item.PublishTime.ToString() != null)
                            model.PublishTime = DateTime.Parse(item.PublishTime.ToString()).ToString("yyyy-MM-dd");
                        else
                            model.PublishTime = "";

                        model.ProjectAddress = item.ProjectAddress;

                        model.Property = item.Property;
                        if (item.Property > 0 && item.Property.ToString() != "")
                        {
                            switch (item.Property)
                            {
                                case 1:
                                    model.PropertyName = "不限";
                                    break;
                                case 2:
                                    model.PropertyName = "全职";
                                    break;
                                case 3:
                                    model.PropertyName = "兼职";
                                    break;
                                case 4:
                                    model.PropertyName = "实习";
                                    break;
                            }
                        }
                        else
                        {
                            model.PropertyName = "不限";
                        }
                        model.Experience = item.Experience;
                        if (item.Experience > 0 && item.Experience.ToString() != "")
                        {
                            switch (item.Experience)
                            {
                                case 1:
                                    model.ExperienceName = "不限";
                                    break;
                                case 2:
                                    model.ExperienceName = "无经验";
                                    break;
                                case 3:
                                    model.ExperienceName = "1年以下";
                                    break;
                                case 4:
                                    model.ExperienceName = "1-3年";
                                    break;
                                case 5:
                                    model.ExperienceName = "3-5年";
                                    break;
                                case 6:
                                    model.ExperienceName = "5-10年";
                                    break;
                                case 7:
                                    model.ExperienceName = "10年以上";
                                    break;
                            }
                        }
                        else
                        {
                            model.ExperienceName = "不限";
                        }
                        model.Education = item.Education;
                        if (item.Education > 0 && item.Education.ToString() != "")
                        {
                            switch (item.Education)
                            {
                                case 1:
                                    model.EducationName = "不限";
                                    break;
                                case 2:
                                    model.EducationName = "无学历要求";
                                    break;
                                case 3:
                                    model.EducationName = "高中/中转/中技及以下";
                                    break;
                                case 4:
                                    model.EducationName = "大专及等同学历";
                                    break;
                                case 5:
                                    model.EducationName = "本科学历";
                                    break;
                                case 6:
                                    model.EducationName = "硕士学历";
                                    break;
                                case 7:
                                    model.EducationName = "博士及以上";
                                    break;
                                case 8:
                                    model.EducationName = "其他";
                                    break;
                            }
                        }
                        else
                        {
                            model.EducationName = "不限";
                        }

                        model.RecNumber = item.RecNumber;
                        model.JobDescription = item.JobDescription;
                        model.ComProfile = item.ComProfile;

                        model.JobCategory = item.JobCategory;
                        if (item.JobCategory > 0 && item.JobCategory.ToString() != "")
                        {
                            switch (item.JobCategory)
                            {
                                case 1:
                                    model.JobCateGoryName = "web应用开发";
                                    break;
                                case 2:
                                    model.JobCateGoryName = "移动应用开发";
                                    break;
                                case 3:
                                    model.JobCateGoryName = "微信开发";
                                    break;
                                case 4:
                                    model.JobCateGoryName = "应用桌面开发";
                                    break;
                                case 5:
                                    model.JobCateGoryName = "系统分析设计";
                                    break;
                                case 6:
                                    model.JobCateGoryName = "数据分析设计";
                                    break;
                            }
                        }
                        else
                        {
                            model.JobCateGoryName = "其他";
                        }
                        model.ComAddress = item.ComAddress;
                        model.TecLanguage = item.TecLanguage;
                        if (item.TecLanguage > 0 && item.TecLanguage.ToString() != "")
                        {
                            switch (item.TecLanguage)
                            {
                                case 1:
                                    model.TecLanguageName = "Java";
                                    break;
                                case 2:
                                    model.TecLanguageName = "C/C++";
                                    break;
                                case 3:
                                    model.TecLanguageName = "C#/Asp.Net";
                                    break;
                                case 4:
                                    model.TecLanguageName = "HTML";
                                    break;
                                case 5:
                                    model.TecLanguageName = "PHP";
                                    break;
                            }
                        }
                        else
                        {
                            model.TecLanguageName = "其他";
                        }
                        model.Remark = item.Remark;
                        model.CreateUser = item.CreateUser;
                        model.CreateTime = item.CreateTime;

                        model.UpdateUser = item.UpdateUser;
                        model.UpdateTime = item.UpdateTime;
                    }
                }
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
