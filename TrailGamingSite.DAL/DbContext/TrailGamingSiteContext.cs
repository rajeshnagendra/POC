using TrailGamingSite.Models.Model;
using System.Data.Entity;

namespace TrailGamingSite.DAL
{
    public class TrailGamingSiteContext : DbContext
    {
        //public TrailGamingSiteContext() : base()
        //{           
        //}

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
