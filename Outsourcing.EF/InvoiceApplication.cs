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
    
    public partial class InvoiceApplication
    {
        public string ID { get; set; }
        public int CustomerCompanyID { get; set; }
        public int OutrCompanyID { get; set; }
        public Nullable<int> SettlementYear { get; set; }
        public Nullable<int> SettlementMonth { get; set; }
        public Nullable<double> SettlementMoney { get; set; }
        public Nullable<int> InvoiceOutState { get; set; }
        public Nullable<int> InvoiceCustomerState { get; set; }
        public string OperationUser { get; set; }
        public Nullable<System.DateTime> OperationTime { get; set; }
    
        public virtual CustomerCompnay CustomerCompnay { get; set; }
        public virtual OutsourcingCompany OutsourcingCompany { get; set; }
    }
}