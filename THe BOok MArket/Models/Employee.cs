//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THe_BOok_MArket.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Sales = new HashSet<Sale>();
        }
    
        public int Employee_ID { get; set; }
        public Nullable<int> User_ID { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_Surname { get; set; }
        public string Employee_Address { get; set; }
        public Nullable<int> Emp_Phone { get; set; }
        public string Emp_Email { get; set; }
        public Nullable<long> ID_Number { get; set; }
        public Nullable<int> EmpTitle_ID { get; set; }
        public Nullable<int> EmpGender_ID { get; set; }
        public string ImageData { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public virtual Employee_Gender Employee_Gender { get; set; }
        public virtual Employee_Title Employee_Title { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
