using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibrary.Controllers
{
    public class CartController : Controller {
        private ELibraryEntities db = new ELibraryEntities();

        // GET: Cart
        public ActionResult Index() {
            return View();
        }

        public ActionResult Add(int id) {
            ((Cart)Session["cart"]).AddAt(id, 1);
            return RedirectToAction("Index");
        }

        public ActionResult Subtract(int id) {
            ((Cart)Session["cart"]).SubtractAt(id, 1);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id) {
            ((Cart)Session["cart"]).CartItems.RemoveAt(id);
            return RedirectToAction("Index");
        }

        public ActionResult Validate(PurchaseInfo info) {
            string trxID = info.trxID;
            if (!string.IsNullOrEmpty(trxID)) {
                User sessionUser = (User)Session["user"];

                PurchaseRecord record = new PurchaseRecord() {
                    user_ = sessionUser.id,
                    date_ = DateTime.Now,
                    trx = trxID,
                    address_ = sessionUser.address_,
                    confirmed = 0
                };
                
                db.PurchaseRecords.Add(record);
                db.SaveChanges();
            }
            ((Cart)Session["cart"]).Clear();
            return RedirectToAction("Index");
        }
    }
}