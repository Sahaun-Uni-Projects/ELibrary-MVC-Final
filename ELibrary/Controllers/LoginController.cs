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
                    User userCheck = db.Users.FirstOrDefault(u => (u.email == user.email) && (u.pass == user.pass));
                    if (userCheck == null) {
                        throw new Exception("Credentials do not match");
                    } else {
                        Session["cart"] = new Cart();
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