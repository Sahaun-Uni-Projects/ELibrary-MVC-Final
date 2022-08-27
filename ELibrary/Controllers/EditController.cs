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

        public ActionResult User_(int? id) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            int _id = id ?? 1;
            var user = db.Users.FirstOrDefault(u => u.id == _id);
            return ((user == null) ? (ActionResult)HttpNotFound() : View(user));
        }

        public ActionResult Featured() {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            List<Book> featured = new List<Book>();
            foreach (FeaturedBook fb in db.FeaturedBooks.ToList()) {
                featured.Add(fb.Book1);
            }
            ViewBag.books = db.Books.ToList();
            ViewBag.featured = featured;

            return View();
        }

        public ActionResult FeaturedToggle(int id) {
            FeaturedBook featured = db.FeaturedBooks.FirstOrDefault(f => f.Book1.id == id);

            if (featured == null) {
                db.FeaturedBooks.Add(new FeaturedBook() {
                    book = db.Books.FirstOrDefault(b => b.id == id).id
                });
            } else {
                db.FeaturedBooks.Remove(featured);
            }
            db.SaveChanges();

            return RedirectToAction("Featured");
        }

        [HttpPost]
        public ActionResult BookUpdate(int id, Book book) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            Book newBook = db.Books.FirstOrDefault(b => b.id == id);
            newBook.id = book.id;
            newBook.cover = book.cover;
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
        public ActionResult UserUpdate(int id, User user) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            User newUser = db.Users.FirstOrDefault(u => u.id == id);
            newUser.email = user.email;
            newUser.fullname = user.fullname;
            newUser.address_ = user.address_;
            newUser.phone = user.phone;
            newUser.pass = user.pass;
            db.SaveChanges();
            return RedirectToAction("User", new { id = id });
        }
    }
}