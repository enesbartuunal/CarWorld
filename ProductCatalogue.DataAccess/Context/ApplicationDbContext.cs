using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalogue.DataAccess.Context
{
    public class ApplicationDbContext:IdentityDbContext<User> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
              
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Offer> Offers { get; set; }
           
    }
}
