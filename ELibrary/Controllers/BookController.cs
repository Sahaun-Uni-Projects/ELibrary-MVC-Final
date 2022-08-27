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
            return ((book == null) ? (ActionResult)HttpNotFound() : View(book));
        }
    }
}