using DiplomApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DiplomApplication.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize]
        public ActionResult DocList()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}