using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomApplication.Models
{
    public class LoginModel
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }

    public class PasswordModel
    {
        public string UserPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}