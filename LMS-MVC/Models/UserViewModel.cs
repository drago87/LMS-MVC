using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_MVC.Models
{
    public class UserViewModel
    {
        public ApplicationUser user { get; set; }
        public int ClassunitId { get; set; }
        public string RoleId { get; set; }
    }
}