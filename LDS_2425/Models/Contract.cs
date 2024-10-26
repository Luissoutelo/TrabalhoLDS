namespace LDS_2425.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public required string Description { get; set; }

        public required Loan_Listing Listing { get; set; }

        public required int ListingId { get; set; }
    }
}
