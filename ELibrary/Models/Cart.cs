using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.Models {
    public class Cart {
        private ELibraryEntities db = new ELibraryEntities();
        public List<CartItem> CartItems { get; set; }
        
        public Cart() {
            CartItems = new List<CartItem>();
        }

        public void AddCartItem(CartItem item) {
            int ind = -1;
            int q = -1;
            for (int i = 0; i < CartItems.Count; ++i) {
                CartItem cartItem = CartItems[i];
                if (cartItem.book.id == item.book.id) {
                    ind = i;
                    q = cartItem.quantity;
                    break;
                }
            }

            if (ind == -1) {
                CartItems.Add(item);
            } else {
                CartItems.RemoveAt(ind);
                AddCartItem(item.book, item.quantity + q);
            }
        }

        public void AddCartItem(Book book, int quantity) {
            AddCartItem(new CartItem(book, quantity));
        }

        public void AddAt(int ind, int q) {
            CartItem cartItem = CartItems[ind];
            cartItem.quantity += q;
        }

        public void SubtractAt(int ind, int q) {
            CartItem cartItem = CartItems[ind];
            cartItem.quantity -= q;
            if (cartItem.quantity <= 0) {
                CartItems.RemoveAt(ind);
            }
        }

        public int GetCost() {
            int cost = 0;
            foreach (CartItem item in CartItems) {
                BookRecord bookRecord = db.BookRecords.FirstOrDefault(b => b.book == item.book.id);
                if (bookRecord != null) {
                    cost += bookRecord.price * item.quantity;
                }
            }
            return cost;
        }
    }
}