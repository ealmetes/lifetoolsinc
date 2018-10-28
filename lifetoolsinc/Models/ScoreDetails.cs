using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class ScoreDetails
    {
        public string accesskey { get; set; }
        public int classid { get; set; }
        public int score { get; set; }
        public string scorelevel { get; set; }
        public string scoredetail { get; set; }
        public string title { get; set; }
        public string color { get; set; }

    }
}