using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace THe_BOok_MArket.Models
{
    [MetadataType(typeof(AccoutMetaData))]
    public partial class User
    {
        //public string ConfirmPassword { get; set; }
        //public readonly object ConfirmPassword;
    }

    public class AccoutMetaData
    {
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string UserPassword { get; set; }

        public virtual User_Role User_Role { get; set; }

        //[Display(Name = "Confirm Password")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        //public string PassConfirm { get; set; }


    }


    public class UsersContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Ignore(user => user.PassConfirm);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
    }
}