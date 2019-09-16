using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THe_BOok_MArket.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class Employee
    {
    }

    public class UserMetaData
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Name required")]
        public string Employee_Name { get; set; }

        [Display(Name = "Surname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Surname required")]
        public string Employee_Surname { get; set; }

        [Display(Name = "Physical Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Physical Address required")]
        public string Employee_Address { get; set; }

        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact details required")]
        [DataType(DataType.PhoneNumber)]
        public int Emp_Phone { get; set; }

        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Valid Email address required")]
        [DataType(DataType.EmailAddress)]
        public string Emp_Email { get; set; }

        [Display(Name = "Id/Passport")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ID/Passport required")]
        public Nullable<long> ID_Number { get; set; }


        [Display(Name = "Photo")]
        public string ImageData { get; set; }

        //public HttpPostedFileBase ImageFile { get; set; }


    }

}