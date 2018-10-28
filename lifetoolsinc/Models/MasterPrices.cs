using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class MasterPrices
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Code Price Per Unit")]
        public decimal? codeprice { get; set; }

        [Display(Name = "Type")]
        public string type { get; set; }
    }
}