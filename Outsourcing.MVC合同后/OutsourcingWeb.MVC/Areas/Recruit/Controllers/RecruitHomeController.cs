using Outsourcing.EF.DAL.Image;
using Outsourcing.EF.DAL.Recruit;
using Outsourcing.EF.Model.Recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Outsourcing.EF.Model.Index;
using Outsourcing.EF;
using Aspose.Words;
using Aspose.Words.Drawing;
using System.IO;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using System.Drawing.Printing;

namespace OutsourcingWeb.MVC.Areas.Recruit.Controllers
{
 
 
    public class RecruitHomeController : Controller
    {
        #region  DAL操作对象
        RecuritHomeDAL dal = new RecuritHomeDAL();
        ImageDAL imagedal = new ImageDAL();
        #endregion
        // GET: Recruit/RecruitHome
        public ActionResult PersonalInfo(string id,string loginid)
        {
            PersonalShowModel model = dal.GetPersonalModel(Convert.ToInt32(id));
            ViewBag.LoginID = loginid;
            return View(model);
        }

        /// <summary>
        /// 打印简历
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintWord(int id)
        {
            try
            {
                PersonalShowModel model = dal.GetPersonalModel(Convert.ToInt32(id));
                string templateFile = Server.MapPath("~/Text1.dot");
                string saveDocFile = Server.MapPath("~/Text1.docx");
                //载入模板
                Document doc = new Document(templateFile);
              
                //载入数据
                String[] fieldNames = new String[] { "PersonName", "Sex", "Age", "Education", "Job", "Remark", "WorkLife", "ProjectLife", "EducationLife", "WorkDay", "Books", "InSchoolLife", "Language", "ProjectPower", "Honor", "TrainLife", "Hobby", "JobWanted" };
                Object[] fieldValues = new Object[] { model.PersonName, model.Sex, model.PersonalInfoId, model.Education, model.Publications, model.Remark, model.Resume, "略", model.Educations, model.OutsourcingCompanyId + "年", "帝都本科 软件工程专业 4年", "多姿多彩美好生活", "英语 汉语", "悦动圈项目", "计算机操作语言之父", "火星时代培训机构", "打篮球，网游", "期望薪资" };
                //合并模版，相当于页面的渲染
                doc.MailMerge.Execute(fieldNames, fieldValues);
                doc.Print();
            }
            catch (Exception)
            {

                throw;
            }
            return View("/RecruitHome/Recruit/PersonalInfo");

        }
        /// <summary>
        /// 导出简历
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportWord(int id)
        {
            try
            {
                //获取数据
                PersonalShowModel model = dal.GetPersonalModel(Convert.ToInt32(id));
               //获取模板
                string templateFile = Server.MapPath("~/Text1.dot");
                string saveDocFile = Server.MapPath("~/Text1.docx");
                //载入模板
                Document doc = new Document(templateFile);
                //载入数据
                String[] fieldNames = new String[] { "PersonName", "Sex", "Age", "Education", "Job", "Remark", "WorkLife", "ProjectLife", "EducationLife","WorkDay", "Books", "InSchoolLife", "Language", "ProjectPower", "Honor", "TrainLife", "Hobby","JobWanted" };
                Object[] fieldValues = new Object[] { model.PersonName, model.Sex, model.PersonalInfoId, model.Education, model.Publications, model.Remark, model.Resume, "略", model.Educations ,model.OutsourcingCompanyId+"年","帝都本科 软件工程专业 4年","多姿多彩美好生活","英语 汉语","悦动圈项目","计算机操作语言之父","火星时代培训机构", "打篮球，网游","期望薪资" };
                //合并模版，相当于页面的渲染
                doc.MailMerge.Execute(fieldNames, fieldValues);
                //保存合并后的文档
                doc.Save(saveDocFile);
                //保存导出
                var docStream = new MemoryStream();
                //导出数据
                doc.Save(docStream, SaveOptions.CreateSaveOptions(SaveFormat.Doc));
                return base.File(docStream.ToArray(), "application/msword", "Template.doc");

               
            }
            catch (Exception ex)
            {

                ViewBag.msg = ex.Message;
            }
           
            return View("/RecruitHome/Recruit/PersonalInfo");
            
        }

        public ActionResult RequireInfo(string id, string loginid)
        {
            RequiredModel model = dal.GetRequiredModel(Convert.ToInt32(id));
            ViewBag.LoginID = loginid;
            return View(model);
        }

        public ActionResult GetDateByType(string name,int page=1)
        {
            IndexShow model = new IndexShow();

            if (Session["types"] != null)
            {
                string type = Session["types"].ToString();
                if (type == "1")
                {
                    ViewBag.Name = name;
                    var list = imagedal.GetRequirementList(name).OrderByDescending(m => m.PublishTime).ToPagedList(page, 5);
                    return View("GetRequireInfoPage", list);
                }
                else
                {
                    ViewBag.Name = name;
                    var list = imagedal.GetPersonalInfoList(name).OrderByDescending(m => m.CreateTime).ToPagedList(page, 5);

                    return View("GetPersonalInfoPage", list);
                }
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult GetImageFile(int id, string type)
        {
            ImageFile model = new ImageFile();

            var list = imagedal.GetImage(type);

            if (id <= list.Count && list.Count > 0 && list.Any())
            {
                if (list[id - 1] != null)
                {
                    model = list[id - 1];
                    ViewBag.ID = id;
                    ViewBag.Type = type;
                }
                
            }
            return View(model);
        }
    }
}