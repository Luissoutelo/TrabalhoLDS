namespace LDS_2425.Models
{
    public class ShoppingCartLoan_Listing
    {
        public int Id { get; set; }

        public required int ShoppingCartId { get; set; }

        public required int Loan_ListingId { get; set; }
    }
}
