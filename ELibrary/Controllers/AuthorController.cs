using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class AuthorController : Controller {
        private ELibraryEntities db = new ELibraryEntities();

        // GET: Author
        public ActionResult Index(int? id)
        {
            Author author = db.Authors.FirstOrDefault(a => a.id == id);
            return ((author == null) ? (ActionResult)HttpNotFound() : View(author));
        }
    }
}