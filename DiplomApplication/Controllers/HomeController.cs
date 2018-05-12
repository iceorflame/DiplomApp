using DiplomApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using System.IO;

namespace DiplomApplication.Controllers
{
    public class HomeController : Controller
    {
        UserContext db = new UserContext();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            SelectList priorities = new SelectList(db.Priorities, "PriorityId", "PriorityName");
            ViewBag.Priorities = priorities;
            SelectList users = new SelectList( db.Users.Where(c=>c.RoleId>2), "UserId", "UserName" );
            
            ViewBag.Users = users;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(Order order, Models.File file, HttpPostedFileBase uploadFile = null)
        {
            if (uploadFile != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadFile.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadFile.ContentLength);
                }
                // установка массива байтов
                file.FileByte = imageData;
                file.FileName = uploadFile.FileName;
                file.FileType = "";
                db.Files.Add(file);
                order.FileId = file.FileId;
            }
            order.OrderOut = DateTime.Now;
            
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("DocList","Home");
        }

        [Authorize]
        public ActionResult DocList()
        {
            var orders = db.Orders.Include(p=>p.User);
            var orders2 = orders.Include(p=>p.File);
            return View(orders2.ToList());
        }

        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                Models.File file = db.Files.Find(order.FileId);
                if (file != null)
                {
                    db.Files.Remove(file);
                    db.SaveChanges();
                }
                db.Orders.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("DocList", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                DateTime date = order.OrderDate;
                ViewBag.date = date;
                SelectList priorities = new SelectList(db.Priorities, "PriorityId", "PriorityName", order.PriorityId);
                ViewBag.Priorities = priorities;
                SelectList users = new SelectList(db.Users.Where(c => c.RoleId > 2), "UserId", "UserName", order.UserId);
                ViewBag.Users = users;
                return View(order);
            }
            return RedirectToAction("DocList", "Home");
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DocList", "Home");
        }

    }
}