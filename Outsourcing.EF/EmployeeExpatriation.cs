//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Outsourcing.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeExpatriation
    {
        public string ID { get; set; }
        public Nullable<int> FirstPartyID { get; set; }
        public Nullable<int> SecondPartyID { get; set; }
        public string FirstContent { get; set; }
        public string SecondContent { get; set; }
        public string PersonList { get; set; }
        public Nullable<int> FirstPartyStatus { get; set; }
        public Nullable<int> SecondPartyStatus { get; set; }
        public Nullable<int> FirstPartEffectiveStatus { get; set; }
        public Nullable<int> SecondPartyEffectiveStatus { get; set; }
        public Nullable<int> ContractStatus { get; set; }
        public Nullable<int> ReviewUserID { get; set; }
        public Nullable<System.DateTime> ReviewTime { get; set; }
        public string ReviewContract { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> EffectiveTime { get; set; }
        public Nullable<int> FirstOrSecondDownload { get; set; }
        public string UploadFilePath { get; set; }
        public string ContactFileFul { get; set; }
    
        public virtual CustomerCompnay CustomerCompnay { get; set; }
        public virtual OutsourcingCompany OutsourcingCompany { get; set; }
    }
}