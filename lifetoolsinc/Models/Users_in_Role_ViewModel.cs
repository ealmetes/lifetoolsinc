using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Models
{
    public class Users_in_Role_ViewModel : Controller
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}