using System.ComponentModel.DataAnnotations;

namespace LDS_2425.Models
{
    public class ShoppingCart
    {
        [Key]

        public ICollection<ShoppingCartMachine>? Machines { get; set; }
        public int Id { get; set; }
        public int userId {  get; set; }

    }
}
