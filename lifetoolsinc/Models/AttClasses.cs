using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class AttClasses
    {
      [Key]
      public int id { get; set; }
      public string Text { get; set; }
      public int ClassCategoryID { get; set; }
      public int Score { get; set; }
      public string ScoreLevel { get; set; }
      public string Title { get; set; }
      public static List<AttClasses> attributes = new List<AttClasses>();
    }
}