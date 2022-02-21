#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Data
{
    public class MyShopContext : DbContext
    {
        public MyShopContext (DbContextOptions<MyShopContext> options)
            : base(options)
        {
        }

        public DbSet<MyShop.Models.User> User { get; set; }

        public DbSet<MyShop.Models.Category> Category { get; set; }

        public DbSet<MyShop.Models.Order> Order { get; set; }

        public DbSet<MyShop.Models.Good> Good { get; set; }

        public DbSet<MyShop.Models.OrderGood> OrderGood { get; set; }
    }
}
