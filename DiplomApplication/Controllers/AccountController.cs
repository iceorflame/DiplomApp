using DiplomApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;

namespace DiplomApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.UserLogin == model.UserLogin && u.UserPassword == model.UserPassword);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserLogin, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Не знайдено користувача з таким логіном і паролем");
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Config()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Config(PasswordModel model)
        {
            UserContext db = new UserContext();
            if (ModelState.IsValid)
            {
                User user = null;
                user = db.Users.FirstOrDefault(u => u.UserLogin == User.Identity.Name && u.UserPassword == model.UserPassword);
                if (user != null && model.NewPassword == model.ConfirmPassword)
                {
                    user.UserPassword = model.NewPassword;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Logoff", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Не співпадають паролі. Спробуйте знову.");
                }
            }
                return View(model);
        }
    }
}