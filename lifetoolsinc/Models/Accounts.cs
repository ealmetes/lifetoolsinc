using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Accounts
    {
        [Key]
        public string key { get; set; }
        public int statusid { get; set; }
        public string candidate { get; set; }
        //[Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\(\d{3}\)\s{0,1}\d{3}-\d{4}$", ErrorMessage = "Enter a valid number (888) 888-88888")]
        public string phone { get; set; }
        public string businessid { get; set; }
        //[Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateassigned { get; set; }
        public DateTime? dateexecuted { get; set; }
        public Decimal? unit { get; set; }
        public int promocodeid { get; set; }
        public string type { get; set; }
        public int page;
        public IEnumerable<lifetoolsinc.Models.AttPages> pages;
        public IEnumerable<lifetoolsinc.Models.Attributes> choices;
        public IEnumerable<lifetoolsinc.Models.AttClasses> classes;
        public IEnumerable<lifetoolsinc.Models.Contents> contents;
        public IEnumerable<lifetoolsinc.Models.CandidatesResults> results;
        public static List<Accounts> attributes = new List<Accounts>();


    }
}

