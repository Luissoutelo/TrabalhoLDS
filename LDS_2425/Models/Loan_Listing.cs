namespace LDS_2425.Models
{
    public class Loan_Listing
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Brand { get; set; }

        public required string Model { get; set; }

        public required string Condition { get; set; }

        public required int Capacity { get; set; }

        public string? Description { get; set; }

        public required DateOnly YearManufacture { get; set; }

        public required float Price { get; set; }

        public required string Image { get; set; }

        public required DateOnly DateListed { get; set; }

        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; }

        public required Boolean WorkerAvailable { get; set; }

        public required Boolean TransportNecessary { get; set; }

        public required Category Category { get; set; }

        public required int CategoryId { get; set; }

        public required User Owner { get; set; }

        public required int OwnerId { get; set; }

        public required User User { get; set; }

        public required int UserId { get; set; }

        public ICollection<Contract>? Contracts { get; set; }

        public ICollection<ShoppingCartLoan_Listing>? ShoppingCarts { get; set; }

        public ICollection<FavoritesPageLoan_Listing>? FavoritesPages { get; set; }
    }
}
