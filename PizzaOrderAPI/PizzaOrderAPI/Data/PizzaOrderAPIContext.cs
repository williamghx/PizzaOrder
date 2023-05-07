using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Models;

namespace PizzaOrderAPI.Data
{
    public class PizzaOrderAPIContext : DbContext
    {
        public PizzaOrderAPIContext (DbContextOptions<PizzaOrderAPIContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaOrderAPI.Models.Pizza> Pizza { get; set; } = default!;

        public DbSet<PizzaOrderAPI.Models.Store>? Store { get; set; }
    }
}
