using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class AttPages
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int id { get; set; }
       
        [Display(Name = "Page Number")]
        public int page { get; set; }
        [Display(Name = "Active")]
        public Boolean active { get; set; }

        [Display(Name = "Muted")]
        public Boolean muted { get; set; }

    }
}