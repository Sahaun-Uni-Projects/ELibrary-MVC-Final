using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class RegisterController : Controller
    {
        private ELibraryEntities db = new ELibraryEntities();
        
        // GET: Register
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid) {
                try {
                    if (String.IsNullOrEmpty(user.email) || String.IsNullOrEmpty(user.pass) || String.IsNullOrEmpty(user.fullname)) {
                        throw new Exception("Fields can not be empty");
                    } else if (!user.email.Contains("@") || !user.email.Contains(".com")) {
                        throw new Exception("Invalid email");
                    }

                    user.email = user.email.ToLower();
                    User userCheck = db.Users.FirstOrDefault(u => u.id == user.id);
                    if (userCheck != null) {
                        TempData["alert"] = "Email exists already";
                        return RedirectToAction("Index", "Login");
                    }

                    user.joindate = DateTime.Now;
                    user.type_ = 0;

                    db.Users.Add(user);
                    db.SaveChanges();
                    TempData["alert"] = $"User account for {user.fullname} created successfully!";
                } catch (Exception e) {
                    TempData["alert"] = e.Message;
                }
            }
            return View();
        }
    }
}