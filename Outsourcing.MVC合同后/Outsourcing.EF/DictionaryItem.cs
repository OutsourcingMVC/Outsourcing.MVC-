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
    
    public partial class DictionaryItem
    {
        public int DictionaryItemID { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> DictionaryID { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
