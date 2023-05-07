using PizzaOrderAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderAPI.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        public Address Location { get; set; }
        [MaxLength(20), Required]
        public string Phone { get; set; }
    }

    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StreetNo { get; set; }
        [MaxLength(50), Required]
        public string StreetName { get; set; }
        [MaxLength(10), Required]
        public StreetType StreetType { get; set; }
        [MaxLength(50), Required]
        public string Suburb { get; set; }
        [MaxLength(4), Required]
        public string Postcode { get; set; }
        [MaxLength(3), Required]
        public State State { get; set; }
    }

    public class StoreQuery
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public State? State { get; set; }
        public string? Suburb { get; set; }
        public string? Postcode { get; set; }
    }

    public class StoreUpdate
    {
        public string? Name { get; set; }
        public string? phone { get; set; }
        public AddressUpdate? Location { get; set;}
    }

    public class AddressUpdate
    {
        public string? StreetNo { get; set; }
        public string? StreetName { get; set; }
        public StreetType? StreetType { get; set; }
        public string? Suburb { get; set; }
        public State? State { get; set; }
        public string? Postcode { get; set; }
    }
}
