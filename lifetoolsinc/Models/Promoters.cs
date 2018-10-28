using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Promoters
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Promoter")]
        public string promoter { get; set; }
        [Display(Name = "Discount %")]
        public int? discount { get; set; }
        [Display(Name = "Code")]
        public string code { get; set; }
        [Display(Name = "Active")]
        public Boolean active { get; set; }
        [Display(Name = "Start Date")]
        public DateTime startdate { get; set; }
        [Display(Name = "Expire Date")]
        public DateTime expiredate { get; set; }
    }
}