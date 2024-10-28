namespace LDS_2425.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public required string CompanyName { get; set; }

        public required string CompanyEmail { get; set; }

        public int CompanyPhone { get; set; }

        public required int CompanyNif { get; set; }

        public required DateTime DateReceipt { get; set; }

        public int? OwnerId { get; set; }

        public required int UserId { get; set; }

        public int? Loan_ListingId { get; set; }

        public int? MachineId { get; set; }

        public int? ContractId { get; set; }
    }
}
