using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THe_BOok_MArket.Models
{
    [MetadataType(typeof(RoleMetaData))]
    public partial class User_Role
    {
    }

    public class RoleMetaData
    {
        [Display(Name = "User Role Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Name required")]
        public string UserRole_Description { get; set; }
    }
}