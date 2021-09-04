using Microsoft.EntityFrameworkCore;
using ShopBridges.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridges.Business
{
    public class ShopBridgesContext : DbContext
    {

        public ShopBridgesContext(DbContextOptions<ShopBridgesContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
