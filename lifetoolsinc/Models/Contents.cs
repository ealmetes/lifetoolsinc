using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Contents 
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string text { get; set; }
        public string type { get; set; }
        public static List<Contents> attributes = new List<Contents>();
        public IEnumerable<lifetoolsinc.Models.Contents> contents;
    }
}