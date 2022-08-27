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
                    if (String.IsNullOrEmpty(user.user_email) || String.IsNullOrEmpty(user.user_password) || String.IsNullOrEmpty(user.user_fullname)) {
                        throw new Exception("Fields can not be empty");
                    } else if (!user.user_email.Contains("@") || !user.user_email.Contains(".com")) {
                        throw new Exception("Invalid email");
                    }

                    user.user_email = user.user_email.ToLower();
                    User userCheck = db.Users.FirstOrDefault(u => u.user_email == user.user_email);
                    if (userCheck != null) {
                        TempData["alert"] = "Email exists already";
                        return RedirectToAction("Index", "Login");
                    }

                    db.Users.Add(user);
                    db.SaveChanges();
                } catch (Exception e) {
                    TempData["alert"] = e.Message;

                }
            }
            return View();
        }
    }
}