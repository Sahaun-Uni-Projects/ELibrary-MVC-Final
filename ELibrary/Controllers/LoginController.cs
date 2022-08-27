using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class LoginController : Controller {
        private ELibraryEntities db = new ELibraryEntities();

        // GET: Login
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user) {
            if (ModelState.IsValid) {
                try {
                    user.user_email = user.user_email.ToLower();
                    User userCheck = db.Users.FirstOrDefault(u => (u.user_email == user.user_email) && (u.user_password == user.user_password));
                    if (userCheck == null) {
                        throw new Exception("Credentials do not match");
                    } else {
                        Session["user"] = userCheck;
                        return RedirectToAction("Index", "Profile");
                    }
                } catch (Exception e) {
                    TempData["alert"] = e.Message;
                }
            }
            return View();
        }
    }
}