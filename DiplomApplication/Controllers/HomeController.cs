﻿using DiplomApplication.Models;
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
        public ActionResult Add(Order order, Models.File file, HttpPostedFileBase uploadFile)
        {
            byte[] imageData = null;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(uploadFile.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadFile.ContentLength);
            }
            // установка массива байтов
            file.FileByte = imageData;
            file.FileName = "";
            file.FileType = "";
            order.OrderOut = DateTime.Now;
            db.Files.Add(file);
            db.Orders.Add(order);
            db.SaveChanges();
            db.Files.Add(file);
            return RedirectToAction("DocList","Home");
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