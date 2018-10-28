using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Checkout 
    {
        public int keycount { get; set; }
        public decimal? amount { get; set; }
        public int promocodeid { get; set; }
        public int? discount { get; set; }
        public decimal? totaldiscount { get; set; }      
        public decimal? totalamount { get; set; }
        
    }
}