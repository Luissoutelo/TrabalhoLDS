﻿namespace LDS_2425.Models
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

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public required Boolean WorkerAvailable { get; set; }

        public required Boolean TransportNecessary { get; set; }

        public required int CategoryId { get; set; }

        public required int OwnerId { get; set; }

        public int UserId { get; set; }
    }
}
