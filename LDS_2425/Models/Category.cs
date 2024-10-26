namespace LDS_2425.Models
{
    public class Category
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Machine>? Machines { get; set; }

        public ICollection<Loan_Listing>? LoanListings { get; set; }
    }
}
