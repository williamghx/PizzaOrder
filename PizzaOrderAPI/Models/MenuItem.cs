using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderAPI.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Pizza")]
        public virtual int PizzaId { get; set; }

        [Display(Name = "Store")]
        public virtual int StoreId { get; set; }

        public decimal Price { get; set; }
        
        [ForeignKey("PizzaId")]
        public virtual Pizza Pizza { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
    }

    public class MenuUpdate
    {
        public int? StoreId { get; set; }
        public int? PizzaId { get; set; }
        public Pizza? Pizza { get; set; }
        public decimal? Price { get; set; }
    }
}
