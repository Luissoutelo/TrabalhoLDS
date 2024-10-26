namespace LDS_2425.Models
{
    public class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required int PhoneNumber { get; set; }

        public required string type { get; set; }

        public ICollection<Loan_Listing>? LoanedIn { get; set; }

        public ICollection<Loan_Listing>? LoanedOut { get; set; }
    }
}
