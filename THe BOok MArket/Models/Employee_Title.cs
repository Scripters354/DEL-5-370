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
    
    public partial class Employee_Title
    {
        public Employee_Title()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int EmpTitle_ID { get; set; }
        public string Title_Description { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
