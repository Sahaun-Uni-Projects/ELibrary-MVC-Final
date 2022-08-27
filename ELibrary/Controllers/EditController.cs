using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class EditController : Controller {
        private ELibraryEntities db = new ELibraryEntities();

        // GET: Edit

        public Boolean IsAdminSession() {
            User sessionUser = (User)Session["user"];
            if (sessionUser != null) {
                if (sessionUser.type_ == 1) return true;
            }
            return false;
        }

        public ActionResult Book(int? id) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            int _id = id ?? 1;
            var book = db.Books.FirstOrDefault(b => b.id == _id);
            return ((book == null) ? (ActionResult)HttpNotFound() : View(book));
        }

        public ActionResult Author(int? id) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            int _id = id ?? 1;
            var author = db.Authors.FirstOrDefault(a => a.id == _id);
            return ((author == null) ? (ActionResult)HttpNotFound() : View(author));
        }

        public ActionResult User_(string email) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            if (String.IsNullOrEmpty(email)) email = db.Users.First().email;
            var user = db.Users.FirstOrDefault(u => u.email == email);
            return ((user == null) ? (ActionResult)HttpNotFound() : View(user));
        }

        [HttpPost]
        public ActionResult BookUpdate(int id, Book book) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            Book newBook = db.Books.FirstOrDefault(b => b.id == id);
            newBook.id = book.id;
            newBook.details = book.details;
            newBook.author = book.author;
            newBook.name = book.name;
            db.SaveChanges();
            return RedirectToAction("Book", new { id = id });
        }

        [HttpPost]
        public ActionResult AuthorUpdate(int id, Author author) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            Author newAuthor = db.Authors.FirstOrDefault(a => a.id == id);
            newAuthor.name = author.name;
            newAuthor.details = author.details;
            db.SaveChanges();
            return RedirectToAction("Author", new { id = id });
        }

        [HttpPost]
        public ActionResult UserUpdate(string email, User user) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            User newUser = db.Users.FirstOrDefault(u => u.email == email);
            newUser.fullname = user.fullname;
            newUser.address_ = user.address_;
            newUser.phone = user.phone;
            newUser.pass = user.pass;
            db.SaveChanges();
            return RedirectToAction("User", new { email = email });
        }
    }
}