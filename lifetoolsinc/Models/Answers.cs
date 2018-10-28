using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Answers
    {
      
      public string accesskey { get; set; }
      [Key]
      public int id { get; set; }
      public string attribute { get; set; }
      public int value { get; set; }
      public int classid { get; set; }
      public string email { get; set; }
      public DateTime datetaken { get; set; }
      public string orguid { get; set; }
      public int pageid { get; set; }
    }
}