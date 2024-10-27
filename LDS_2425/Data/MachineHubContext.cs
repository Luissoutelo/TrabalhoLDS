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
            // Listagem
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

            //Recibo
            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Owner)
                .WithMany(u => u.ReceiptsOwner)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.User)
                .WithMany(u => u.ReceiptsUser)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Loan_Listing)
                .WithMany()
                .HasForeignKey(r => r.Loan_ListingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Machine)
                .WithMany()
                .HasForeignKey(r => r.MachineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Contract)
                .WithOne(c => c.Receipt)
                .HasForeignKey<Contract>(c => c.ReceiptId); // Contrato é dependente de Receipt

            // User
            modelBuilder.Entity<User>()
                .HasOne(u => u.ShoppingCart)
                .WithOne(s => s.User)
                .HasForeignKey<ShoppingCart>(u => u.UserId); // Carrinho é dependente do utilizador

            modelBuilder.Entity<User>()
                .HasOne(u => u.FavoritesPage)
                .WithOne(f => f.User)
                .HasForeignKey<FavoritesPage>(u => u.UserId); // Favoritos é dependente do utilizador
        }

        public bool ValidateReceipt(Receipt receipt)
        {
            if (!(receipt.Loan_ListingId.HasValue ^ receipt.MachineId.HasValue))
            {
                return false; // Falha se nenhum ou os dois tiverem valor
            }

            // If the receipt refers to a LoanListing, a ContractId is required
            if (receipt.Loan_ListingId != null && !receipt.ContractId.HasValue)
            {
                return false; // Se for listagem e não tiver contrato
            }

            // Passa se for venda de máquina ou se for listagem com contrato
            return true;
        }
    }
}
