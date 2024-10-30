using System.ComponentModel.DataAnnotations;

namespace LDS_2425.Models
{
    public class ShoppingCartMachine
    {
        [Key]
        public int Id { get; set; }

        public required int ShoppingCartId { get; set; }

        public required int MachineId { get; set; }
    }
}
