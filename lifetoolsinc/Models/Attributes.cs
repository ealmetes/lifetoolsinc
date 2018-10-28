using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Attributes 
    {
        // GET: Attributes
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int id { get; set; }

        [Display(Name = "Choices")]
        public string Text { get; set; }
        [Display(Name = "Page ID")]
        public int pageid { get; set; }
        [Display(Name = "Class Category ID")]
        public int ClassCategoryid { get; set; }
    }
}