using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class NoPermissionController : Controller
    {
        // GET: NoPermission
        public ActionResult Index()
        {
            return View();
        }
    }
}