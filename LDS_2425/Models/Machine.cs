namespace LDS_2425.Models
{
    public class Machine
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Brand { get; set; }

        public required string Model { get; set; }

        public required string Condition { get; set; }

        public required int Capacity { get; set; }

        public string? Description { get; set; }

        public required DateOnly Year_of_Manufacture { get; set; }

        public required float Price { get; set; }

        public required string Image {  get; set; }

        public required int CategoryId { get; set; }
    }
}
