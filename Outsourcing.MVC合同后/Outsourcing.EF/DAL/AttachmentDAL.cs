using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF.DAL
{
    public class AttachmentDAL : BaseDAL<Attachment>
    {
        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Attachment> GetAttachmentList(string Attachmentname)
        {
            List<Attachment> Attachments = new List<Attachment>();
            using (var ct = new DB())
            {
                if (!string.IsNullOrEmpty(Attachmentname))
                {
                    int[] a = { 1, 2, 3 };
                    Attachments = ct.Attachment.Where(c => c.AttachmentName.IndexOf(Attachmentname) != -1
                ).ToList();
                }
                else {
                    Attachments = ct.Attachment.ToList();
                }
                return Attachments;
            }

        }
        /// <summary>
        /// 根据id条件获Menu实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Attachment GetAttachment(int Attachmentid)
        {
            using (var ct = new DB())
            {
                var Attachment = ct.Attachment.Where(c => c.AttachmentId == Attachmentid).FirstOrDefault();
                return Attachment;
            }
        }
    }
}
