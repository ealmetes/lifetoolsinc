using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Access
    {
        [Key]
        // 
        public string key { get; set; }
        public string email { get; set; }

    }
}