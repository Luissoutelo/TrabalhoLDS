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

        public required int ShoppingCartId { get; set; }

        public required int FavoritesPageId { get; set; }
    }
}
