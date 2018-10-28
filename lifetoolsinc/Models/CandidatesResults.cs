using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class CandidatesResults 
    {
      [Key]
      public string attribute { get; set; }
      public int value { get; set; }    
      public int classid { get; set; }
      public int ClassCategoryID { get; set; }
      public string accesskey { get; set; }
      public string email { get; set; }
       public string Title { get; set; }

        public static List<CandidatesResults> results = new List<CandidatesResults>();
        public static List<Contents> contents = new List<Contents>();
        public static List<Accounts> accounts = new List<Accounts>();
        public static List<AttClasses> classes = new List<AttClasses>();
        public static List<ScoreDetails> scoredetails = new List<ScoreDetails>();

    }
}