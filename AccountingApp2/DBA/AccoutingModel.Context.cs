//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccountingApp2.DBA
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AccountingDBEntities1 : DbContext
    {
        public AccountingDBEntities1()
            : base("name=AccountingDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Crypto> Cryptoes { get; set; }
        public virtual DbSet<CryptoOrder> CryptoOrders { get; set; }
        public virtual DbSet<StockOrder> StockOrders { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
    }
}
