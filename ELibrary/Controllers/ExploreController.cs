using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class ExploreController : Controller {
        private ELibraryEntities db = new ELibraryEntities();

        // GET: Explore
        public ActionResult Index(string sort, int? _page) {
            if (String.IsNullOrWhiteSpace(sort)) sort = "Title";

            int page = _page ?? 1;
            List<Book> books = db.Books.ToList();

            switch (sort) {
                case "Title":
                    books.Sort((b1, b2) => {
                        return (b1.name.CompareTo(b2.name));
                    });
                    break;
                case "Author":
                    books.Sort((b1, b2) => {
                        return (b1.Author1.name.CompareTo(b2.Author1.name));
                    });
                    break;
                default: break;
            }

            return View(new BookList() {
                Books = books
            });
        }
    }
}