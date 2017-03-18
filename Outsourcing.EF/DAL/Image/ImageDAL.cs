using Outsourcing.EF.Model.Index;
using Outsourcing.EF.Model.Recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL.Image
{
    /// <summary>
    /// 图片管理数据层
    /// </summary>
    public class ImageDAL : BaseDAL<ImageFile>
    {
        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<ImageFile> GetImageFileList(string imgName)
        {
            List<ImageFile> imgList = new List<ImageFile>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(imgName))
                {
                    int[] a = { 1, 2, 3 };
                    imgList = ct.ImageFile.Where(c => c.ImageName.Contains(imgName)).ToList();
                }
                else
                {
                    imgList = ct.ImageFile.ToList();
                }

            }
            return imgList;

        }

        public List<ImageFile> GetImage(string type)
        {
            List<ImageFile> list = new List<ImageFile>();
            try
            {
                using (var dbcontext = new DB())
                {
                    list = dbcontext.ImageFile.Where(m => m.Type == type).OrderByDescending(m => m.CreateTime).ToList();
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ImageFile GetPlatformImage(string type)
        {
            using (var dbcontext = new DB())
            {
                var id = dbcontext.ImageFile.Where(m => m.Type == type).Max(m => m.ID);
                var model = dbcontext.ImageFile.FirstOrDefault(m => m.ID == id);
                return model;
            }
        }

        public List<RequiredModel> GetRequirementList(string name)
        {
            List<RequiredModel> result = new List<RequiredModel>();
            try
            {
                using (var dbcontext = new DB())
                {
                    var list = dbcontext.Requirement.ToList();
                    if (list.Count > 0 && list.Any())
                    {
                        foreach (var item in list)
                        {
                            #region 转换为RequiredModel
                            RequiredModel model = new RequiredModel();
                            model.CompnayId = item.CompnayId;
                            model.RequirementId = item.RequirementId;
                            model.ProjectName = item.ProjectName;
                            model.CompanyName = item.ComAddress;

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

                            result.Add(model);
                            #endregion
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        result = result.Where(m => m.JobName.Contains(name)).ToList();
                    }
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PersonalInfoModel> GetPersonalInfoList(string name)
        {
            try
            {
                using (var dbcontext = new DB())
                {
                    var list = (from model in dbcontext.PersonalInfo
                                join model1 in dbcontext.OutsourcingCompany
                                on model.OutsourcingCompanyId equals model1.CompnayId.ToString()
                                select new PersonalInfoModel
                                {
                                    PersonalInfoId = model.PersonalInfoId,
                                    OutsourcingCompanyId = model.OutsourcingCompanyId,
                                    PersonName = model.PersonName,
                                    Sex = model.Sex,
                                 
                                    Education = model.Education,
                                    Publications = model.Publications,
                                    CompnayName = model1.CompnayName
                                }).ToList();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        list = list.Where(m => m.Publications.Contains(name)).ToList();
                    }
                    return list;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<PersonalInfoModel> GetPersonalPageList(string name)
        {
            try
            {
                using (var dbcontext = new DB())
                {
                    var list = (from model in dbcontext.PersonalInfo
                                join model1 in dbcontext.OutsourcingCompany
                                on model.OutsourcingCompanyId equals model1.CompnayId.ToString()
                                select new PersonalInfoModel
                                {
                                    PersonalInfoId = model.PersonalInfoId,
                                    OutsourcingCompanyId = model.OutsourcingCompanyId,
                                    PersonName = model.PersonName,
                                    Sex = model.Sex,
                                    Education = model.Education,
                                    Publications = model.Publications,
                                    CompnayName = model1.CompnayName
                                }).AsQueryable();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        list = list.Where(m => m.Publications.Contains(name));
                    }
                    return list;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PtrcInfoModel> GetPtrcInfoList(int type)
        {
            List<PtrcInfoModel> list = new List<PtrcInfoModel>();
            using (var dbcontext = new DB())
            {
                switch (type)
                {
                    case 1:
                        var result = dbcontext.PersonalInfo.Select(m => m.Publications).Distinct().ToList();
                        if (result.Count > 0 && result.Any())
                        {
                            foreach (var item in result)
                            {
                                PtrcInfoModel model = new PtrcInfoModel();
                                model.Name = item;
                                list.Add(model);
                            }
                        }
                        break;
                    case 2:
                        var result1 = dbcontext.Requirement.Select(m => m.JobName).Distinct().ToList();
                        if (result1.Count > 0 && result1.Any())
                        {
                            foreach (var item in result1)
                            {
                                PtrcInfoModel model = new PtrcInfoModel();
                                model.Name = item;
                                list.Add(model);
                            }
                        }
                        break;
                }
            }
            return list;

        }

        public List<WaitEmployer> GetWaitEmployerList(int type)
        {
            List<WaitEmployer> list = new List<WaitEmployer>();
            try
            {
                using (var dbcontext = new DB())
                {
                    switch (type)
                    {
                        case 1:
                            list = (from model in dbcontext.PersonSettlement.AsQueryable()
                                    join cus in dbcontext.CustomerCompnay.AsQueryable()
                                    on model.CustomerID equals cus.CompnayId
                                    into result
                                    from getr in result.DefaultIfEmpty()
                                    group new
                                    {
                                        model,
                                        getr
                                    }
                                    by model.CustomerID
                                 into getresult
                                    from a in getresult.DefaultIfEmpty()
                                    select new WaitEmployer
                                    {
                                        Name = a.getr.CompnayName,
                                        Money = getresult.Sum(m => m.model.RealWages)
                                    }).ToList();
                            break;
                        case 2:
                            break;
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
