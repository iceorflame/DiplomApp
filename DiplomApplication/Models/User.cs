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
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        ICollection<Order> Orders { get; set; }
        public User()
        {
            Orders = new List<Order>();
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderOut { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class Priority
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Priority()
        {
            Orders = new List<Order>();
        }
    }
}