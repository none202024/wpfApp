using System.Data.Entity;
using Lab5.Models;

namespace Lab5
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<StatusAgreement> StatusesAgreement { get; set; }
        public DbSet<TypeAgreement> TypesAgreements{ get; set; }
    }
}
