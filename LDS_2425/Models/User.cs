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

        public ICollection<Receipt>? ReceiptsOwner { get; set; }

        public ICollection<Receipt>? ReceiptsUser { get; set; }

        public required ShoppingCart ShoppingCart { get; set; }

        public required int ShoppingCartId { get; set; }

        public required FavoritesPage FavoritesPage { get; set; }

        public required int FavoritesPageId { get; set; }
    }
}
