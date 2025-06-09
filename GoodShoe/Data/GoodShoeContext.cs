using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Models;

namespace GoodShoe.Data
{
    public class GoodShoeContext : DbContext
    {
        public GoodShoeContext (DbContextOptions<GoodShoeContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; } = default!;
    }
}
