﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ELibraryEntities : DbContext
    {
        public ELibraryEntities()
            : base("name=ELibraryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<FeaturedBook> FeaturedBooks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BookRecord> BookRecords { get; set; }
        public virtual DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public virtual DbSet<PurchaseRecord> PurchaseRecords { get; set; }
        public virtual DbSet<PurchaseRecordBook> PurchaseRecordBooks { get; set; }
    }
}
