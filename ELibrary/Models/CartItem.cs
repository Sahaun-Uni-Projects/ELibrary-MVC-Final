using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.Models {
    public class CartItem {
        public Book book { get; set; }
        public int quantity { get; set; }

        public CartItem(Book _book, int _quantity) {
            book = _book;
            quantity = _quantity;
        }
    }
}