//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ELibrary.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseRecordBook
    {
        public int id { get; set; }
        public Nullable<int> record { get; set; }
        public Nullable<int> book { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual Book Book1 { get; set; }
        public virtual PurchaseRecord PurchaseRecord { get; set; }
    }
}
