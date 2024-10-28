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

        public DbSet<FavoritesPage> FavoritesPages { get; set; }

        public DbSet<FavoritesPageLoan_Listing> FavoritesPageLoan_Listings { get; set; }

        public DbSet<FavoritesPageMachine> FavoritesPageMachines { get; set; }

        public DbSet<Loan_Listing> Loan_Listings { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartLoan_Listing> ShoppingCartLoan_ListingConnections { get; set; }

        public DbSet<ShoppingCartMachine> ShoppingCartMachineConnections { get; set; }

        public DbSet<User> Users { get; set; }

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
