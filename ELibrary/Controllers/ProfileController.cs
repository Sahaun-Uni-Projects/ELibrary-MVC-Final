using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class ProfileController : Controller {
        private ELibraryEntities db = new ELibraryEntities();

        // GET: Profile
        public ActionResult Index()
        {
            User user = (User)Session["user"];
            if (user != null) {
                User userCheck = db.Users.FirstOrDefault(u => u.id == user.id);
                if (userCheck != null) {
                    return View(user);
                }
            }
            return RedirectToAction("Index", "Login");
        }
        
        public ActionResult UserUpdate(int id, User user) {
            User newUser = db.Users.FirstOrDefault(u => u.id == id);
            newUser.fullname = user.fullname;
            newUser.address_ = user.address_;
            newUser.phone = user.phone;
            newUser.pass = user.pass;
            db.SaveChanges();
            Session["user"] = newUser;
            return RedirectToAction("Index");
        }
    }
}