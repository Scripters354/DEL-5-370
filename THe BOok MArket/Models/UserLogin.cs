using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THe_BOok_MArket.Models
{
    public class UserLogin
    {
        [Display(Name = "Enter Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Display(Name = "Enter Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}