using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class RecordController : Controller {
        private ELibraryEntities db = new ELibraryEntities();


        public Boolean IsAdminSession() {
            User sessionUser = (User)Session["user"];
            if (sessionUser != null) {
                if (sessionUser.type_ == 1) return true;
            }
            return false;
        }

        // GET: Record
        public ActionResult Index() {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            ViewBag.records = db.PurchaseRecords.ToList();
            return View();
        }

        public ActionResult Confirm(int id) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            PurchaseRecord record = db.PurchaseRecords.FirstOrDefault(r => r.id == id);
            if (record != null) {
                var items = db.PurchaseRecordBooks.Where(r => r.record == record.id).ToList();
                foreach (var item in items) {
                    var bookRecord = db.BookRecords.FirstOrDefault(r => r.Book1.id == item.Book1.id);
                    bookRecord.quantity -= item.quantity ?? 0;
                    db.SaveChanges();
                }

                record.confirmed = 1;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {
            if (!IsAdminSession()) return RedirectToAction("Index", "NoPermission");

            PurchaseRecord record = db.PurchaseRecords.FirstOrDefault(r => r.id == id);
            if (record != null) {
                record.confirmed = 2;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}