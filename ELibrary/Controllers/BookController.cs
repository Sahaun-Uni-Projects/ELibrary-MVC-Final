using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class BookController : Controller {
        private ELibraryEntities db = new ELibraryEntities();

        // GET: Book
        public ActionResult Index(int? id) {
            Book book = db.Books.FirstOrDefault(b => b.id == id);
            if (book == null) return (ActionResult)HttpNotFound();

            BookRecord record = db.BookRecords.FirstOrDefault(b => b.Book1.id == id);
            ViewBag.record = record;

            return View(book);
        }

        public ActionResult AddToCart(int id) {
            Cart cart = (Cart)Session["cart"];
            Book book = db.Books.FirstOrDefault(b => b.id == id);
            cart.AddCartItem(book, 1);
            return RedirectToAction("Index", new { id = id });
        }
    }
}