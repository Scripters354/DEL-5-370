//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THe_BOok_MArket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int User_ID { get; set; }
        public Nullable<int> UserRole_ID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string GUID { get; set; }
        public Nullable<System.DateTime> GUIDExpiry { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual User_Role User_Role { get; set; }
    }
}
