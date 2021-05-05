
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VMP.NetCore.Exercise01.Model.Models;

namespace VMP.NetCore.Exercise01.Data
{
    public class ProjectDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Product> Product { get; set; }

        public ProjectDbContext() { }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        private const string ConnectionString = @"Data Source=.\En2017;Initial Catalog=NetCoreDBExercise;Persist Security Info=True;User ID=sa;Password=123456";

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(ConnectionString);
        //    //optionsBuilder.UseSqlServer(ConfigValues.DefaultConnectionString);
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(ConfigValues.DefaultConnectionString);
        //}
    }
}
