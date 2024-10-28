namespace LDS_2425.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public ICollection<ShoppingCartMachine>? Machines { get; set; }

        public ICollection<ShoppingCartLoan_Listing>? LoanListings { get; set; }

        public required int UserId { get; set; }
    }
}
