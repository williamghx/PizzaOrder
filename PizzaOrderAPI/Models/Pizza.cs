using System.ComponentModel.DataAnnotations;

namespace PizzaOrderAPI.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
    }

    public class Topping
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
