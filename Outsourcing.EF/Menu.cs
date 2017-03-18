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
    
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            this.Role = new HashSet<Role>();
        }
    
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public Nullable<int> PID { get; set; }
        public string Url { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<int> Invalid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Role { get; set; }
    }
}