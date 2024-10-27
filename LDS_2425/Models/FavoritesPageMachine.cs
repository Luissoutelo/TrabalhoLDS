namespace LDS_2425.Models
{
    public class FavoritesPageMachine
    {
        public int Id { get; set; }

        public required int FavoritesPageId { get; set; }

        public required int MachineId { get; set; }
    }
}
