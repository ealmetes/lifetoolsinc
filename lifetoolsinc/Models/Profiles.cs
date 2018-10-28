using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Profiles
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Zip Code")]
        public string Zipcode { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }
        [Display(Name = "Role ID")]
        public string RoleId { get; set; }
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}