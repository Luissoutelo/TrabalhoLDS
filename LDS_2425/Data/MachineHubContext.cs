using LDS_2425.Models;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Data
{
    public class MachineHubContext : DbContext
    {
        public MachineHubContext(DbContextOptions<MachineHubContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Loan_Listing> Loan_Listings { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartLoan_Listing> ShoppingCartLoan_ListingConnections { get; set; }

        public DbSet<ShoppingCartMachine> ShoppingCartMachineConnections { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan_Listing>()
                .HasOne(l => l.Owner)
                .WithMany(u => u.LoanedIn)
                .HasForeignKey(l => l.OwnerId)
                .OnDelete(DeleteBehavior.Restrict); //Para não apagar listagens se apagarmos o perfil

            modelBuilder.Entity<Loan_Listing>()
                .HasOne(l => l.User)
                .WithMany(u => u.LoanedOut)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
