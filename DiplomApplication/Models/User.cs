using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}