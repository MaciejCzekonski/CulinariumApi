using System;
using System.Collections.Generic;
using System.Text;
using Culinarium.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Culinarium.Repository.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasMany(x => x.Ingredients);
            modelBuilder.Entity<Recipe>().HasOne(x => x.Picture).WithOne(x => x.Recipe).HasForeignKey<Picture>(x => x.RecipeId);
            modelBuilder.Entity<Recipe>().HasMany(x => x.Ratings).WithOne(x => x.Recipe);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
