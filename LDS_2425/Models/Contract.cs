namespace LDS_2425.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public required string Description { get; set; }

        public  Loan_Listing Listing { get; set; }

        public  int ListingId { get; set; }

        public  Receipt Receipt { get; set; }

        public int ReceiptId { get; set; }
    }
}
