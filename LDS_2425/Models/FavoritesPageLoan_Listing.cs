namespace LDS_2425.Models
{
    public class FavoritesPageLoan_Listing
    {
        public int Id { get; set; }

        public required int FavoritesPageId { get; set; }

        public required int Loan_ListingId { get; set; }
    }
}
