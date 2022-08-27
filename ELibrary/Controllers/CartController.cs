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
            Cart cart = (Cart)Session["cart"];

            if (!string.IsNullOrEmpty(trxID)) {
                User sessionUser = (User)Session["user"];

                PurchaseRecord record = new PurchaseRecord() {
                    user_ = sessionUser.id,
                    date_ = DateTime.Now,
                    trx = trxID,
                    price = cart.GetCost(),
                    address_ = sessionUser.address_,
                    confirmed = 0
                };

                foreach (CartItem item in cart.CartItems) {
                    PurchaseRecordBook recordBook = new PurchaseRecordBook() {
                        record = record.id,
                        book = item.book.id,
                        quantity = item.quantity
                    };
                    db.PurchaseRecordBooks.Add(recordBook);
                }

                db.PurchaseRecords.Add(record);
                db.SaveChanges();
            }

            cart.Clear();
            return RedirectToAction("Index");
        }
    }
}