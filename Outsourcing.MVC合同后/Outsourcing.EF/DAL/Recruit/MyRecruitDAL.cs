using Outsourcing.EF.Model.Recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL.Recruit
{
    /// <summary>
    /// 我的招聘数据层
    /// </summary>
    public class MyRecruitDAL
    {
        /// <summary>
        /// 获取菜单项
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public List<Menu> GetMenuList(int types)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                using (DB dbContext = new DB())
                {
                    list = dbContext.Menu.Where(m => m.OrderNumber == types || m.OrderNumber == 3).ToList();
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取简历列表
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public List<RecruitModel> GetRecruitList(int loginId)
        {
            try
            {
                List<RecruitModel> list = new List<RecruitModel>();
                using (DB dbContext = new DB())
                {
                    list = (from m in dbContext.MyRecruit
                            select new RecruitModel
                            {
                                ID = m.ID,
                                LoginId = m.LoginId,
                                CreteTime = m.CreateTime,
                                UpdateTime = m.CreateTime,
                                RecruitName = m.RecruitName,
                                //简历公开程度
                                Private = m.Private
                            }).ToList();
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MySelfModel GetMySelfInfo(int loginid, int id)
        {
            MySelfModel model = new MySelfModel();
            try
            {
                using (DB dbcontext = new DB())
                {
                    var recruit = dbcontext.MyRecruit.FirstOrDefault(m => m.LoginId == loginid && m.ID == id);
                    if (recruit != null)
                    {
                        model.ID = recruit.ID;
                        model.RecruitName = recruit.RecruitName;
                        model.AttendYear = recruit.AttendYear;
                        model.RegisteredPerson = recruit.RegisteredPerson;
                        model.NowAdress = recruit.NowAdress;
                        model.IfMarried = recruit.IfMarried;
                        model.Nationality = recruit.Nationality;
                        model.CredentialsStatus = recruit.CredentialsStatus;
                        model.CredentialsNumber = recruit.CredentialsNumber;
                        model.OverseasStudent = recruit.OverseasStudent;
                        model.PoliticsStatus = recruit.PoliticsStatus;
                        model.Private = recruit.Private;
                        model.UpdateTime = recruit.UpdateTime;
                        model.CreateTime = recruit.CreateTime;
                        model.UserName = recruit.UserName;
                        model.UserSex = recruit.UserSex;
                        model.Birthday = recruit.Birthday;
                        model.TelPhone = recruit.TelPhone;
                        model.Mail = recruit.Mail;
                        model.LoginId = recruit.LoginId;
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
